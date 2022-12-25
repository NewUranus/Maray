using Maray.Configs;

using NLog;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Helpers
{
    internal class NLogHelper
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void WriteLog(object message)
        {
            logger.Info(message);
        }

        public static void WriteExceptionLog(object message)
        {
            logger.Error(message);
        }
    }
}