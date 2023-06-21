using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DudeWithAnApi.Models;
using DudeWithAnApi.ResponseDOs;

namespace DudeWithAnApi.Repositories
{
    public interface IQuoteTranslationRepository : IRepository<QuoteTranslation>
    {
        Task<QuoteTranslation> GetByQuoteAndLanguageAsync(int quoteId, string languageCode);
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
    }
}
