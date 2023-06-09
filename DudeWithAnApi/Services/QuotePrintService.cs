using System;
using DudeWithAnApi.Models;
using DudeWithAnApi.Repositories;
using DudeWithAnApi.ResponseDOs;
using Microsoft.EntityFrameworkCore;

namespace DudeWithAnApi.Services
{
    public interface IQuotePrintService
    {
        Task<IEnumerable<QuotePrintByDay>> GetQuotePrintsByDay(int year, int month);
        Task<IEnumerable<QuotePrintByMonth>> GetQuotePrintsByMonth(int year);
        Task AddPrint(Quote quote);
    }

    public class QuotePrintService : IQuotePrintService
    {
        private readonly IQuotePrintRepository _quotePrintRepository;

        public QuotePrintService(IQuotePrintRepository quoteprintRepository)
        {
            _quotePrintRepository = quoteprintRepository;
        }

        public async Task<IEnumerable<QuotePrintByDay>> GetQuotePrintsByDay(int year, int month)
        {
            return await _quotePrintRepository.GetQuotePrintsByDay(year, month);
        }

        public async Task<IEnumerable<QuotePrintByMonth>> GetQuotePrintsByMonth(int year)
        {
            return await _quotePrintRepository.GetQuotePrintsByMonth(year);
        }

        public async Task AddPrint(Quote quote)
        {
            QuotePrint print = new QuotePrint();
            print.QuoteId = quote.Id;
            print.PrintedAt = DateTime.UtcNow;
            print.RequestId = "???";
            _quotePrintRepository.AddMetricsAsync(print);
        }
    }
}

