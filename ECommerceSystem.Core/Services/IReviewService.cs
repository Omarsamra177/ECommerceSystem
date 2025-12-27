using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;

namespace ECommerceSystem.Core.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetByProductIdAsync(Guid productId);
        Task AddAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(Guid id);
    }
}
