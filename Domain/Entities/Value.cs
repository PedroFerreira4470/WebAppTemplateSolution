using Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Value : Auditable
    {
        public int ValueId { get; private set; }
        public int ValueNumber { get; set; }
    }
}
