using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;
using ECommerceSystem.Core.Services;
using ECommerceSystem.DTOs.Reviews;

namespace ECommerceSystem.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewsController(IReviewService service)
        {
            _service = service;
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetForProduct(Guid productId)
        {
            var reviews = await _service.GetByProductAsync(productId);

            return Ok(reviews.Select(r => new ReviewResponseDto
            {
                Id = r.Id,
                ProductId = r.ProductId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                UserEmail = r.User.Email
            }));
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Add([FromBody] CreateReviewDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var review = await _service.AddAsync(
                userId,
                dto.ProductId,
                dto.Rating,
                dto.Comment
            );

            return Ok(Map(review));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CreateReviewDto dto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var review = await _service.UpdateAsync(
                id,
                userId,
                dto.Rating,
                dto.Comment
            );

            return Ok(Map(review));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var role = Enum.Parse<UserRole>(User.FindFirstValue(ClaimTypes.Role));

            await _service.DeleteAsync(id, userId, role);
            return NoContent();
        }

        private static ReviewResponseDto Map(Review r)
        {
            return new ReviewResponseDto
            {
                Id = r.Id,
                ProductId = r.ProductId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                UserEmail = r.User.Email
            };
        }
    }
}
