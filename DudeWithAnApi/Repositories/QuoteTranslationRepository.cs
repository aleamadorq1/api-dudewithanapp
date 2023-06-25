using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DudeWithAnApi.Models;
using DudeWithAnApi.ResponseDOs;

namespace DudeWithAnApi.Repositories
{
    public interface IQuoteTranslationRepository : IRepository<QuoteTranslation>
    {
        Task<QuoteTranslation> GetByQuoteAndLanguageAsync(int quoteId, string languageCode);
        Task MoveCsvTranslationsAsync(int oldQuoteId, int newQuoteId);
    }

    public class QuoteTranslationRepository : Repository<QuoteTranslation>, IQuoteTranslationRepository
    {
        private readonly AppDbContext _context;

        public QuoteTranslationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<QuoteTranslation> GetByQuoteAndLanguageAsync(int quoteId, string languageCode)
        {
            return await _context.QuoteTranslations.Where(q => q.QuoteId == quoteId && q.LanguageCode == languageCode).FirstOrDefaultAsync();
        }

        public async Task MoveCsvTranslationsAsync(int oldQuoteId, int newQuoteId)
        {
            var oldQuoteTranslations = _context.QuoteTranslations.Where(qt => qt.QuoteId == oldQuoteId);
            await oldQuoteTranslations.ForEachAsync(quoteTranslation => quoteTranslation.QuoteId = newQuoteId);

            await _context.SaveChangesAsync();
        }
    }
}
