using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Helpers
{
    internal class StringHelper
    {
        /// <summary> 逗号分隔的字符串,转List<string> </summary> <param name="str"></param> <returns></returns>
        public static List<string> String2List(string str)
        {
            try
            {
                str = str.Replace(Environment.NewLine, "");
                return new List<string>(str.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string ToString(object obj)
        {
            try
            {
                return (obj == null ? string.Empty : obj.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
                return string.Empty;
            }
        }
    }
}