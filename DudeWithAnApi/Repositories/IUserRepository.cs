using DudeWithAnApi.Models;
using System.Threading.Tasks;

namespace DudeWithAnApi.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
