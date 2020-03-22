using Domain.Extensions;

namespace Domain.Entities
{
    public class Value : Auditable, IActive
    {
        public int ValueId { get; private set; }
        public int ValueNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
