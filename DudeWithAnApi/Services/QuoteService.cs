using DudeWithAnApi.Models;
using DudeWithAnApi.Repositories;

namespace DudeWithAnApi.Services
{
    public interface IQuoteService
    {
        Task<Quote> GetLatest();
        Task<IEnumerable<Quote>> GetQuotesAsync();
        Task DeleteQuoteAsync(int id);
        Task<Quote> UpdateQuoteAsync(Quote quote);
        Task ToggleQuoteAsync(int id);
        Task<IEnumerable<Quote>> GetQuotesPublishedAsync();
        Task<IEnumerable<QuoteTranslation>> GetTranslationsByQuoteId(int quoteId);
    }

    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IRepository<QuoteTranslation> _quoteTranslationRepository;

        public QuoteService(IQuoteRepository userRepository, IRepository<QuoteTranslation> quoteTranslationRepository)
        {
            _quoteRepository = userRepository;
            _quoteTranslationRepository = quoteTranslationRepository;
        }

        public Task<Quote> GetLatest()
        {
            var quote = _quoteRepository.GetLatestAsync();
            return quote;
        }

        public async Task<IEnumerable<Quote>> GetQuotesAsync()
        {
            return await _quoteRepository.GetQuotesAsync();
        }

        public async Task<IEnumerable<Quote>> GetQuotesPublishedAsync()
        {
            return await _quoteRepository.GetQuotesPublishedAsync();
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
            return await _quoteRepository.UpdateQuoteAsync(quote);
        }

        public Task<IEnumerable<QuoteTranslation>> GetTranslationsByQuoteId(int quoteId)
        {
            return _quoteTranslationRepository.FindAsync(t => t.QuoteId == quoteId);
        }
    }
}

