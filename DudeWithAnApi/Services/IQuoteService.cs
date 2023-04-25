using System;
using DudeWithAnApi.Models;
using DudeWithAnApi.ResponseDOs;

namespace DudeWithAnApi.Interfaces
{
    public interface IQuoteService
    {
        Task<Quote> GetLatest();
    }
}

