using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;

namespace ECommerceSystem.Core.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetByProductAsync(Guid productId);
        Task<Review> AddAsync(Guid userId, Guid productId, int rating, string comment);
        Task<Review> UpdateAsync(Guid reviewId, Guid userId, int rating, string comment);
        Task DeleteAsync(Guid reviewId, Guid userId, UserRole role);
    }
}
