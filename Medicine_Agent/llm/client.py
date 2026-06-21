import asyncio
import json
import logging
from typing import List, Optional, Callable, Dict, Any

from openai import OpenAI, APIError, RateLimitError, APITimeoutError
from openai.types.chat import ChatCompletion

from .schemas import Message, LLMResponse, ToolCall
from config.settings import LLMConfig

logger = logging.getLogger(__name__)


class LLMClient:
    """OpenAI-compatible LLM client."""

    def __init__(self, config: LLMConfig, api_key: str):
        """
        Initialize LLM client.

        Args:
            config: LLM configuration
            api_key: API key (resolved from config or env)
        """
        self.config = config
        self._client = OpenAI(
            api_key=api_key,
            base_url=config.base_url,
            timeout=config.request_timeout,
        )
        self._tools: List[Dict[str, Any]] = []

    def register_tool(self, name: str, description: str, parameters: dict) -> None:
        """Register a tool for function calling.

        Args:
            name: Tool name
            description: Tool description
            parameters: JSON Schema for tool parameters
        """
        self._tools.append({
            "type": "function",
            "function": {
                "name": name,
                "description": description,
                "parameters": parameters,
            },
        })

    def clear_tools(self) -> None:
        """Clear all registered tools."""
        self._tools.clear()

    async def chat(
        self,
        messages: List[Message],
        stream: bool = False,
        on_chunk: Optional[Callable[[str], None]] = None,
        use_tools: bool = True,
    ) -> LLMResponse:
        """Send a chat completion request.

        Args:
            messages: List of conversation messages
            stream: Whether to stream the response
            on_chunk: Callback for streaming chunks
            use_tools: If False, send request without tool definitions
                (useful for final answer generation after max iterations)

        Returns:
            LLMResponse with content and/or tool calls
        """
        max_retries = 3
        last_error = None

        for attempt in range(max_retries):
            try:
                return await asyncio.to_thread(self._make_request, messages, stream, on_chunk, use_tools)
            except RateLimitError as e:
                last_error = e
                wait_time = 2 ** attempt
                logger.warning(f"Rate limited, retrying in {wait_time}s (attempt {attempt + 1}/{max_retries})")
                await asyncio.sleep(wait_time)
            except APITimeoutError as e:
                last_error = e
                logger.warning(f"Timeout, retrying (attempt {attempt + 1}/{max_retries})")
            except APIError as e:
                last_error = e
                logger.error(f"API error: {e}")
                raise

        raise RuntimeError(f"Failed after {max_retries} retries: {last_error}")

    def _make_request(
        self,
        messages: List[Message],
        stream: bool,
        on_chunk: Optional[Callable[[str], None]],
        use_tools: bool = True,
    ) -> LLMResponse:
        """Make the actual API request."""
        openai_messages = [m.to_openai_dict() for m in messages]

        kwargs = {
            "model": self.config.model,
            "messages": openai_messages,
            "temperature": self.config.temperature,
            "max_tokens": self.config.max_tokens,
            "stream": stream,
        }

        if self._tools and use_tools:
            kwargs["tools"] = self._tools
            kwargs["tool_choice"] = "auto"

        if stream:
            return self._handle_stream(kwargs, on_chunk)
        else:
            return self._handle_sync(kwargs)

    def _handle_sync(self, kwargs: dict) -> LLMResponse:
        """Handle synchronous (non-streaming) response."""
        response: ChatCompletion = self._client.chat.completions.create(**kwargs)
        choice = response.choices[0]
        message = choice.message

        tool_calls = None
        if message.tool_calls:
            tool_calls = [
                ToolCall(
                    id=tc.id,
                    name=tc.function.name,
                    arguments=json.loads(tc.function.arguments) if tc.function.arguments else {},
                )
                for tc in message.tool_calls
            ]

        return LLMResponse(
            content=message.content,
            tool_calls=tool_calls,
            finish_reason=choice.finish_reason or "stop",
            model=response.model,
            usage=response.usage.model_dump() if response.usage else None,
        )

    def _handle_stream(
        self,
        kwargs: dict,
        on_chunk: Optional[Callable[[str], None]],
    ) -> LLMResponse:
        """Handle streaming response."""
        stream = self._client.chat.completions.create(**kwargs)

        content_parts: List[str] = []
        tool_calls_map: Dict[str, dict] = {}
        finish_reason = "stop"
        model = ""

        for chunk in stream:
            if chunk.model:
                model = chunk.model

            delta = chunk.choices[0].delta if chunk.choices else None
            if not delta:
                continue

            if delta.content:
                content_parts.append(delta.content)
                if on_chunk:
                    on_chunk(delta.content)

            if delta.tool_calls:
                for tc in delta.tool_calls:
                    if tc.id:
                        tool_calls_map[tc.index] = {
                            "id": tc.id,
                            "name": tc.function.name if tc.function else "",
                            "arguments": tc.function.arguments if tc.function else "",
                        }
                    else:
                        if tc.index in tool_calls_map:
                            tool_calls_map[tc.index]["arguments"] += tc.function.arguments if tc.function else ""

            if chunk.choices[0].finish_reason:
                finish_reason = chunk.choices[0].finish_reason

        tool_calls = None
        if tool_calls_map:
            tool_calls = [
                ToolCall(
                    id=tc["id"],
                    name=tc["name"],
                    arguments=json.loads(tc["arguments"]),
                )
                for tc in sorted(tool_calls_map.values(), key=lambda x: x.get("id", ""))
            ]

        return LLMResponse(
            content="".join(content_parts) if content_parts else None,
            tool_calls=tool_calls,
            finish_reason=finish_reason,
            model=model,
        )
