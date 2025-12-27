using System;

namespace ECommerceSystem.Core.Entities
{
    public class Review
    {
        public Guid Id { get; set; }

        public int Rating { get; set; }   

        public string Comment { get; set; }

        public Guid ProductId { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        // Relationships 
        public Product Product { get; set; }

        public User User { get; set; }
    }
}
