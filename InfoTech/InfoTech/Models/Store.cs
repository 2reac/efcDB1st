using System;
using System.Collections.Generic;

namespace InfoTech.Models
{
    public partial class Store
    {
        public Store()
        {
            Order = new HashSet<Order>();
            Stock = new HashSet<Stock>();
        }

        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Stock> Stock { get; set; }
    }
}
