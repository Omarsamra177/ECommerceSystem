using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;
using ECommerceSystem.Core.Services;
using ECommerceSystem.DTOs.Products;
using ECommerceSystem.DTOs.Common;

namespace ECommerceSystem.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            Guid? categoryId,
            decimal? minPrice,
            decimal? maxPrice,
            double? minRating,
            string? sortBy,
            int page = 1,
            int pageSize = 10)
        {
            var products = await _service.GetAllAsync(
                categoryId,
                minPrice,
                maxPrice,
                minRating,
                sortBy,
                page,
                pageSize);

            var items = products.Select(p => new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock
            }).ToList();

            var totalCount = items.Count;

            var response = new PagedResponseDto<ProductResponseDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Items = items
            };

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Seller,Admin")]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var sellerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                SellerId = sellerId,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _service.CreateAsync(product, dto.CategoryIds);

            return Ok(new ProductResponseDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
                Price = created.Price,
                Stock = created.Stock
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Seller,Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateProductDto dto)
        {
            var sellerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var updated = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock
            };

            await _service.UpdateAsync(id, sellerId, updated, dto.CategoryIds);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Seller,Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var sellerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var role = Enum.Parse<UserRole>(User.FindFirstValue(ClaimTypes.Role));

            await _service.DeleteAsync(id, sellerId, role);
            return NoContent();
        }
    }
}
