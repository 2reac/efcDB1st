using System;
using System.Collections.Generic;

namespace InfoTech.Models
{
    public partial class Discount
    {
        public Discount()
        {
            Order = new HashSet<Order>();
        }

        public int DiscountId { get; set; }
        public string DiscountCode { get; set; }
        public int DiscountPercentage { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
