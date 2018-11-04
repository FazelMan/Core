using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Common.Extentions
{
    public static class EnumExtention
    {
        public static T GetEnum<T>(this string name)
        {
            var enumList = Enum.GetNames(typeof(T)).FirstOrDefault(x => x.ToLower() == name.ToLower());
            var enumType = (T)Enum.Parse(typeof(T), enumList ?? throw new InvalidOperationException());
            return enumType;
        }
    }
}
