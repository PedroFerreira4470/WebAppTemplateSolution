using System;

namespace Application.Common.HelperExtensions.DatesExtensions
{
    //Determine the Next date by passing in a DayOfWeek(i.e.From this date, when is the next Tuesday?)
    public static class NextDate
    {
        public static DateTime Next(this DateTime current, DayOfWeek dayOfWeek)
        {
            var offsetDays = dayOfWeek - current.DayOfWeek;
            if (offsetDays <= 0)
            {
                offsetDays += 7;
            }
            var result = current.AddDays(offsetDays);
            return result;
        }
    }
}
