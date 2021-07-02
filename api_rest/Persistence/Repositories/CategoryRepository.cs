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
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {

        public CategoryRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _context.categories.ToListAsync();
        }

        public async Task AddAsync(Category category)
        {
            await _context.categories.AddAsync(category);
        }
        public async Task<Category> FindByIdAsync(int id)
        {
            return await _context.categories.FindAsync(id);
        }

        public void Update(Category category)
        {
            _context.categories.Update(category);
        }
    
        public void Remove(Category category)
        {
            _context.categories.Remove(category);
        }
    }
}
