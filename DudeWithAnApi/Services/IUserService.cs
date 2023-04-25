using System;
using DudeWithAnApi.Models;

namespace DudeWithAnApi.Interfaces
{
    public interface IUserService
    {
        bool Authenticate(string email, string password);
    }
}

