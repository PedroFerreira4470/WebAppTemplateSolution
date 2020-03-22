using Domain.Enums;
using Domain.Extensions;

namespace Domain.Entities
{
    public class Order : Auditable, IActive
    {
        public int OrderId { get; private set; }
        public string OrderName { get; set; }
        public PriorityLevel Priority { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public bool IsActive { get; set; }
    }
}