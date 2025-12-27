using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerceSystem.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Delete(T entity);
        Task DeleteByIdAsync(Guid id);
    }
}
