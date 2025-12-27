using ECommerceSystem.Core;
using System;
using System.Collections.Generic;

namespace ECommerceSystem.Core.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }

        // Relationships
        public User User { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
