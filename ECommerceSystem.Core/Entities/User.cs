using System;
using System.Collections.Generic;

namespace ECommerceSystem.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public UserRole Role { get; set; }

        public DateTime CreatedAt { get; set; }

        // Relationships
        public ICollection<Order> Orders { get; set; }

        public ICollection<Product> Products { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}
