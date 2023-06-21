using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DudeWithAnApi.Models;
using Microsoft.Extensions.Caching.Memory;
using DudeWithAnApi.ResponseDOs;
using Microsoft.AspNetCore.Authorization;
using DudeWithAnApi.Services;
using DudeWithAnApi.Repositories;

namespace DudeWithAnApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class QuoteTranslationController : ControllerBase
    {
        private readonly IQuoteTranslationService _quoteTranslationService;
        private readonly IRepository<QuoteTranslation> _quoteTranslationRepository;

        public QuoteTranslationController(IRepository<QuoteTranslation> quoteTranslationRepository, IQuoteTranslationService quoteTranslationService)
        {
            _quoteTranslationService = quoteTranslationService;
            _quoteTranslationRepository = quoteTranslationRepository;
        }

        [HttpGet]
        [HttpGet("getByQuoteId")]
        public async Task<ActionResult<IEnumerable<QuoteTranslation>>> GetTranslationsByQuote(int quoteId)
        {
            var translations = await _quoteTranslationService.GetByQuoteId(quoteId);
            return Ok(translations);
        }

        // POST: api/Quote
        [HttpPost]
        public async Task<ActionResult<Quote>> CreateTranslation(QuoteTranslation translation)
        {
            translation.IsDeleted = 0;
            await _quoteTranslationRepository.AddAsync(translation);
            return Ok();
        }

    }
}
