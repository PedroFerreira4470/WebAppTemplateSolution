using Domain.Extensions;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Customer : IActive, IAuditable
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
        public int CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }

        public ICollection<Order> Orders { get; }
    }
}
