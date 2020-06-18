using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Common.HelperExtensions.String
{
    //for mwthods that doesnt use fluentvalidation
    public static class ValidEmailFormat
    {
        public static bool IsValidEmailFormat(this string input)
            => Regex.IsMatch(input, @"[a-zA-Z0-9\.-_]+@[a-zA-Z0-9\.-_]+");
    }
}
