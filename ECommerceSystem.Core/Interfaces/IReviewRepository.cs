using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;

namespace ECommerceSystem.Core.Interfaces
{
    public interface IReviewRepository : IGenericRepository<Review>
    {
        Task<IEnumerable<Review>> GetByProductIdAsync(Guid productId);
    }
}
