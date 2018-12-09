using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace FazelMan.Core.Common.Extentions
{
    public static class DisplayNameEnum
    {
        public static string DisplayName(this Enum value)
        {
            try
            {
                if (value == null)
                    return string.Empty;
                Type enumType = value.GetType();
                String enumValue = Enum.GetName(enumType, value);
                MemberInfo member = enumType.GetMember(enumValue)[0];

                var attrs = member.GetCustomAttributes(typeof(DisplayAttribute), false);
                var outString = ((DisplayAttribute)attrs[0]).Name;

                if (((DisplayAttribute)attrs[0]).ResourceType != null)
                {
                    outString = ((DisplayAttribute)attrs[0]).GetName();
                }

                return outString;
            }
            catch (System.Exception)
            {
                return string.Empty;
            }

        }
    }
}
