﻿using System;
using Domain.Extensions.Interfaces;

namespace Domain.Extensions
{
    public abstract class Auditable : IAuditable
    {
        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
