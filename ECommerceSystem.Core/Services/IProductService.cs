using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;

namespace ECommerceSystem.Core.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<IEnumerable<Product>> GetAllAsync(
            Guid? categoryId,
            decimal? minPrice,
            decimal? maxPrice,
            double? minRating,
            string? sortBy,
            int page,
            int pageSize);

        Task<Product?> GetByIdAsync(Guid id);

        Task<Product> CreateAsync(Product product, IEnumerable<Guid> categoryIds);

        Task UpdateAsync(Guid productId, Guid sellerId, Product updated, IEnumerable<Guid> categoryIds);

        Task DeleteAsync(Guid productId, Guid sellerId, UserRole role);

        Task<IEnumerable<Product>> GetInventoryAsync(Guid userId, UserRole role);

        Task UpdateStockAsync(Guid productId, Guid userId, UserRole role, int stock);
    }
}
