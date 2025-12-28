using System;
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
        public ProductRepository(AppDbContext context) : base(context) { }

        public IQueryable<Product> QueryWithIncludes()
        {
            return _context.Products
                .Include(p => p.ProductCategories)
                .Include(p => p.Reviews)
                .Include(p => p.OrderItems);
        }

        public async Task AddCategoryAsync(Guid productId, Guid categoryId)
        {
            await _context.ProductCategories.AddAsync(new ProductCategory
            {
                ProductId = productId,
                CategoryId = categoryId
            });
        }

        public async Task ClearCategoriesAsync(Guid productId)
        {
            var links = await _context.ProductCategories
                .Where(pc => pc.ProductId == productId)
                .ToListAsync();

            _context.ProductCategories.RemoveRange(links);
        }
    }
}
