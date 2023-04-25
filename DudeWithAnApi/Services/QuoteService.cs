using System;
using DudeWithAnApi.Interfaces;
using DudeWithAnApi.Models;
using DudeWithAnApi.Repositories;
using DudeWithAnApi.ResponseDOs;
using Microsoft.EntityFrameworkCore;

namespace DudeWithAnApi.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IRepository<QuotePrint> _quoteprintRepository;

        public QuoteService(IQuoteRepository userRepository, IRepository<QuotePrint> quotePrintRepository)
        {
            _quoteRepository = userRepository;
            _quoteprintRepository = quotePrintRepository;
        }

        public Task<Quote> GetLatest()
        {
            var quote = _quoteRepository.GetLatestAsync();
            return quote;
        }
    }
}

