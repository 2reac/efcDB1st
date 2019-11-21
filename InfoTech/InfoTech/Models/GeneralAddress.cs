using System;
using System.Collections.Generic;

namespace InfoTech.Models
{
    public partial class GeneralAddress
    {
        public GeneralAddress()
        {
            Customer = new HashSet<Customer>();
            DeliveryAddress = new HashSet<DeliveryAddress>();
        }

        public int AddressId { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string ZipCode { get; set; }

        public virtual ICollection<Customer> Customer { get; set; }
        public virtual ICollection<DeliveryAddress> DeliveryAddress { get; set; }
    }
}
