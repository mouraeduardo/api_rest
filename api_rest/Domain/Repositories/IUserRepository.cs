using api_rest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_rest.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ListAsync();
        Task AddAsync(User User);
        Task<User> FindByIdAsync(int id);
        Task<User> FirstOrDefaultAsync(string username, string password);
        void Update(User user);
        void Remove(User user);
    }
}
