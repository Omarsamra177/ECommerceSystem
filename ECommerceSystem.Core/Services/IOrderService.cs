using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;

namespace ECommerceSystem.Core.Services
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(Guid userId);
        Task<IEnumerable<Order>> GetUserOrdersAsync(Guid userId);
        Task<Order?> GetByIdAsync(Guid orderId);
        Task UpdateStatusAsync(Guid orderId, OrderStatus status);
    }
}
