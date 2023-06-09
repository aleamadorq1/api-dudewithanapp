using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DudeWithAnApi.Models;
using DudeWithAnApi.ResponseDOs;

namespace DudeWithAnApi.Repositories
{
    public interface IQuotePrintRepository : IRepository<QuotePrint>
    {
        Task<IEnumerable<QuotePrintByDay>> GetQuotePrintsByDay(int year, int month);
        Task<IEnumerable<QuotePrintByMonth>> GetQuotePrintsByMonth(int year);
    }

    public class QuotePrintRepository : Repository<QuotePrint>, IQuotePrintRepository
    {
        private readonly AppDbContext _context;

        public QuotePrintRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QuotePrintByDay>> GetQuotePrintsByDay(int year, int month)
        {
            return await _context.QuotePrints
                .Where(qp => qp.PrintedAt.Year == year && qp.PrintedAt.Month == month)
                .GroupBy(qp => qp.PrintedAt.Date)
                .Select(g => new QuotePrintByDay { Date = g.Key, Count = g.Count() })
                .ToListAsync();
        }

        public async Task<IEnumerable<QuotePrintByMonth>> GetQuotePrintsByMonth(int year)
        {
            return await _context.QuotePrints
                .Where(qp => qp.PrintedAt.Year == year)
                .GroupBy(qp => new { qp.PrintedAt.Year, qp.PrintedAt.Month })
                .Select(g => new QuotePrintByMonth { Date = new DateTime(g.Key.Year, g.Key.Month, 1), Count = g.Count() })
                
                .ToListAsync();
        }

    }
}
