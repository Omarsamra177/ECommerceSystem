using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;
using ECommerceSystem.Core.Interfaces;
using ECommerceSystem.Core.Services;

namespace ECommerceSystem.Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Review>> GetByProductAsync(Guid productId)
        {
            return await _unitOfWork.Reviews.GetByProductIdAsync(productId);
        }

        public async Task<Review> AddAsync(Guid userId, Guid productId, int rating, string comment)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5");

            var existing = await _unitOfWork.Reviews
                .GetByUserAndProductAsync(userId, productId);

            if (existing != null)
                throw new InvalidOperationException("You already reviewed this product");

            var review = new Review
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ProductId = productId,
                Rating = rating,
                Comment = comment,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Reviews.AddAsync(review);
            await _unitOfWork.SaveChangesAsync();

            return await _unitOfWork.Reviews.GetByIdWithUserAsync(review.Id)
                   ?? throw new Exception("Review load failed");
        }

        public async Task<Review> UpdateAsync(Guid reviewId, Guid userId, int rating, string comment)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("Rating must be between 1 and 5");

            var review = await _unitOfWork.Reviews.GetByIdWithUserAsync(reviewId);

            if (review == null)
                throw new Exception("Review not found");

            if (review.UserId != userId)
                throw new UnauthorizedAccessException("You can only update your own review");

            review.Rating = rating;
            review.Comment = comment;

            await _unitOfWork.SaveChangesAsync();
            return review;
        }

        public async Task DeleteAsync(Guid reviewId, Guid userId, UserRole role)
        {
            var review = await _unitOfWork.Reviews.GetByIdAsync(reviewId);

            if (review == null)
                throw new Exception("Review not found");

            if (role != UserRole.Admin && review.UserId != userId)
                throw new UnauthorizedAccessException("You can only delete your own review");

            _unitOfWork.Reviews.Remove(review);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
