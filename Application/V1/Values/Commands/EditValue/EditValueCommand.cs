using System;
using System.Collections.Generic;
using System.Text;

namespace Application.V1.Values.Commands.EditValue
{
    public class EditValueCommand
    {
        /// <summary>
        /// Value Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Value for the Number
        /// </summary>
        public int NewValue { get; set; }
    }
}
