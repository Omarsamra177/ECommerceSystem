using ECommerceSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceSystem.Core.Interfaces
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        Task<IEnumerable<CartItem>> GetByUserIdAsync(Guid userId);
    }
}
