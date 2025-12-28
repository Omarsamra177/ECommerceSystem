using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ECommerceSystem.Core.Services;
using ECommerceSystem.DTOs.Payments;

namespace ECommerceSystem.Controllers
{
    [ApiController]
    [Route("api/payments")]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("pay")]
        public async Task<IActionResult> Pay([FromBody] PayOrderDto dto)
        {
            await _paymentService.PayAsync(dto.OrderId, dto.Method);
            return Ok();
        }
    }
}
