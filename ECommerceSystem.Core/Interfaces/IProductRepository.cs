using System;
using System.Linq;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;

namespace ECommerceSystem.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IQueryable<Product> QueryWithIncludes();
        Task AddCategoryAsync(Guid productId, Guid categoryId);
        Task ClearCategoriesAsync(Guid productId);
    }
}
