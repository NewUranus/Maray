using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Helpers
{
    internal class NumberHelper
    {
        /// <summary> 转Int </summary>
        /// <param name="obj"> </param>
        /// <returns> </returns>
        public static int ToInt(object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}