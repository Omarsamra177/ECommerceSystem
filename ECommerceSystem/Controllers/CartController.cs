using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerceSystem.Core.Services;
using ECommerceSystem.DTOs.Cart;

namespace ECommerceSystem.Controllers
{
    [ApiController]
    [Route("api/cart")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyCart()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var items = await _service.GetUserCartAsync(userId);

            var result = items.Select(i => new CartItemDto
            {
                CartItemId = i.Id,
                ProductId = i.ProductId,
                ProductName = i.Product.Name,
                Price = i.Product.Price,
                Quantity = i.Quantity
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddToCartDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.AddAsync(userId, dto.ProductId, dto.Quantity);
            return Ok();
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateQuantity(Guid productId, [FromBody] UpdateCartQuantityDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.UpdateQuantityAsync(userId, productId, dto.Quantity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _service.RemoveAsync(id);
            return NoContent();
        }
    }
}
