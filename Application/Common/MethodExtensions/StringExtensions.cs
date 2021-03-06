﻿using System.Text.RegularExpressions;

namespace Application.Common.MethodExtensions
{
    public static class StringExtensions
    {
        private const string _emailPattern
            = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public static bool IsValidEmailFormat(this string input)
        {
            if (input is null)
            {
                return false;
            }
            return Regex.IsMatch(input, _emailPattern, RegexOptions.IgnoreCase);
        }

        public static bool IsNotNullOrEmpty(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return true;
        }
    }
}
