using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ECommerceSystem.Core.Entities;
using ECommerceSystem.Core.Interfaces;
using ECommerceSystem.Core.Services;

namespace ECommerceSystem.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _unitOfWork.Products.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync(
            Guid? categoryId,
            decimal? minPrice,
            decimal? maxPrice,
            double? minRating,
            string? sortBy,
            int page,
            int pageSize)
        {
            var query = _unitOfWork.Products.QueryWithIncludes();

            if (categoryId.HasValue)
            {
                query = query.Where(p =>
                    p.ProductCategories.Any(pc => pc.CategoryId == categoryId));
            }

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (minRating.HasValue)
            {
                query = query.Where(p =>
                    p.Reviews.Any() &&
                    p.Reviews.Average(r => r.Rating) >= minRating.Value);
            }

            query = sortBy switch
            {
                "price_asc" => query.OrderBy(p => p.Price),

                "price_desc" => query.OrderByDescending(p => p.Price),

                "rating_asc" => query.OrderBy(p =>
                    p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0),

                "rating_desc" => query.OrderByDescending(p =>
                    p.Reviews.Any() ? p.Reviews.Average(r => r.Rating) : 0),

                "popularity_asc" => query.OrderBy(p =>
                    p.OrderItems.Any() ? p.OrderItems.Sum(oi => oi.Quantity) : 0),

                "popularity_desc" => query.OrderByDescending(p =>
                    p.OrderItems.Any() ? p.OrderItems.Sum(oi => oi.Quantity) : 0),

                _ => query
            };

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _unitOfWork.Products.GetByIdAsync(id);
        }

        public async Task<Product> CreateAsync(Product product, IEnumerable<Guid> categoryIds)
        {
            await _unitOfWork.Products.AddAsync(product);

            foreach (var categoryId in categoryIds)
                await _unitOfWork.Products.AddCategoryAsync(product.Id, categoryId);

            await _unitOfWork.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(Guid productId, Guid sellerId, Product updated, IEnumerable<Guid> categoryIds)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);

            if (product == null)
                throw new Exception("Product not found");

            if (product.SellerId != sellerId)
                throw new Exception("Unauthorized");

            product.Name = updated.Name;
            product.Description = updated.Description;
            product.Price = updated.Price;
            product.Stock = updated.Stock;

            await _unitOfWork.Products.ClearCategoriesAsync(productId);

            foreach (var categoryId in categoryIds)
                await _unitOfWork.Products.AddCategoryAsync(productId, categoryId);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid productId, Guid sellerId, UserRole role)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);

            if (product == null)
                throw new Exception("Product not found");

            if (role != UserRole.Admin && product.SellerId != sellerId)
                throw new Exception("Unauthorized");

            _unitOfWork.Products.Remove(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetInventoryAsync(Guid userId, UserRole role)
        {
            var products = await _unitOfWork.Products.GetAllAsync();

            if (role == UserRole.Seller)
                products = products.Where(p => p.SellerId == userId);

            if (role == UserRole.Customer)
                throw new Exception("Unauthorized");

            return products;
        }

        public async Task UpdateStockAsync(Guid productId, Guid userId, UserRole role, int stock)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);

            if (product == null)
                throw new Exception("Product not found");

            if (role != UserRole.Admin && product.SellerId != userId)
                throw new Exception("Unauthorized");

            product.Stock = stock;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
