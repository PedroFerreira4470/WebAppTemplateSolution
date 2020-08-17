using System;

namespace Application.Common.MethodExtensions
{
    public static class NumberExtensions
    {

        public static bool IsBiggerThan<T>(this T number1, T number2) where T : struct, IConvertible
            => ((dynamic)number1 > (dynamic)number2);
        public static bool IsBiggerOrEqualsThan<T>(this T number1, T number2) where T : struct, IConvertible
            => ((dynamic)number1 >= (dynamic)number2);

        public static bool IsSmallerThan<T>(this T number1, T number2) where T : struct, IConvertible
            => ((dynamic)number1 < (dynamic)number2);
        public static bool IsSmallerOrEqualsThan<T>(this T number1, T number2) where T : struct, IConvertible
            => ((dynamic)number1 <= (dynamic)number2);

        public static bool IsValueEven<T>(this T number) where T : struct, IConvertible
        {
            if (typeof(T) == typeof(DateTime))
            {
                throw new ArgumentException("Date Format is not Valid");
            }

            return ((dynamic)number % 2) == 0;
        }
        public static bool IsValueOdd<T>(this T number) where T : struct, IConvertible
        {
            if (typeof(T) == typeof(DateTime))
            {
                throw new ArgumentException("Date Format is not Valid");
            }

            return ((dynamic)number % 2) != 0;
        }
    }
}
