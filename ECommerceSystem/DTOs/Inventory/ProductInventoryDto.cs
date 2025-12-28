using System;

namespace ECommerceSystem.DTOs.Inventory
{
    public class ProductInventoryDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
