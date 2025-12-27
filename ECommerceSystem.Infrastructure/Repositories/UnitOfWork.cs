using ECommerceSystem.Core.Interfaces;
using ECommerceSystem.Infrastructure.Data;
using System.Threading.Tasks;

namespace ECommerceSystem.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUserRepository Users { get; }
        public IProductRepository Products { get; }
        public ICategoryRepository Categories { get; }
        public IReviewRepository Reviews { get; }
        public ICartItemRepository CartItems { get; }
        public IOrderRepository Orders { get; }

        public UnitOfWork(
            AppDbContext context,
            IUserRepository users,
            IProductRepository products,
            ICategoryRepository categories,
            IReviewRepository reviews,
            ICartItemRepository cartItems,
            IOrderRepository orders)
        {
            _context = context;
            Users = users;
            Products = products;
            Categories = categories;
            Reviews = reviews;
            CartItems = cartItems;
            Orders = orders;
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
