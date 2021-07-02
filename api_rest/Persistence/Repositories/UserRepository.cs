using api_rest.Domain.Models;
using api_rest.Domain.Repositories;
using api_rest.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_rest.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {

        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _context.users.ToListAsync();
        }

        public async Task AddAsync(User User)
        {
            await _context.AddAsync(User);
        }

        public async Task<User> FindByIdAsync(int id)
        {
            return await _context.users.FindAsync(id);
        }

        public async Task<User> FirstOrDefaultAsync(string username, string password)
        {
            return await _context.users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public void Update(User user)
        {
            _context.users.Update(user);
        }
    
        public void Remove(User user)
        {
            _context.users.Remove(user);
        }
    }
}
