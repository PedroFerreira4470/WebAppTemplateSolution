using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Extensions.Interfaces
{
   public interface IAuditable
    {
        string CreatedBy { get; set; }

        DateTime Created { get; set; }

        string LastModifiedBy { get; set; }

        DateTime? LastModified { get; set; }
    }
}
