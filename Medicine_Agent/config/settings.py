import os
import yaml
from pathlib import Path
from typing import List, Optional
from pydantic import BaseModel, Field


class LLMConfig(BaseModel):
    """LLM connection configuration."""
    api_key: str = Field(default="", description="API key for OpenAI-compatible service")
    base_url: str = Field(default="https://api.openai.com/v1", description="OpenAI-compatible endpoint URL")
    model: str = Field(default="gpt-4o-mini", description="Model name")
    temperature: float = Field(default=0.7, ge=0.0, le=2.0, description="Sampling temperature")
    max_tokens: int = Field(default=4096, ge=1, description="Maximum tokens in response")
    request_timeout: int = Field(default=120, ge=1, description="Timeout for LLM API request in seconds")


class SearchConfig(BaseModel):
    """Browser search configuration."""
    allowed_domains: List[str] = Field(
        default=[
            "pubmed.ncbi.nlm.nih.gov",
            "ncbi.nlm.nih.gov",
            "doi.org",
            "pmc.ncbi.nlm.nih.gov",
        ],
        description="List of allowed domains for browser search"
    )
    headless: bool = Field(default=True, description="Run browser in headless mode")
    timeout_ms: int = Field(default=30000, ge=1000, description="Browser timeout in milliseconds")
    max_results: int = Field(default=3, ge=1, le=50, description="Maximum search results to return")


class ServerConfig(BaseModel):
    """HTTP server configuration."""
    host: str = Field(default="127.0.0.1", description="Server host")
    port: int = Field(default=8000, ge=1024, le=65535, description="Server port")
    log_level: str = Field(default="info", description="Logging level")
    request_timeout: int = Field(default=120, ge=1, description="Request timeout in seconds")


class AgentConfig(BaseModel):
    """Agent behavior configuration."""
    max_iterations: int = Field(default=6, ge=1, le=100, description="Maximum ReAct iterations")
    max_tool_calls_per_step: int = Field(default=1, ge=1, le=10, description="Maximum tool calls per step")


class AppConfig(BaseModel):
    """Main application configuration."""
    llm: LLMConfig = Field(default_factory=LLMConfig)
    search: SearchConfig = Field(default_factory=SearchConfig)
    server: ServerConfig = Field(default_factory=ServerConfig)
    agent: AgentConfig = Field(default_factory=AgentConfig)

    @classmethod
    def from_yaml(cls, path: Path) -> "AppConfig":
        """Load configuration from YAML file."""
        if not path.exists():
            return cls()
        with open(path, "r", encoding="utf-8") as f:
            data = yaml.safe_load(f)
        return cls(**data) if data else cls()

    def to_yaml(self, path: Path) -> None:
        """Save configuration to YAML file."""
        path.parent.mkdir(parents=True, exist_ok=True)
        with open(path, "w", encoding="utf-8") as f:
            yaml.dump(self.model_dump(), f, default_flow_style=False, allow_unicode=True)

    def resolve_api_key(self) -> str:
        """Get API key from config or environment variable."""
        if self.llm.api_key:
            return self.llm.api_key
        env_key = os.environ.get("MEDICINE_AGENT_API_KEY")
        if env_key:
            return env_key
        return ""


# Global config instance
_config: Optional[AppConfig] = None


def load_config(path: Optional[Path] = None) -> AppConfig:
    """Load configuration from YAML file.
    
    Args:
        path: Path to config file. If None, uses default path.
    
    Returns:
        AppConfig instance
    """
    global _config
    if path is None:
        path = Path(__file__).parent / "config.yaml"
    _config = AppConfig.from_yaml(path)
    return _config


def save_config(config: AppConfig, path: Optional[Path] = None) -> None:
    """Save configuration to YAML file.
    
    Args:
        config: AppConfig instance to save
        path: Path to config file. If None, uses default path.
    """
    if path is None:
        path = Path(__file__).parent / "config.yaml"
    config.to_yaml(path)


def get_config() -> AppConfig:
    """Get the global configuration instance.
    
    Returns:
        AppConfig instance
    """
    global _config
    if _config is None:
        _config = load_config()
    return _config
