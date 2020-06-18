using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.HelperExtensions.Number
{
    public static class BiggerThan 
    {
        public static bool IsBiggerThan<T>(this T number1, T number2) where T : struct, IConvertible
            => ((dynamic) number1 > (dynamic)number2);
    }
}
