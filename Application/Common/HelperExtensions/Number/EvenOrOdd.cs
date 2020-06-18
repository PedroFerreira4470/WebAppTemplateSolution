using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.HelperExtensions.Number
{
    public static class EvenOrOdd
    {
        public static bool IsValueEven<T>(this T number) where T : struct, IConvertible
            => ((dynamic)number % 2) == 0;
        public static bool IsValueOdd<T>(this T number) where T : struct, IConvertible
            => ((dynamic)number % 2) != 0;
    }
}
