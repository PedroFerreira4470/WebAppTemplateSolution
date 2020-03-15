using Domain.Extensions;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Customer : Auditable, IActive
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; private set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Order> Orders { get; }
    }
}
