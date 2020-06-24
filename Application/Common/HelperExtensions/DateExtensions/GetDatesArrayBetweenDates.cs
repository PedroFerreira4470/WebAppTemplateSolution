using System;

namespace Application.Common.HelperExtensions.DatesExtensions
{
    public static class GetDatesArrayBetweenDates
    {
        //This extension method takes a start and end date and returns a list of those dates in an array.
        public static DateTime[] GetDatesArray(this DateTime fromDate, DateTime toDate)
        {
            var days = (toDate - fromDate).Days;
            var dates = new DateTime[days];

            for (var i = 0; i < days; i++)
            {
                dates[i] = fromDate.AddDays(i);
            }

            return dates;
        }
    }
}
