using System;
using DudeWithAnApi.Interfaces;
using DudeWithAnApi.Models;
using DudeWithAnApi.Repositories;

namespace DudeWithAnApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool Authenticate(string email, string password)
        {
            var user = _userRepository.GetByEmailAsync(email).Result;
            return (user != null && user.Password == password);
        }
    }
}

