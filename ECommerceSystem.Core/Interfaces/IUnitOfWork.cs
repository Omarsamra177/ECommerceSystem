using System;
using System.Threading.Tasks;

namespace ECommerceSystem.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IProductRepository Products { get; }
        ICategoryRepository Categories { get; }
        ICartItemRepository CartItems { get; }
        IOrderRepository Orders { get; }
        IReviewRepository Reviews { get; }

        Task<int> SaveChangesAsync();
    }
}
