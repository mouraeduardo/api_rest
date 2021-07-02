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
    public class ProductRepository : BaseRepository, IProductRepository
    {

        public ProductRepository(AppDbContext context) :base(context) { }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _context.products.ToListAsync();
        }
    
        public async Task AddAsync(Product Product)
        {
            await _context.products.AddAsync(Product);
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            return await _context.products.FindAsync(id);
        }

        public void Update(Product product)
        {
            _context.products.Update(product);
        }

        public void Remove(Product product)
        {
            _context.Remove(product);
        }
    }
}
