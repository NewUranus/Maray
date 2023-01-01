using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Configs
{
    internal class PathConfig
    {
        #region 嵌入资源

        /// <summary> v2ray客户端配置样例文件名 </summary>
        public const string v2raySampleClient = "Maray.Sample.SampleClientConfig.txt";

        /// <summary> v2ray服务端配置样例文件名 </summary>
        public const string v2raySampleServer = "Maray.Sample.SampleServerConfig.txt";

        /// <summary> v2ray配置Httprequest文件名 </summary>
        public const string v2raySampleHttprequestFileName = "Maray.Sample.SampleHttprequest.txt";

        /// <summary> v2ray配置Httpresponse文件名 </summary>
        public const string v2raySampleHttpresponseFileName = "Maray.Sample.SampleHttpresponse.txt";

        public const string CustomRoutingFileName = "Maray.Sample.custom_routing_";

        public const string v2raySampleInbound = "Maray.Sample.SampleInbound.txt";

        #endregion 嵌入资源

        public static string MaraySettingFilePath { get; set; }

        public static string LogPath { get; set; }

        public static string v2rayConfig { get; set; }

        static PathConfig()
        {
            var appDataPath = FileSystem.AppDataDirectory;
            var startupPath = GetCurrentDictionary(Assembly.GetExecutingAssembly().Location);

            var configPath = appDataPath + "\\Configs\\";
            CreateDirectory(configPath);

            var cachePath = FileSystem.CacheDirectory;

            var logPath = startupPath + "\\Logs\\";
            CreateDirectory(logPath);

            MaraySettingFilePath = configPath + "Maray.json";

            v2rayConfig = configPath + "v2rayConfig.json";
        }

        private static void CreateDirectory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        private static string GetCurrentDictionary(string dir)
        {
            return System.IO.Directory.GetParent(dir).FullName;
            return Directory.GetDirectoryRoot(dir);
        }
    }
}