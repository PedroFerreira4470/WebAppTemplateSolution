using System;
using System.Collections.Generic;
using System.Text;
using Domain.Extensions.Interfaces;

namespace Domain.Extensions
{
    public class Active : IActive
    {
        public bool IsActive { get; set; }
    }
}
