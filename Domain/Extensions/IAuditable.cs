using System;

namespace Domain.Extensions
{
    public interface IAuditable
    {
         int CreatedBy { get; set; }

         DateTime Created { get; set; }

         int LastModifiedBy { get; set; }

         DateTime? LastModified { get; set; }
    }
}
