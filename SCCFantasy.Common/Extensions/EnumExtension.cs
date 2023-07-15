using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.Common.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum enumValue)
        {
            MemberInfo memberInfo = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();

            if (memberInfo != null)
            {
                DescriptionAttribute attribute = memberInfo.GetCustomAttribute(typeof(DescriptionAttribute), true) as DescriptionAttribute;

                return attribute?.Description;
            }

            return string.Empty;
        }
    }
}
