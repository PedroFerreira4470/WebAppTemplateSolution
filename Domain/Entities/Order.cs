using Domain.Enums;
using Domain.Extensions.ShadowProperties;

namespace Domain.Entities
{
    [Auditable]
    [SoftDelete]
    public class Order
    {
        public int OrderId { get; private set; }
        public string OrderName { get; set; }
        public PriorityLevel Priority { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}