using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.HelperExtensions.DatesExtensions
{
    public static class IsBetweenDates
    {
        //Check to see if a date is between two dates. 'Nuff said.
        public static bool Between(this DateTime dt, DateTime rangeBeg, DateTime rangeEnd)
        {
            return dt.Ticks >= rangeBeg.Ticks && dt.Ticks <= rangeEnd.Ticks;
        }
    }
}
