using api_rest.Communication;
using api_rest.Domain.Models;
using api_rest.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_rest.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> ListAsync();
        Task<UserResponse> SaveAsync(User user);
        Task<UserResponse> UpdateAsync(User user, int id);
        Task<UserResponse> DeleteAsync(int id);
        Task<UserResponse> FirstOrDefaultAsync(string username, string password);
    }
}
