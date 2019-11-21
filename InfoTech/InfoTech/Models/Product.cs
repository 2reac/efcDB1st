using System;
using System.Collections.Generic;

namespace InfoTech.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderProduct = new HashSet<OrderProduct>();
            ProductCategory = new HashSet<ProductCategory>();
            Stock = new HashSet<Stock>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        public string Description { get; set; }
        public decimal? ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public string Origin { get; set; }
        public bool? InStock { get; set; }
        public int CategoryId { get; set; }
        public int ProductDiscountPercent { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
        public virtual ICollection<ProductCategory> ProductCategory { get; set; }
        public virtual ICollection<Stock> Stock { get; set; }
    }
}
