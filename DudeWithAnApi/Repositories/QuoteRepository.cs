using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DudeWithAnApi.Models;
using DudeWithAnApi.Interfaces;
using DudeWithAnApi.ResponseDOs;

namespace DudeWithAnApi.Repositories
{
    public class QuoteRepository : Repository<Quote>, IQuoteRepository
    {
        private readonly AppDbContext _context;

        public QuoteRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Quote> GetLatestAsync()
        {
            return await _context.Quotes.Where(q => q.IsDeleted == 0).OrderByDescending(q => q.Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Quote>> GetQuotesAsync()
        {
            return await _context.Quotes.Where(q => q.IsDeleted == 0).OrderByDescending(q => q.IsActive).ThenByDescending(q => q.Id).ToListAsync();
        }

        public Task DeleteQuoteAsync(int id)
        {
            Quote quote = GetByIdAsync(id).Result;
            quote.IsDeleted = 1;
            return UpdateAsync(quote);
        }

        public Task ToggleQuoteAsync(int id)
        {
            Quote quote = GetByIdAsync(id).Result;
            quote.IsActive = quote.IsActive ==0 ? 1:0;
            return UpdateAsync(quote);
        }

        public Task UpdateQuoteAsync(Quote quote)
        {
            Quote oldQuote = GetByIdAsync(quote.Id).Result;
            oldQuote.IsDeleted = 1;
            UpdateAsync(oldQuote);
            Quote newQuote = new();
            newQuote.QuoteText = quote.QuoteText;
            newQuote.SecondaryText = quote.SecondaryText;
            newQuote.Url = quote.Url;
            newQuote.CreationDate = DateTime.UtcNow;
            newQuote.IsActive = quote.IsActive;
            newQuote.IsDeleted = 0;
            return AddAsync(newQuote);
        }
    }
}
