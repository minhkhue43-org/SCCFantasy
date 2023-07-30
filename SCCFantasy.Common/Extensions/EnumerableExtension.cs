using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.Common.Extensions
{
    public static class EnumerableExtension
    {
        public static string GetString(this IEnumerable<string> list, string separator = ",")
        {
            return string.Join(separator, list);
        }
    }
}
