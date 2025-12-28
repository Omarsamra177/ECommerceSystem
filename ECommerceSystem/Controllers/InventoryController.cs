using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;
using ECommerceSystem.Core.Services;
using ECommerceSystem.DTOs.Inventory;

namespace ECommerceSystem.Controllers
{
    [ApiController]
    [Route("api/inventory")]
    [Authorize(Roles = "Admin,Seller")]
    public class InventoryController : ControllerBase
    {
        private readonly IProductService _productService;

        public InventoryController(IProductService productService)
        {
            _productService = productService;
        }

        // GET /api/inventory
        [HttpGet]
        public async Task<IActionResult> GetInventory()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var role = Enum.Parse<UserRole>(User.FindFirstValue(ClaimTypes.Role));

            var products = await _productService.GetInventoryAsync(userId, role);

            return Ok(products.Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock
            }));
        }

        // PUT /api/inventory/{productId}
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateStock(
            Guid productId,
            [FromBody] UpdateInventoryDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var role = Enum.Parse<UserRole>(User.FindFirstValue(ClaimTypes.Role));

            await _productService.UpdateStockAsync(
                productId,
                userId,
                role,
                dto.Stock
            );

            return NoContent();
        }
    }
}
