using System;
using System.Collections.Generic;

namespace ECommerceSystem.Core.Entities
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        // Relationships 
        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
