using System;
using Domain.Enums;
using Domain.Extensions;

namespace Domain.Entities
{
    public class Order : IActive, IAuditable
    {
        public int OrderId { get; private set; }
        public string OrderName { get; set; }
        public PriorityLevel Priority { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}