using System;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;

namespace ECommerceSystem.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
