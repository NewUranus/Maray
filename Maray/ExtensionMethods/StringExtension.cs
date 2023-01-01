using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.ExtensionMethods
{
    internal static class StringExtension
    {
        public static string TrimEx(this string value)
        {
            return value == null ? string.Empty : value.Trim();
        }
    }
}