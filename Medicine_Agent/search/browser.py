import logging
import asyncio
from typing import Optional, List
from urllib.parse import urlparse

from playwright.async_api import async_playwright, Browser, Page, Playwright

from config.settings import SearchConfig

logger = logging.getLogger(__name__)


class BrowserManager:
    """Manages Playwright browser lifecycle."""

    def __init__(self, config: SearchConfig):
        self.config = config
        self._playwright: Optional[Playwright] = None
        self._browser: Optional[Browser] = None

    async def start(self) -> None:
        """Start the browser."""
        if self._browser:
            return
        self._playwright = await async_playwright().start()
        self._browser = await self._playwright.chromium.launch(
            headless=self.config.headless,
        )
        logger.info("Browser started")

    async def stop(self) -> None:
        """Stop the browser."""
        if self._browser:
            await self._browser.close()
            self._browser = None
        if self._playwright:
            await self._playwright.stop()
            self._playwright = None
        logger.info("Browser stopped")

    async def new_page(self) -> Page:
        """Create a new browser page."""
        if not self._browser:
            await self.start()
        context = await self._browser.new_context(
            user_agent=(
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) "
                "AppleWebKit/537.36 (KHTML, like Gecko) "
                "Chrome/120.0.0.0 Safari/537.36"
            ),
            viewport={"width": 1920, "height": 1080},
        )
        page = await context.new_page()
        page.set_default_timeout(self.config.timeout_ms)
        return page

    def is_domain_allowed(self, url: str) -> bool:
        """Check if URL domain is in allowed domains list.

        Args:
            url: URL to check

        Returns:
            True if domain is allowed
        """
        parsed = urlparse(url)
        domain = parsed.netloc.lower()

        # Remove www. prefix for comparison
        if domain.startswith("www."):
            domain = domain[4:]

        for allowed in self.config.allowed_domains:
            allowed_clean = allowed.lower()
            if allowed_clean.startswith("www."):
                allowed_clean = allowed_clean[4:]
            if domain == allowed_clean or domain.endswith("." + allowed_clean):
                return True

        return False

    def filter_allowed_urls(self, urls: List[str]) -> List[str]:
        """Filter URLs to only those with allowed domains.

        Args:
            urls: List of URLs to filter

        Returns:
            Filtered list of URLs
        """
        return [url for url in urls if self.is_domain_allowed(url)]
