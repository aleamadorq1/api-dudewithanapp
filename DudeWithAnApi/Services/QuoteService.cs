using System;
using DudeWithAnApi.Models;
using DudeWithAnApi.Repositories;
using DudeWithAnApi.ResponseDOs;
using Microsoft.EntityFrameworkCore;

namespace DudeWithAnApi.Services
{
    public interface IQuoteService
    {
        Task<Quote> GetLatest();
        Task<IEnumerable<Quote>> GetQuotesAsync();
        Task DeleteQuoteAsync(int id);
        Task UpdateQuoteAsync(Quote quote);
        Task ToggleQuoteAsync(int id);
    }

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

        public async Task<IEnumerable<Quote>> GetQuotesAsync()
        {
            return await _quoteRepository.GetQuotesAsync();
        }

        public async Task DeleteQuoteAsync(int id)
        {
            await _quoteRepository.DeleteQuoteAsync(id);
        }

        public async Task ToggleQuoteAsync(int id)
        {
            await _quoteRepository.ToggleQuoteAsync(id);
        }

        public async Task UpdateQuoteAsync(Quote quote)
        {
            await _quoteRepository.UpdateQuoteAsync(quote);
        }
    }
}

