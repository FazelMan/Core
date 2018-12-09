using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Core.Common.Enums;

namespace Core.Common.Extentions
{
    public static class RegexExtention
    {
        private static readonly Dictionary<RegexType, string> DicRegex = new Dictionary<RegexType, string>()
        {
            {RegexType.IranPhoneNumber, @"^(\+98|98|0)?9\d{9}$" },
            {RegexType.Email, @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$" }
        };

        public static bool IsValidRegex(this string value, RegexType regexType)
        {
            var pattern = DicRegex[regexType];
            return new Regex(pattern, RegexOptions.IgnoreCase).IsMatch(value);
        }
    }

}
