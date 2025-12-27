using ECommerceSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceSystem.Core.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetByUserIdAsync(Guid userId);
    }
}
