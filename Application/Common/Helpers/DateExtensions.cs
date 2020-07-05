using System;

namespace Application.Common.HelperExtensions
{
    public static class DateExtensions
    {
        //This extension method takes a start and end date and returns a list of those dates in an array.
        public static DateTime[] GetDatesUntil(this DateTime fromDate, DateTime toDate)
        {
            var days = (toDate - fromDate).Days + 1;
            if (days < 1)
            {
                return new DateTime[0];
            }
            var dates = new DateTime[days];

            for (var i = 0; i < days; i++)
            {
                dates[i] = fromDate.AddDays(i);
            }

            return dates;
        }

        //Determine the Next date by passing in a DayOfWeek(i.e.From this date, when is the next Tuesday?)
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
