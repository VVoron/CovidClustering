import json
import logging
import re
from typing import List, Optional, Callable, Awaitable, Dict, Any

from llm.client import LLMClient
from llm.schemas import Message, LLMResponse, ToolCall
from search.scholar import ScholarSearch, SearchResult
from config.settings import AgentConfig
from .prompts import SYSTEM_PROMPT

logger = logging.getLogger(__name__)


class ReActAgent:
    """ReAct (Reasoning + Acting) agent for medical research."""

    def __init__(
        self,
        llm_client: LLMClient,
        scholar_search: ScholarSearch,
        config: AgentConfig,
    ):
        self.llm = llm_client
        self.scholar = scholar_search
        self.config = config

        # Register tools with LLM
        self.llm.clear_tools()
        self.llm.register_tool(
            name="search_web",
            description="Search PubMed (MEDLINE database) for scientific articles. Use this to find medical research papers.",
            parameters={
                "type": "object",
                "properties": {
                    "query": {
                        "type": "string",
                        "description": "Search query for Google Scholar",
                    }
                },
                "required": ["query"],
            },
        )
        self.llm.register_tool(
            name="open_url",
            description="Open a specific URL (e.g., PubMed article, full text) and extract its text content. Use this to read the full content of a scientific article found via search_web.",
            parameters={
                "type": "object",
                "properties": {
                    "url": {
                        "type": "string",
                        "description": "Full URL to open (e.g., https://pubmed.ncbi.nlm.nih.gov/12345678/)",
                    }
                },
                "required": ["url"],
            },
        )

    async def process_message(
        self,
        message: str,
        conversation_history: Optional[List[Message]] = None,
        use_system_prompt: bool = True,
        on_thought: Optional[Callable[[str], None]] = None,
        on_tool_call: Optional[Callable[[str, str], None]] = None,
        on_tool_result: Optional[Callable[[str], None]] = None,
    ) -> str:
        """Process a user message through the ReAct loop.

        Args:
            message: User message
            conversation_history: Previous conversation messages
            use_system_prompt: If True, prepend the system prompt.
                Set to False when the message already contains all instructions
                (e.g., prompts from .NET client with CSV data).
            on_thought: Callback for each thought step
            on_tool_call: Callback for tool calls (name, args)
            on_tool_result: Callback for tool results

        Returns:
            Final answer text
        """
        messages: List[Message] = []

        # Add system prompt (only if requested)
        if use_system_prompt:
            messages.append(Message.system(SYSTEM_PROMPT))

        # Add conversation history
        if conversation_history:
            messages.extend(conversation_history)

        # Add user message
        messages.append(Message.user(message))

        # Track whether tools have been called at least once
        tools_called = False

        for iteration in range(self.config.max_iterations):
            logger.info(f"ReAct iteration {iteration + 1}/{self.config.max_iterations}")

            # Get LLM response
            response = await self.llm.chat(messages)

            # Extract thought for callbacks
            thought_text = ""
            if response.content:
                thought_match = re.search(r"Thought:\s*(.*?)(?=\nAction:|\nFinal Answer:|$)", response.content, re.DOTALL)
                if thought_match:
                    thought_text = thought_match.group(1).strip()

            # Build a compact version of assistant content for history
            history_content = None
            if response.content:
                if "Final Answer:" in response.content:
                    history_content = self._extract_final_answer(response.content)
                elif response.tool_calls:
                    history_content = response.content
                else:
                    history_content = response.content

            # Add assistant message to history
            messages.append(Message.assistant(
                content=history_content,
                tool_calls=response.tool_calls,
            ))

            # Check for final answer
            if response.content and "Final Answer:" in response.content:
                # If no tools were called, force the model to use tools
                if not tools_called:
                    logger.warning("Model produced final answer without calling any tools! Forcing tool use.")
                    # Remove the last assistant message (the premature final answer)
                    messages.pop()
                    # Add a reminder to use tools
                    messages.append(Message.user(
                        "Ты не использовал инструменты поиска! Используй search_web для поиска "
                        "релевантных статей (достаточно 1-2 запросов), затем открой найденные "
                        "статьи через open_url. Только после этого формируй Final Answer."
                    ))
                    continue

                final_answer = self._extract_final_answer(response.content)
                logger.info("Agent produced final answer")
                return final_answer

            if response.tool_calls:
                tools_called = True
                for tool_call in response.tool_calls:
                    # Pass only the thought (without Action) to callback
                    if on_thought and thought_text:
                        on_thought(thought_text)
                    elif on_thought and response.content:
                        on_thought(response.content[:200])

                    tool_name = tool_call.name
                    tool_args = tool_call.arguments

                    if on_tool_call:
                        on_tool_call(tool_name, json.dumps(tool_args, ensure_ascii=False))

                    logger.info(f"Tool call: {tool_name}({tool_args})")

                    if tool_name == "search_web":
                        query = self._extract_query(tool_args)
                        result = await self._tool_search_web(query)
                    elif tool_name == "open_url":
                        url = self._extract_url(tool_args)
                        result = await self._tool_open_url(url)
                    else:
                        result = f"Error: Unknown tool '{tool_name}'"

                    if on_tool_result:
                        on_tool_result(result)

                    # Add tool result to messages
                    messages.append(Message.tool(
                        content=result,
                        tool_call_id=tool_call.id,
                        name=tool_name,
                    ))
            else:
                # No tool calls and no final answer
                if response.content:
                    # Check if the response contains made-up URLs (not from search results)
                    urls = re.findall(r'https?://[^\s]+', response.content)
                    if urls and not tools_called:
                        logger.warning(f"Model generated URLs without calling search_web: {urls}")
                        # Remove the last assistant message
                        messages.pop()
                        # Force tool use
                        messages.append(Message.user(
                            "Ты сгенерировал URL самостоятельно, не используя search_web! "
                            "Запрещено выдумывать URL. Используй search_web для поиска статей "
                            "(достаточно 1-2 запросов), затем open_url для открытия найденных URL."
                        ))
                        continue
                    return response.content
                else:
                    return "Не удалось получить ответ. Пожалуйста, попробуйте переформулировать вопрос."

        # Max iterations reached
        logger.warning(f"Max iterations ({self.config.max_iterations}) reached")
        return (
            f"Достигнут лимит итераций ({self.config.max_iterations}). "
            f"Пожалуйста, уточните вопрос или попробуйте разбить его на части."
        )

    def _extract_query(self, tool_args: dict) -> str:
        """Extract query from tool arguments, handling deeply nested format.

        DeepSeek sometimes generates nested structures like:
        {arguments: {arguments: {query: ...}}} or {query: ...}
        This method recursively searches for the first 'query' key.
        """
        return self._deep_find(tool_args, "query")

    def _extract_url(self, tool_args: dict) -> str:
        """Extract URL from tool arguments, handling deeply nested format.

        DeepSeek sometimes generates nested structures like:
        {arguments: {arguments: {url: ...}}} or {url: ...}
        This method recursively searches for the first 'url' key.
        """
        return self._deep_find(tool_args, "url")

    def _deep_find(self, d: dict, key: str) -> str:
        """Recursively search for a key in a nested dict structure.

        DeepSeek sometimes wraps arguments in multiple {arguments: ...} layers.
        This method drills down through 'arguments' keys until it finds the target key.

        Args:
            d: Dictionary to search
            key: Key to find (e.g., 'query' or 'url')

        Returns:
            Value of the found key, or empty string if not found
        """
        if not isinstance(d, dict):
            return ""

        # Direct match
        if key in d and isinstance(d[key], str):
            return d[key]

        # Drill through 'arguments' wrappers
        inner = d.get("arguments", {})
        if isinstance(inner, dict):
            # Direct match in inner
            if key in inner and isinstance(inner[key], str):
                return inner[key]
            # Recursively drill deeper
            return self._deep_find(inner, key)

        return ""

    async def _tool_search_web(self, query: str) -> str:
        """Execute search_web tool.

        Args:
            query: Search query

        Returns:
            Formatted search results
        """
        if not query:
            logger.warning("search_web called with empty query!")
            return "Ошибка: поисковый запрос пуст. Пожалуйста, укажите запрос."

        logger.info(f"Executing search_web with query: '{query}'")
        try:
            results = await self.scholar.search(query)
            if not results:
                return "Поиск не дал результатов. Попробуйте изменить запрос."

            formatted = f"Результаты поиска по запросу '{query}':\n\n"
            for i, result in enumerate(results, 1):
                formatted += f"[{i}] {result.to_text()}\n"

            return formatted
        except Exception as e:
            logger.error(f"Search error: {e}")
            return f"Ошибка при поиске: {e}"

    async def _tool_open_url(self, url: str) -> str:
        """Execute open_url tool.

        Args:
            url: URL to visit

        Returns:
            Page text content
        """
        if not url:
            return "Ошибка: URL не указан."
        try:
            content = await self.scholar.visit_page(url)
            return content
        except Exception as e:
            logger.error(f"Open URL error: {e}")
            return f"Ошибка при открытии URL: {e}"

    def _extract_final_answer(self, content: str) -> str:
        """Extract final answer from LLM response.

        Args:
            content: Full response content

        Returns:
            Final answer text without Thought/Action prefixes
        """
        # Try to find Final Answer section
        match = re.search(
            r"Final Answer:\s*(.*?)$",
            content,
            re.DOTALL,
        )
        if match:
            result = match.group(1).strip()
            # Remove any remaining "Thought:" or "Action:" lines from the result
            result = re.sub(r"^(?:Thought|Action):.*$", "", result, flags=re.MULTILINE).strip()
            return result

        # Fallback: return everything after last "Thought:"
        parts = content.split("Thought:")
        if len(parts) > 1:
            result = parts[-1].strip()
            result = re.sub(r"^(?:Thought|Action):.*$", "", result, flags=re.MULTILINE).strip()
            return result

        return content.strip()
