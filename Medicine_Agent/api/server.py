import asyncio
import logging
import uuid
from typing import Dict, Optional

from fastapi import FastAPI, HTTPException
from fastapi.middleware.cors import CORSMiddleware

from .models import AnalyzeRequest, AnalyzeResponse, ChatRequest, ChatResponse, HealthResponse, ConfigUpdateRequest
from agent.agent import ReActAgent
from config.settings import AppConfig, load_config, save_config, get_config

logger = logging.getLogger(__name__)


class ConversationStore:
    """Simple in-memory conversation store."""

    def __init__(self):
        self._store: Dict[str, list] = {}

    def get_or_create(self, conversation_id: Optional[str]) -> str:
        if conversation_id and conversation_id in self._store:
            return conversation_id
        new_id = conversation_id or str(uuid.uuid4())
        self._store[new_id] = []
        return new_id

    def get_history(self, conversation_id: str):
        return self._store.get(conversation_id, [])

    def add_message(self, conversation_id: str, role: str, content: str):
        if conversation_id not in self._store:
            self._store[conversation_id] = []
        from llm.schemas import Message
        self._store[conversation_id].append(Message(role=role, content=content))

    def clear(self, conversation_id: str):
        self._store.pop(conversation_id, None)


class AgentApp:
    """Application container for agent factory."""

    def __init__(self):
        self.config: Optional[AppConfig] = None
        self.conversations = ConversationStore()
        self._initialized = False

    async def initialize(self):
        """Load configuration."""
        if self._initialized:
            return

        self.config = get_config()
        self._initialized = True
        logger.info("App initialized successfully")

    async def create_agent(self) -> ReActAgent:
        """Create a new agent instance with isolated LLM client and browser.
        
        Each call creates a fresh agent with its own:
        - LLM client (separate OpenAI connection)
        - Browser manager (separate Playwright browser instance)
        - Scholar search instance
        
        This ensures parallel requests don't interfere with each other.
        """
        if not self.config:
            raise RuntimeError("App not initialized")

        # Resolve API key
        api_key = self.config.resolve_api_key()
        if not api_key:
            logger.warning(
                "No API key configured. Set MEDICINE_AGENT_API_KEY env var "
                "or configure llm.api_key in config.yaml"
            )

        # Create isolated LLM client
        from llm.client import LLMClient
        llm_client = LLMClient(self.config.llm, api_key)

        # Create isolated browser and search
        from search.browser import BrowserManager
        from search.scholar import ScholarSearch
        browser_manager = BrowserManager(self.config.search)
        await browser_manager.start()
        scholar_search = ScholarSearch(browser_manager)

        # Create agent
        agent = ReActAgent(llm_client, scholar_search, self.config.agent)
        return agent

    async def shutdown_agent(self, agent: ReActAgent):
        """Shutdown an agent's browser resources."""
        try:
            if agent.scholar and agent.scholar.browser:
                await agent.scholar.browser.stop()
        except Exception as e:
            logger.warning(f"Error shutting down agent browser: {e}")


# Global application instance
app_instance = AgentApp()


async def _run_agent_with_cleanup(
    agent: ReActAgent,
    message: str,
    conversation_history: list,
    use_system_prompt: bool,
    on_thought,
    on_tool_call,
    on_tool_result,
) -> str:
    """Run agent and ensure cleanup happens."""
    try:
        response = await agent.process_message(
            message=message,
            conversation_history=conversation_history,
            use_system_prompt=use_system_prompt,
            on_thought=on_thought,
            on_tool_call=on_tool_call,
            on_tool_result=on_tool_result,
        )
        return response
    finally:
        await app_instance.shutdown_agent(agent)


