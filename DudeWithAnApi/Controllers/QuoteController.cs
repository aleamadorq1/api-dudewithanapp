using Microsoft.AspNetCore.Mvc;
using DudeWithAnApi.Models;
using Microsoft.Extensions.Caching.Memory;
using DudeWithAnApi.ResponseDOs;
using DudeWithAnApi.Services;
using Microsoft.AspNetCore.Authorization;
using DudeWithAnApi.Repositories;

namespace DudeWithAnApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly IRepository<Quote> _quoteRepository;
        private readonly IQuoteService _quoteService;


        public QuoteController(IRepository<Quote> quoteRepository, IQuoteService quoteService)
        {
            _quoteRepository = quoteRepository;
            _quoteService = quoteService;
        }

        [HttpGet]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Quote>>> GetQuotes()
        {
            var quotes = await _quoteService.GetQuotesAsync();
            return Ok(quotes);
        }

        [HttpGet("published")]
        public async Task<ActionResult<IEnumerable<Quote>>> GetQuotesPublished(string? language)
        {
            var quotes = await _quoteService.GetQuotesPublishedAsync(language is null ? "" : language);
            return Ok(quotes);
        }

        // GET: api/Quote/5
        [HttpGet("{id}/translated")]
        public async Task<ActionResult<Quote>> GetTranslatedQuote(int id, string? language)
        {
            var quote = await _quoteService.GetQuoteTranslatedAsync(id, language is null? "": language);

            if (quote == null)
            {
                return NotFound();
            }

            return Ok(quote);
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
        public async Task<ActionResult<QuoteDO>> GetLatest(string? language)
        {
            var quote = await _quoteService.GetLatest(language is null ? "" : language);

            if (quote == null)
            {
                return NotFound();
            }

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
        public async Task<ActionResult<Quote>> UpdateQuote(int id, Quote quote)
        {
            if (id != quote.Id)
            {
                return BadRequest();
            }

            var newQuote = await _quoteService.UpdateQuoteAsync(quote);
            return Ok(newQuote);
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
