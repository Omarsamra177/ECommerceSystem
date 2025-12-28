using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;

namespace ECommerceSystem.Core.Services
{
    public interface ICartService
    {
        Task<IEnumerable<CartItem>> GetUserCartAsync(Guid userId);
        Task<CartItem> AddAsync(Guid userId, Guid productId, int quantity);
        Task UpdateQuantityAsync(Guid userId, Guid productId, int quantity);
        Task RemoveAsync(Guid cartItemId);
    }
}
