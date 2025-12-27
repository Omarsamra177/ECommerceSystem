using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;

namespace ECommerceSystem.Core.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId);
        Task<Order> GetByIdAsync(Guid id);
        Task<Order> CreateOrderAsync(Guid userId);
        Task UpdateOrderStatusAsync(Guid orderId, OrderStatus status);
    }
}
