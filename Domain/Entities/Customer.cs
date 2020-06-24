using Domain.Extensions;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Customer : Auditable, IActive
    {
        public int CustomerId { get; private set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }

        public Customer()
        {
            Orders = new HashSet<Order>();
        }
        public ICollection<Order> Orders { get; }
    }
}
