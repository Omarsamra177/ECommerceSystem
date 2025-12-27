using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;

namespace ECommerceSystem.Core.Services
{
    public interface ICartService
    {
        Task<IEnumerable<CartItem>> GetUserCartAsync(Guid userId);
        Task AddItemAsync(Guid userId, Guid productId, int quantity);
        Task UpdateItemAsync(Guid userId, Guid productId, int quantity);
        Task RemoveItemAsync(Guid userId, Guid productId);
    }
}
