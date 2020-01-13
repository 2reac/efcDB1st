using System;
using System.Collections.Generic;

namespace InfoTech.Models
{
    public partial class DeliveryAddress
    {
        public DeliveryAddress()
        {
            Order = new HashSet<Order>();
        }

        public int DeliveryAddressId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int AddressId { get; set; }

        public virtual GeneralAddress Address { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
