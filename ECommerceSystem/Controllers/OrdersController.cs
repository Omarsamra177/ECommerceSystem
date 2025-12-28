using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;
using ECommerceSystem.Core.Services;
using ECommerceSystem.DTOs.Orders;
using ECommerceSystem.DTOs.Payments;

namespace ECommerceSystem.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;

        public OrdersController(
            IOrderService orderService,
            IPaymentService paymentService)
        {
            _orderService = orderService;
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var order = await _orderService.PlaceOrderAsync(userId);

            return Ok(Map(order));
        }

        [HttpPost("pay")]
        public async Task<IActionResult> Pay([FromBody] PayOrderDto dto)
        {
            await _paymentService.PayAsync(dto.OrderId, dto.Method);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var orders = await _orderService.GetUserOrdersAsync(userId);

            return Ok(orders.Select(Map));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var role = User.FindFirstValue(ClaimTypes.Role);

            var order = await _orderService.GetByIdAsync(id);

            if (order == null)
                return NotFound();

            if (role != UserRole.Admin.ToString() && order.UserId != userId)
                return Forbid();

            return Ok(Map(order));
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(
            Guid id,
            [FromBody] UpdateOrderStatusDto dto)
        {
            await _orderService.UpdateStatusAsync(id, dto.Status);
            return NoContent();
        }

        private static OrderResponseDto Map(Order order)
        {
            return new OrderResponseDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                Items = order.OrderItems.Select(i => new OrderItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList()
            };
        }
    }
}
