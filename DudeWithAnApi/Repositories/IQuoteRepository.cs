using DudeWithAnApi.Models;
using DudeWithAnApi.ResponseDOs;
using System.Threading.Tasks;

namespace DudeWithAnApi.Interfaces
{
    public interface IQuoteRepository : IRepository<Quote>
    {
        Task<Quote> GetLatestAsync();
    }
}
