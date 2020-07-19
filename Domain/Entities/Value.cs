using Domain.Extensions;

namespace Domain.Entities
{
    public class Value : AuditableAndActive
    {
        public int ValueId { get; private set; }
        public int ValueNumber { get; set; }

        public Value() { }
        public Value(int number)
        {
            ValueNumber = number;
        }
    }
}
