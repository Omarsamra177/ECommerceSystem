using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ECommerceSystem.Core.Entities;
using ECommerceSystem.Core.Interfaces;
using ECommerceSystem.Infrastructure.Data;

namespace ECommerceSystem.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetBySellerIdAsync(Guid sellerId)
        {
            return await _context.Products
                .Where(p => p.SellerId == sellerId)
                .ToListAsync();
        }
    }
}
