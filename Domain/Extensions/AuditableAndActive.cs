﻿using System;
using System.Collections.Generic;
using System.Text;
using Domain.Extensions.Interfaces;

namespace Domain.Extensions
{
    public class AuditableAndActive : IAuditable, IActive
    {
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
