using Domain.Extensions.ShadowProperties;
using System.Collections.Generic;

namespace Domain.Entities
{
    [Auditable]
    [SoftDelete]
    public class Customer
    {
        public int CustomerId { get; private set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public Customer()
        {
            Orders = new HashSet<Order>();
        }
        public ICollection<Order> Orders { get; }
    }
}
