using DudeWithAnApi.Models;
using DudeWithAnApi.Repositories;
using DudeWithAnApi.ResponseDOs;

namespace DudeWithAnApi.Services
{
    public interface IQuoteService
    {
        Task<QuoteDO> GetLatest(string language);
        Task<IEnumerable<Quote>> GetQuotesAsync();
        Task DeleteQuoteAsync(int id);
        Task<Quote> UpdateQuoteAsync(Quote quote);
        Task ToggleQuoteAsync(int id);
        Task<List<QuoteDO>> GetQuotesPublishedAsync(string language);
        Task<QuoteDO> GetQuoteTranslatedAsync(int id, string language);
        Task<IEnumerable<QuoteTranslation>> GetTranslationsByQuoteId(int quoteId);
    }

    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IQuoteTranslationRepository _quoteTransRepository;
        private readonly IQuotePrintService _quotePrintService;

        public QuoteService(IQuoteRepository userRepository, IQuoteTranslationRepository translationRepository, IQuotePrintService quotePrintRService)
        {
            _quoteRepository = userRepository;
            _quoteTransRepository = translationRepository;
            _quotePrintService = quotePrintRService;
        }

        public async Task<QuoteDO> GetLatest(string language)
        {
            var quote = await _quoteRepository.GetLatestAsync();
            var translated = await GetQuoteTranslatedAsync(quote.Id, language);
            _quotePrintService.AddPrint(quote);
            return translated;
        }

        public async Task<IEnumerable<Quote>> GetQuotesAsync()
        {
            return await _quoteRepository.GetQuotesAsync();
        }

        public async Task<List<QuoteDO>> GetQuotesPublishedAsync(string language)
        {
            IEnumerable<Quote> quotes = await _quoteRepository.GetQuotesPublishedAsync();
            List<QuoteDO> responseArray = new List<QuoteDO>();
            if (language != "")
            {
                foreach (Quote quote in quotes)
                {
                    var response = HydrateResponse(quote);

                    QuoteTranslation quoteTranslation = await _quoteTransRepository.GetByQuoteAndLanguageAsync(response.Id, language);
                    if (quoteTranslation is not null)
                    {
                        response.QuoteText = quoteTranslation.PrimaryText;
                        response.SecondaryText = quoteTranslation.SecondaryText;
                    }
                    responseArray.Add(response);
                    _quotePrintService.AddPrint(quote);
                }
            }
            return responseArray;
        }

        public async Task<QuoteDO> GetQuoteTranslatedAsync(int id, string language)
        {
            Quote quote = await _quoteRepository.GetByIdAsync(id);
            QuoteDO response = HydrateResponse(quote);

            if (language != "")
            {
                QuoteTranslation quoteTranslation = await _quoteTransRepository.GetByQuoteAndLanguageAsync(id, language);
                if (quoteTranslation is not null)
                {
                    response.QuoteText = quoteTranslation.PrimaryText;
                    response.SecondaryText = quoteTranslation.SecondaryText;
                }
            }
            return response;
        }

        public async Task DeleteQuoteAsync(int id)
        {
            await _quoteRepository.DeleteQuoteAsync(id);
        }

        public async Task ToggleQuoteAsync(int id)
        {
            await _quoteRepository.ToggleQuoteAsync(id);
        }

        public async Task<Quote> UpdateQuoteAsync(Quote quote)
        {
            var newQuote = await _quoteRepository.UpdateQuoteAsync(quote);
            if (newQuote.IsCSV == 1)
            {
                await _quoteTransRepository.MoveCsvTranslationsAsync(quote.Id, newQuote.Id);
            }
            
            return newQuote; 
        }

        public Task<IEnumerable<QuoteTranslation>> GetTranslationsByQuoteId(int quoteId)
        {
            return _quoteTransRepository.FindAsync(t => t.QuoteId == quoteId);
        }

        private QuoteDO HydrateResponse(Quote quote)
        {
            QuoteDO quoteDO = new();
            quoteDO.Id = quote.Id;
            quoteDO.QuoteText = quote.QuoteText;
            quoteDO.SecondaryText = quote.SecondaryText;
            quoteDO.IsActive = quote.IsActive;
            quoteDO.Url = quote.Url;
            quoteDO.IsDeleted = quote.IsDeleted;
            quoteDO.CreationDate = quote.CreationDate;
            quoteDO.IsCSV = quote.IsCSV;
            return quoteDO;
        }
    }
}
