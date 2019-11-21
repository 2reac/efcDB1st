using System;
using System.Collections.Generic;

namespace InfoTech.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Order = new HashSet<Order>();
        }

        public int PaymentId { get; set; }
        public string PaymentType { get; set; }
        public decimal? PaymentValue { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
