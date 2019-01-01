using System;
using System.Linq;

namespace FazelMan.Core.Extentions
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
