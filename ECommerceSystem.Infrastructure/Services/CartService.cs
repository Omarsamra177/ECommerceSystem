using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceSystem.Core.Entities;
using ECommerceSystem.Core.Interfaces;
using ECommerceSystem.Core.Services;

namespace ECommerceSystem.Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CartItem>> GetUserCartAsync(Guid userId)
        {
            return await _unitOfWork.CartItems.GetByUserIdAsync(userId);
        }

        public async Task<CartItem> AddAsync(Guid userId, Guid productId, int quantity)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);

            if (product == null)
                throw new Exception("Product not found");

            if (quantity <= 0 || quantity > product.Stock)
                throw new Exception("Invalid quantity");

            var item = new CartItem
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ProductId = productId,
                Quantity = quantity
            };

            await _unitOfWork.CartItems.AddAsync(item);
            await _unitOfWork.SaveChangesAsync();

            return item;
        }

        public async Task UpdateQuantityAsync(Guid userId, Guid productId, int quantity)
        {
            var item = (await _unitOfWork.CartItems.GetByUserIdAsync(userId))
                .FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
                throw new Exception("Cart item not found");

            var product = await _unitOfWork.Products.GetByIdAsync(productId);

            if (quantity <= 0 || quantity > product.Stock)
                throw new Exception("Invalid quantity");

            item.Quantity = quantity;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid cartItemId)
        {
            var item = await _unitOfWork.CartItems.GetByIdAsync(cartItemId);

            if (item == null)
                throw new Exception("Cart item not found");

            await _unitOfWork.CartItems.DeleteByIdAsync(cartItemId);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
