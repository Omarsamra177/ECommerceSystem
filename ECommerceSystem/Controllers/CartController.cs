using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ECommerceSystem.Core.Interfaces;
using ECommerceSystem.Core.Entities;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerceSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyCart()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var items = await _unitOfWork.CartItems.GetByUserIdAsync(userId);
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddToCartRequest request)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var item = new CartItem
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };

            await _unitOfWork.CartItems.AddAsync(item);
            await _unitOfWork.SaveChangesAsync();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _unitOfWork.CartItems.DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }

    public class AddToCartRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
