using System;
using System.Linq;

namespace Application.Common.HelperExtensions
{
    public static class ObjectExtensions
    {
        public static bool IsIn<T>(this T source, params T[] list)
        {
            if (source is null)
            {
                throw new ArgumentNullException("source");
            }
            if (list is null)
            {
                throw new ArgumentNullException("list");
            }
            if (list.Any() == false)
            {
                return false;
            }

            return list.Contains(source);
        }

        //Check to see if a obj is between two dates Inclusive.
        public static bool IsBetween<T>(this T actual, T lower, T upper, bool inclusive = true) where T : IComparable<T>
        {
            if (inclusive)
            {
                return actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) <= 0;
            }
            return actual.CompareTo(lower) > 0 && actual.CompareTo(upper) < 0;
        }


    }
}