def create_app() -> FastAPI:
    """Create and configure FastAPI application."""
    app = FastAPI(
        title="Medicine Agent API",
        description="Medical research agent with LLM and browser search",
        version="0.1.0",
    )

    # CORS middleware for .NET client
    app.add_middleware(
        CORSMiddleware,
        allow_origins=["*"],
        allow_credentials=True,
        allow_methods=["*"],
        allow_headers=["*"],
    )

    @app.on_event("startup")
    async def startup():
        await app_instance.initialize()

    @app.on_event("shutdown")
    async def shutdown():
        pass

    @app.get("/api/health", response_model=HealthResponse)
    async def health_check():
        """Health check endpoint."""
        return HealthResponse(
            status="ok",
            version="0.1.0",
        )

    @app.post("/api/chat", response_model=ChatResponse)
    async def chat(request: ChatRequest):
        """Send a message to the agent."""
        if not app_instance.config:
            raise HTTPException(status_code=503, detail="App not initialized")

        conversation_id = app_instance.conversations.get_or_create(request.conversation_id)
        history = app_instance.conversations.get_history(conversation_id)

        # Track iterations
        iterations = [0]

        def on_thought(thought: str):
            logger.debug(f"Agent thought: {thought}")

        def on_tool_call(name: str, args: str):
            iterations[0] += 1
            logger.info(f"Agent tool call #{iterations[0]}: {name}({args})")

        def on_tool_result(result: str):
            logger.debug(f"Tool result (first 200 chars): {result[:200]}")

        # Create a fresh agent for this request
        agent = await app_instance.create_agent()

        try:
            response = await _run_agent_with_cleanup(
                agent=agent,
                message=request.message,
                conversation_history=history,
                use_system_prompt=True,
                on_thought=on_thought,
                on_tool_call=on_tool_call,
                on_tool_result=on_tool_result,
            )

            # Store in conversation history
            app_instance.conversations.add_message(conversation_id, "user", request.message)
            app_instance.conversations.add_message(conversation_id, "assistant", response)

            return ChatResponse(
                response=response,
                conversation_id=conversation_id,
                iterations=iterations[0],
            )
        except Exception as e:
            logger.error(f"Agent error: {e}", exc_info=True)
            raise HTTPException(status_code=500, detail=str(e))

    @app.get("/api/config")
    async def get_config_endpoint():
        """Get current configuration."""
        config = get_config()
        return config.model_dump(exclude={"llm": {"api_key"}})

    @app.put("/api/config")
    async def update_config_endpoint(request: ConfigUpdateRequest):
        """Update configuration at runtime."""
        config = get_config()

        if request.llm_api_key is not None:
            config.llm.api_key = request.llm_api_key
        if request.llm_base_url is not None:
            config.llm.base_url = request.llm_base_url
        if request.llm_model is not None:
            config.llm.model = request.llm_model
        if request.llm_temperature is not None:
            config.llm.temperature = request.llm_temperature
        if request.allowed_domains is not None:
            config.search.allowed_domains = request.allowed_domains
        if request.max_iterations is not None:
            config.agent.max_iterations = request.max_iterations

        save_config(config)
        logger.info("Configuration updated")

        return {"status": "ok", "message": "Configuration updated. Restart the server for changes to take full effect."}

    @app.post("/api/conversation/{conversation_id}/clear")
    async def clear_conversation(conversation_id: str):
        """Clear conversation history."""
        app_instance.conversations.clear(conversation_id)
        return {"status": "ok"}

    @app.post("/api/analyze", response_model=AnalyzeResponse)
    async def analyze(request: AnalyzeRequest):
        """
        Analyze endpoint for .NET client.
        
        Accepts a prompt with CSV data and analysis instructions,
        processes it through the agent with the system prompt (internet search),
        and returns the result.
        """
        if not app_instance.config:
            return AnalyzeResponse(
                success=False,
                error="App not initialized",
            )

        conversation_id = app_instance.conversations.get_or_create(request.conversation_id)
        history = app_instance.conversations.get_history(conversation_id)

        # Track iterations
        iterations = [0]

        def on_thought(thought: str):
            logger.debug(f"Agent thought: {thought}")

        def on_tool_call(name: str, args: str):
            iterations[0] += 1
            logger.info(f"Agent tool call #{iterations[0]}: {name}({args})")

        def on_tool_result(result: str):
            logger.debug(f"Tool result (first 200 chars): {result[:200]}")

        # Create a fresh agent for this request
        try:
            agent = await app_instance.create_agent()
        except Exception as e:
            logger.error(f"Failed to create agent: {e}", exc_info=True)
            return AnalyzeResponse(
                success=False,
                error=f"Failed to create agent: {e}",
            )

        timeout = app_instance.config.server.request_timeout if app_instance.config else 120
        try:
            response = await asyncio.wait_for(
                _run_agent_with_cleanup(
                    agent=agent,
                    message=request.prompt,
                    conversation_history=history,
                    use_system_prompt=True,
                    on_thought=on_thought,
                    on_tool_call=on_tool_call,
                    on_tool_result=on_tool_result,
                ),
                timeout=timeout,
            )

            # Store in conversation history
            app_instance.conversations.add_message(conversation_id, "user", request.prompt)
            app_instance.conversations.add_message(conversation_id, "assistant", response)

            return AnalyzeResponse(
                success=True,
                response=response,
            )
        except asyncio.TimeoutError:
            logger.error(f"Analyze request timed out after {timeout} seconds")
            await app_instance.shutdown_agent(agent)
            return AnalyzeResponse(
                success=False,
                error="Request timed out",
            )
        except Exception as e:
            logger.error(f"Analyze error: {e}", exc_info=True)
            await app_instance.shutdown_agent(agent)
            return AnalyzeResponse(
                success=False,
                error=str(e),
            )

    return app
