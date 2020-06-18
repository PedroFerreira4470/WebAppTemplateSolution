using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.HelperExtensions.DatesExtensions
{
    //Determine the Next date by passing in a DayOfWeek(i.e.From this date, when is the next Tuesday?)
    public static class NextDate
    {
        public static DateTime Next(this DateTime current, DayOfWeek dayOfWeek)
        {
            int offsetDays = dayOfWeek - current.DayOfWeek;
            if (offsetDays <= 0)
            {
                offsetDays += 7;
            }
            DateTime result = current.AddDays(offsetDays);
            return result;
        }
    }
}
