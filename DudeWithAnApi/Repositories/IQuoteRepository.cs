using DudeWithAnApi.Models;
using DudeWithAnApi.ResponseDOs;
using System.Threading.Tasks;

namespace DudeWithAnApi.Interfaces
{
    public interface IQuoteRepository : IRepository<Quote>
    {
        Task<Quote> GetLatestAsync();
        Task<IEnumerable<Quote>> GetQuotesAsync();
        Task DeleteQuoteAsync(int id);
        Task UpdateQuoteAsync(Quote newQuote);
        Task ToggleQuoteAsync(int id);
    }
}
