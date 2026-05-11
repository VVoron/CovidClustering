#!/usr/bin/env python3
"""Medicine Agent - Medical research agent with LLM and browser search."""

import asyncio
import logging
import sys
from pathlib import Path

import uvicorn

from api.server import create_app
from config.settings import load_config


def setup_logging(level: str = "info") -> None:
    """Configure logging.

    Args:
        level: Logging level name
    """
    logging.basicConfig(
        level=getattr(logging, level.upper(), logging.INFO),
        format="%(asctime)s [%(levelname)s] %(name)s: %(message)s",
        datefmt="%Y-%m-%d %H:%M:%S",
        stream=sys.stdout,
    )


def main() -> None:
    """Main entry point."""
    # Load configuration
    config_path = Path(__file__).parent / "config" / "config.yaml"
    config = load_config(config_path)

    # Setup logging
    setup_logging(config.server.log_level)

    logger = logging.getLogger(__name__)
    logger.info(f"Starting Medicine Agent v0.1.0")
    logger.info(f"LLM endpoint: {config.llm.base_url}")
    logger.info(f"LLM model: {config.llm.model}")
    logger.info(f"Allowed domains: {config.search.allowed_domains}")
    logger.info(f"Server: {config.server.host}:{config.server.port}")

    # Create and run FastAPI app
    app = create_app()

    uvicorn.run(
        app,
        host=config.server.host,
        port=config.server.port,
        log_level=config.server.log_level,
    )


if __name__ == "__main__":
    main()
