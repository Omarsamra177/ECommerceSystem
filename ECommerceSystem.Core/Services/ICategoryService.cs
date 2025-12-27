using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;

namespace ECommerceSystem.Core.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(Guid id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Guid id);
    }
}
