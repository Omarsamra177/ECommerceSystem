using System.Threading.Tasks;
using ECommerceSystem.Core.Interfaces;
using ECommerceSystem.Infrastructure.Data;

namespace ECommerceSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Products = new ProductRepository(_context);
            Categories = new CategoryRepository(_context);
            Reviews = new ReviewRepository(_context);
            CartItems = new CartItemRepository(_context);
            Orders = new OrderRepository(_context);
        }

        public IUserRepository Users { get; }
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        public IReviewRepository Reviews { get; }
        public ICartItemRepository CartItems { get; }
        public IOrderRepository Orders { get; }

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
