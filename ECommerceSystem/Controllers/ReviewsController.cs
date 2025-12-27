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
    public class ReviewsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddReviewRequest request)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var review = new Review
            {
                Id = Guid.NewGuid(),
                Rating = request.Rating,
                Comment = request.Comment,
                ProductId = request.ProductId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Reviews.AddAsync(review);
            await _unitOfWork.SaveChangesAsync();

            return Ok(review);
        }

        [HttpGet("product/{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByProduct(Guid productId)
        {
            var reviews = await _unitOfWork.Reviews.GetByProductIdAsync(productId);
            return Ok(reviews);
        }
    }

    public class AddReviewRequest
    {
        public Guid ProductId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
