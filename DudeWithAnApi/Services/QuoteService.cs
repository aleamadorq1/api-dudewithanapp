using DudeWithAnApi.Models;
using DudeWithAnApi.Repositories;

namespace DudeWithAnApi.Services
{
    public interface IQuoteService
    {
        Task<Quote> GetLatest(string language);
        Task<IEnumerable<Quote>> GetQuotesAsync();
        Task DeleteQuoteAsync(int id);
        Task<Quote> UpdateQuoteAsync(Quote quote);
        Task ToggleQuoteAsync(int id);
        Task<IEnumerable<Quote>> GetQuotesPublishedAsync(string language);
        Task<Quote> GetQuoteTranslatedAsync(int id, string language);
        Task<IEnumerable<QuoteTranslation>> GetTranslationsByQuoteId(int quoteId);
    }

    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IQuoteTranslationRepository _quoteTransRepository;

        public QuoteService(IQuoteRepository userRepository, IQuoteTranslationRepository translationRepository)
        {
            _quoteRepository = userRepository;
            _quoteTransRepository = translationRepository;
        }

        public async Task<Quote> GetLatest(string language)
        {
            var quote = await _quoteRepository.GetLatestAsync();
            var translated = await GetQuoteTranslatedAsync(quote.Id, language);
            return translated;
        }

        public async Task<IEnumerable<Quote>> GetQuotesAsync()
        {
            return await _quoteRepository.GetQuotesAsync();
        }

        public async Task<IEnumerable<Quote>> GetQuotesPublishedAsync(string language)
        {
            IEnumerable<Quote> quotes = await _quoteRepository.GetQuotesPublishedAsync();
            if (language != "")
            {
                foreach (Quote quote in quotes)
                {
                    QuoteTranslation quoteTranslation = await _quoteTransRepository.GetByQuoteAndLanguageAsync(quote.Id, language);
                    if (quoteTranslation is not null)
                    {
                        quote.QuoteText = quoteTranslation.PrimaryText;
                        quote.SecondaryText = quoteTranslation.SecondaryText;
                    }
                }
            }
            return quotes;
        }

        public async Task<Quote> GetQuoteTranslatedAsync(int id, string language)
        {
            Quote quote = await _quoteRepository.GetByIdAsync(id);
            if (language != "")
            {
                QuoteTranslation quoteTranslation = await _quoteTransRepository.GetByQuoteAndLanguageAsync(quote.Id, language);
                if (quoteTranslation is not null)
                {
                    quote.QuoteText = quoteTranslation.PrimaryText;
                    quote.SecondaryText = quoteTranslation.SecondaryText;
                }
            }
            return quote;
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
                _quoteTransRepository.MoveCsvTranslationsAsync(quote.Id, newQuote.Id);
            }
            
            return newQuote; 
        }

        public Task<IEnumerable<QuoteTranslation>> GetTranslationsByQuoteId(int quoteId)
        {
            return _quoteTransRepository.FindAsync(t => t.QuoteId == quoteId);
        }
    }
}

