using System;
using DudeWithAnApi.Interfaces;
using DudeWithAnApi.Models;
using DudeWithAnApi.Repositories;
using DudeWithAnApi.ResponseDOs;
using Microsoft.EntityFrameworkCore;

namespace DudeWithAnApi.Services
{
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

        public void AddPrint(Quote quote)
        {
            QuotePrint print = new QuotePrint();
            print.QuoteId = quote.Id;
            print.PrintedAt = DateTime.UtcNow;
            print.RequestId = "???";
            _quotePrintRepository.AddAsync(print);
        }
    }
}

