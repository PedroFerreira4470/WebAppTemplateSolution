using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Extensions.ShadowProperties
{

    [AttributeUsage(AttributeTargets.Class)]
    public class AuditableAttribute : Attribute
    {
    }
}
