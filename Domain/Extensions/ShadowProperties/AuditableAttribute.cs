﻿using System;

namespace Domain.Extensions.ShadowProperties
{

    [AttributeUsage(AttributeTargets.Class)]
    public class AuditableAttribute : Attribute
    {
    }
}
