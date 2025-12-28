using System.Threading.Tasks;
using ECommerceSystem.Core.Interfaces;
using ECommerceSystem.Infrastructure.Data;
using ECommerceSystem.Infrastructure.Repositories;

namespace ECommerceSystem.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUserRepository Users { get; }
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        public ICartItemRepository CartItems { get; }
        public IOrderRepository Orders { get; }
        public IReviewRepository Reviews { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Users = new UserRepository(context);
            Products = new ProductRepository(context);
            Categories = new CategoryRepository(context);
            CartItems = new CartItemRepository(context);
            Orders = new OrderRepository(context);
            Reviews = new ReviewRepository(context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
