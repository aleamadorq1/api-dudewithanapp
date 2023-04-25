using DudeWithAnApi.Models;
using DudeWithAnApi.ResponseDOs;
using System.Threading.Tasks;

namespace DudeWithAnApi.Interfaces
{
    public interface IQuotePrintRepository : IRepository<QuotePrint>
    {
        Task<IEnumerable<QuotePrintByDay>> GetQuotePrintsByDay(int year, int month);
        Task<IEnumerable<QuotePrintByMonth>> GetQuotePrintsByMonth(int year);
    }
}
