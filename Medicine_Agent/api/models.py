from __future__ import annotations

from typing import Optional, List
from pydantic import BaseModel, Field


class AnalyzeRequest(BaseModel):
    """Request model for analyze endpoint (used by .NET client)."""
    prompt: str = Field(..., description="Prompt with CSV data and analysis instructions")
    conversation_id: Optional[str] = Field(default=None, description="Conversation ID for history tracking")


class AnalyzeResponse(BaseModel):
    """Response model for analyze endpoint (used by .NET client)."""
    success: bool = Field(..., description="Whether the request was successful")
    response: Optional[str] = Field(default=None, description="Agent response text")
    error: Optional[str] = Field(default=None, description="Error message if request failed")


class ChatRequest(BaseModel):
    """Request model for chat endpoint."""
    message: str = Field(..., description="User message")
    conversation_id: Optional[str] = Field(default=None, description="Conversation ID for history tracking")


class ChatResponse(BaseModel):
    """Response model for chat endpoint."""
    response: str = Field(..., description="Agent response")
    conversation_id: str = Field(..., description="Conversation ID")
    iterations: int = Field(default=0, description="Number of ReAct iterations used")


class HealthResponse(BaseModel):
    """Response model for health check."""
    status: str = Field(default="ok", description="Service status")
    version: str = Field(default="0.1.0", description="Service version")


class ConfigUpdateRequest(BaseModel):
    """Request model for config update."""
    llm_api_key: Optional[str] = Field(default=None, description="LLM API key")
    llm_base_url: Optional[str] = Field(default=None, description="LLM base URL")
    llm_model: Optional[str] = Field(default=None, description="LLM model name")
    llm_temperature: Optional[float] = Field(default=None, ge=0.0, le=2.0, description="LLM temperature")
    allowed_domains: Optional[List[str]] = Field(default=None, description="Allowed search domains")
    max_iterations: Optional[int] = Field(default=None, ge=1, le=100, description="Max agent iterations")
