using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DudeWithAnApi.Models;
using DudeWithAnApi.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using DudeWithAnApi.ResponseDOs;
using DudeWithAnApi.Services;

namespace DudeWithAnApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly IRepository<Quote> _quoteRepository;
        private readonly IQuoteService _quoteService;
        private readonly IQuotePrintService _quotePrintService;
        private readonly IMemoryCache _memoryCache;

        public QuoteController(IRepository<Quote> quoteRepository, IQuoteService quoteService, IQuotePrintService quotePrintService, IMemoryCache memoryCache)
        {
            _quoteRepository = quoteRepository;
            _quoteService = quoteService;
            _quotePrintService = quotePrintService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quote>>> GetQuotes()
        {
            var quotes = await _quoteService.GetQuotesAsync();
            return Ok(quotes);
        }

        // GET: api/Quote/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quote>> GetQuote(int id)
        {
            var quote = await _quoteRepository.GetByIdAsync(id);

            if (quote == null)
            {
                return NotFound();
            }

            return Ok(quote);
        }

        // GET: api/Quote/latest
        [HttpGet("latest")]
        public async Task<ActionResult<Quote>> GetLatest()
        {
            const string cacheKey = "latest_quote";
            if (!_memoryCache.TryGetValue(cacheKey, out Quote quote))
            {
                quote = await _quoteService.GetLatest();

                if (quote == null)
                {
                    return NotFound();
                }

                // Set cache options
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromHours(6)); // Adjust the cache duration as needed

                // Save the quote in the cache
                _memoryCache.Set(cacheKey, quote, cacheOptions);
            }
            await _quotePrintService.AddPrint(quote);
            return Ok(quote);
        }

        // POST: api/Quote
        [HttpPost]
        public async Task<ActionResult<Quote>> CreateQuote(Quote quote)
        {
            quote.IsDeleted = 0;
            quote.CreationDate = DateTime.UtcNow;
            await _quoteRepository.AddAsync(quote);
            return CreatedAtAction("GetQuote", new { id = quote.Id }, quote);
        }

        // PUT: api/Quote/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuote(int id, Quote quote)
        {
            if (id != quote.Id)
            {
                return BadRequest();
            }

            await _quoteService.UpdateQuoteAsync(quote);

            return NoContent();
        }

        // DELETE: api/Quote/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            var quote = await _quoteRepository.GetByIdAsync(id);
            if (quote == null)
            {
                return NotFound();
            }

            await _quoteService.DeleteQuoteAsync(quote.Id);

            return NoContent();
        }


    }
}
