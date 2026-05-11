import logging
import re
from typing import List, Optional
from urllib.parse import quote, urlparse

from playwright.async_api import Page

from .browser import BrowserManager

logger = logging.getLogger(__name__)


class SearchResult:
    """Represents a single search result."""
    def __init__(self, title: str, url: str, snippet: str):
        self.title = title
        self.url = url
        self.snippet = snippet

    def __repr__(self) -> str:
        return f"SearchResult(title={self.title!r}, url={self.url!r})"

    def to_text(self) -> str:
        """Format result as text for LLM."""
        return f"Title: {self.title}\nURL: {self.url}\nSnippet: {self.snippet}\n"


class ScholarSearch:
    """PubMed search via Playwright.
    
    Google Scholar is unreliable due to CAPTCHA and traffic checks,
    so we use PubMed directly as the primary search engine.
    """

    PUBMED_URL = "https://pubmed.ncbi.nlm.nih.gov/?term={query}&size=10"

    def __init__(self, browser_manager: BrowserManager):
        self.browser = browser_manager

    async def search(self, query: str, max_results: Optional[int] = None) -> List[SearchResult]:
        """Search PubMed and return results.

        Args:
            query: Search query
            max_results: Maximum number of results (default from config)

        Returns:
            List of SearchResult
        """
        if max_results is None:
            max_results = self.browser.config.max_results

        logger.info(f"=== SEARCHING PubMed for: '{query}' ===")
        results = await self._search_pubmed(query, max_results)
        logger.info(f"PubMed returned {len(results)} results")
        return results

    async def _search_pubmed(self, query: str, max_results: int) -> List[SearchResult]:
        """Search PubMed."""
        url = self.PUBMED_URL.format(query=quote(query))
        logger.info(f"Navigating to PubMed URL: {url}")

        page = await self.browser.new_page()
        try:
            await page.goto(url, wait_until="domcontentloaded")
            await page.wait_for_timeout(3000)

            # Debug: log page title
            logger.info(f"PubMed page title: {await page.title()}")
            logger.info(f"PubMed page URL: {page.url}")

            results = await self._extract_pubmed_results(page, max_results)
            logger.info(f"PubMed: extracted {len(results)} results")
            return results[:max_results]

        except Exception as e:
            logger.warning(f"PubMed search error: {e}", exc_info=True)
            return []
        finally:
            await page.close()

    async def _extract_pubmed_results(self, page: Page, max_results: int) -> List[SearchResult]:
        """Extract search results from PubMed page."""
        results: List[SearchResult] = []

        # PubMed result selectors - expanded list for various PubMed layouts
        selectors = [
            "article.full-docsum",
            "div.docsum-wrap",
            "div.lvl1",
            ".results-list article",
            "div.rslt",
            "div[class*='docsum']",
            "div[class*='result']",
            ".docsum-item",
        ]

        elements = []
        for selector in selectors:
            try:
                elements = await page.query_selector_all(selector)
                if elements:
                    logger.debug(f"PubMed: found {len(elements)} elements with selector: {selector}")
                    break
            except Exception:
                continue

        if not elements:
            logger.warning("No PubMed result elements found with any selector")
            try:
                body_html = await page.inner_html("body")
                logger.debug(f"PubMed body HTML (first 2000 chars): {body_html[:2000]}")
            except Exception:
                pass
            return results

        for element in elements[:max_results]:
            try:
                result = await self._extract_single_pubmed_result(element)
                if result:
                    results.append(result)
            except Exception as e:
                logger.debug(f"Failed to extract PubMed result: {e}")
                continue

        return results

    async def _extract_single_pubmed_result(self, element) -> Optional[SearchResult]:
        """Extract a single search result from a PubMed element."""
        # Title link - try multiple selectors
        title_el = await element.query_selector(
            "a.docsum-title, a.lvl1, a[ref='article'], "
            "a[class*='title'], a[href*='/pubmed/']"
        )
        if not title_el:
            return None

        title = await title_el.inner_text()
        href = await title_el.get_attribute("href")
        url = ""
        if href:
            if href.startswith("/"):
                url = f"https://pubmed.ncbi.nlm.nih.gov{href}"
            else:
                url = href

        # Snippet / abstract
        snippet_el = await element.query_selector(
            ".full-view-snippet, .lvl2, .snippet, "
            "div[class*='snippet'], div[class*='abstract']"
        )
        snippet = ""
        if snippet_el:
            snippet = await snippet_el.inner_text()

        if title:
            return SearchResult(
                title=title.strip(),
                url=url,
                snippet=snippet.strip() if snippet else "",
            )
        return None

    async def visit_page(self, url: str) -> str:
        """Visit a specific URL and extract text content.

        Args:
            url: URL to visit

        Returns:
            Page text content
        """
        if not self.browser.is_domain_allowed(url):
            logger.warning(f"Domain not allowed: {url}")
            return f"Error: Domain {urlparse(url).netloc} is not in the allowed domains list."

        logger.info(f"Visiting page: {url}")
        page = await self.browser.new_page()
        try:
            await page.goto(url, wait_until="networkidle", timeout=self.browser.config.timeout_ms)
            await page.wait_for_timeout(2000)

            # Extract main content
            content = await page.evaluate("""
                () => {
                    // Try to get main content
                    const selectors = [
                        'main', 'article', '.content', '#content', '.article',
                        '.full-view', '.abstract-content', '#abstract',
                        '.article-detail', '.pmc-article',
                        '.abstract', '.article-page',
                    ];
                    for (const sel of selectors) {
                        const el = document.querySelector(sel);
                        if (el && el.innerText.trim().length > 200) {
                            return el.innerText.slice(0, 15000);
                        }
                    }
                    // Fallback to body
                    const body = document.body;
                    return body.innerText.slice(0, 15000);
                }
            """)

            return content or "No content extracted"
        except Exception as e:
            logger.error(f"Failed to visit page {url}: {e}")
            return f"Error visiting page: {e}"
        finally:
            await page.close()
