using System;

namespace ECommerceSystem.Core.Entities
{
    public class CartItem
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        // Relationships
        public User User { get; set; }

        public Product Product { get; set; }
    }
}
