using System;
using DudeWithAnApi.Models;
using DudeWithAnApi.ResponseDOs;
using Microsoft.EntityFrameworkCore;

namespace DudeWithAnApi.Interfaces
{
    public interface IQuoteService
    {
        Task<Quote> GetLatest();
        Task<IEnumerable<Quote>> GetQuotesAsync();
        Task DeleteQuoteAsync(int id);
        Task UpdateQuoteAsync(Quote quote);
    }
}

