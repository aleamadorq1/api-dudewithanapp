using System;
using DudeWithAnApi.Models;
using DudeWithAnApi.ResponseDOs;

namespace DudeWithAnApi.Interfaces
{
    public interface IQuotePrintService
    {
        Task<IEnumerable<QuotePrintByDay>> GetQuotePrintsByDay(int year, int month);
        Task<IEnumerable<QuotePrintByMonth>> GetQuotePrintsByMonth(int year);
        Task AddPrint(Quote quote);
    }
}

