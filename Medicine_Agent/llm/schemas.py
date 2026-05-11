from __future__ import annotations

from typing import List, Optional, Any
from pydantic import BaseModel, Field


class ToolCall(BaseModel):
    """Represents a tool call requested by the LLM."""
    id: str = Field(default="", description="Tool call ID")
    name: str = Field(..., description="Tool name")
    arguments: dict[str, Any] = Field(default_factory=dict, description="Tool arguments")


class Message(BaseModel):
    """A message in the conversation."""
    role: str = Field(..., description="Message role: system, user, assistant, tool")
    content: Optional[str] = Field(default=None, description="Message content")
    tool_calls: Optional[List[ToolCall]] = Field(default=None, description="Tool calls from assistant")
    tool_call_id: Optional[str] = Field(default=None, description="Tool call ID being responded to")
    name: Optional[str] = Field(default=None, description="Tool name for tool messages")

    @classmethod
    def system(cls, content: str) -> "Message":
        return cls(role="system", content=content)

    @classmethod
    def user(cls, content: str) -> "Message":
        return cls(role="user", content=content)

    @classmethod
    def assistant(cls, content: Optional[str] = None, tool_calls: Optional[List[ToolCall]] = None) -> "Message":
        return cls(role="assistant", content=content, tool_calls=tool_calls)

    @classmethod
    def tool(cls, content: str, tool_call_id: str, name: str) -> "Message":
        return cls(role="tool", content=content, tool_call_id=tool_call_id, name=name)

    def to_openai_dict(self) -> dict:
        """Convert to OpenAI API format."""
        result: dict = {"role": self.role}
        if self.content is not None:
            result["content"] = self.content
        if self.tool_calls:
            result["tool_calls"] = [
                {
                    "id": tc.id,
                    "type": "function",
                    "function": {
                        "name": tc.name,
                        "arguments": tc.arguments,
                    },
                }
                for tc in self.tool_calls
            ]
        if self.tool_call_id:
            result["tool_call_id"] = self.tool_call_id
        if self.name:
            result["name"] = self.name
        return result


class LLMResponse(BaseModel):
    """Response from LLM."""
    content: Optional[str] = Field(default=None, description="Response text content")
    tool_calls: Optional[List[ToolCall]] = Field(default=None, description="Tool calls if any")
    finish_reason: str = Field(default="stop", description="Reason for finishing")
    model: str = Field(default="", description="Model used")
    usage: Optional[dict] = Field(default=None, description="Token usage statistics")
