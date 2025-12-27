using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;

namespace ECommerceSystem.Core.Interfaces
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        Task<IEnumerable<CartItem>> GetByUserIdAsync(Guid userId);
    }
}
