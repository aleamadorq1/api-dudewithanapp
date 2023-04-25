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
            return await _context.Quotes.OrderByDescending(q => q.Id).FirstOrDefaultAsync();
        }

    }
}
