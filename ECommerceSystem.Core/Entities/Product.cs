using System;
using System.Collections.Generic;

namespace ECommerceSystem.Core.Entities
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public Guid SellerId { get; set; }

        public DateTime CreatedAt { get; set; }

        // Relationships
        public User Seller { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}
