using System;
using System.Collections.Generic;

namespace InfoTech.Models
{
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
            ProductCategory = new HashSet<ProductCategory>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool? CategoryAvailability { get; set; }

        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<ProductCategory> ProductCategory { get; set; }
    }
}
