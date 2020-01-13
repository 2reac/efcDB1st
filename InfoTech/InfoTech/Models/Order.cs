using System;
using System.Collections.Generic;

namespace InfoTech.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderProduct = new HashSet<OrderProduct>();
        }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? DeliveryAddressId { get; set; }
        public int? StoreId { get; set; }
        public string DiscountCode { get; set; }
        public int? PaymentId { get; set; }
        public string OrderStatus { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual DeliveryAddress DeliveryAddress { get; set; }
        public virtual Discount DiscountCodeNavigation { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
