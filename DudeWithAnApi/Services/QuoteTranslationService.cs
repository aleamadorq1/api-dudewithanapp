using DudeWithAnApi.Models;
using DudeWithAnApi.Repositories;

namespace DudeWithAnApi.Services
{
    public interface IQuoteTranslationService
    {
        Task AddTranslation(QuoteTranslation trans);
        Task AddTranslationFile(IEnumerable<QuoteTranslation> trans);
        Task EditTranslation(QuoteTranslation trans);
        Task DeleteTranslation(int id);
        Task<IEnumerable<QuoteTranslation>> GetByQuoteId(int quoteId);
    }

    public class QuoteTranslationService : IQuoteTranslationService
    {
        private readonly IRepository<QuoteTranslation> _quoteTranslationRepository;

        public QuoteTranslationService(IRepository<QuoteTranslation> quoteTranslationRepository)
        {
            _quoteTranslationRepository = quoteTranslationRepository;
        }

        public async Task AddTranslation(QuoteTranslation trans)
        {
            await _quoteTranslationRepository.AddAsync(trans);
        }

        public async Task AddTranslationFile(IEnumerable<QuoteTranslation> trans)
        {
            await _quoteTranslationRepository.AddRangeAsync(trans);
        }

        public async Task EditTranslation(QuoteTranslation trans)
        {
            var qt = await _quoteTranslationRepository.GetByIdAsync(trans.Id);
            qt.IsDeleted = 1;
            await _quoteTranslationRepository.UpdateAsync(qt);

            await _quoteTranslationRepository.AddAsync(trans);
        }

        public async Task<IEnumerable<QuoteTranslation>> GetByQuoteId(int quoteId)
        {
            var qt = await _quoteTranslationRepository.FindAsync(q => q.QuoteId == quoteId && q.IsDeleted == 0);
            return qt;
        }

        public async Task DeleteTranslation(int id)
        {
            var qt = await _quoteTranslationRepository.GetByIdAsync(id);
            qt.IsDeleted = 1;
            await _quoteTranslationRepository.UpdateAsync(qt);
        }
    }
}

