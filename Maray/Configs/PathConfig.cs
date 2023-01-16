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

        #region MarayConfig

        public static string MaraySettingFilePath { get; set; }
        public static string SubscribeSettingFilePath { get; set; }

        #endregion MarayConfig

        #region 文件夹路径

        public static string ConfigPath { get; set; }
        public static string ResourcePath { get; set; }

        public static string LogPath { get; set; }

        #endregion 文件夹路径

        #region v2rayexe&config

        public static string v2rayExePath { get; set; }
        public static string v2rayExeConfigPath { get; set; }
        public static string XrayExePath { get; set; }

        #endregion v2rayexe&config

        static PathConfig()
        {
            var appDataPath = FileSystem.AppDataDirectory;
            var startupPath = GetCurrentDictionary(Assembly.GetExecutingAssembly().Location);

            #region folder

            ConfigPath = appDataPath + "\\Configs\\";
            CreateDirectory(ConfigPath);

            var cachePath = FileSystem.CacheDirectory;

            var logPath = startupPath + "\\Logs\\";
            CreateDirectory(logPath);

            #endregion folder

            #region v2rayexe&config

            ResourcePath = GetParentDirectory(startupPath, 1) + "Resources";

            var v2rayfolder = ResourcePath + "\\Third\\v2ray-windows-64\\";
            v2rayExePath = v2rayfolder + "v2ray.exe";
            v2rayExeConfigPath = v2rayfolder + "config.json";

            XrayExePath = ResourcePath + "\\Third\\Xray-windows-64\\xray.exe";

            #endregion v2rayexe&config

            #region MarayConfig

            MaraySettingFilePath = ConfigPath + "Maray.json";

            SubscribeSettingFilePath = ConfigPath + "Subscribe.json";

            #endregion MarayConfig
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

        /// <summary> 获取父目录,带\\ </summary>
        /// <param name="path"> 当前目录不带\\ </param>
        /// <param name="i">    第几级父目录 </param>
        /// <returns> </returns>
        public static string GetParentDirectory(string path, int i = 1)
        {
            string result = "";
            if (i == 1)
            {
                result = path.Substring(0, path.LastIndexOf(@"\"));
                return result + "\\";
            }
            else
            {
                var temp = path.Split('\\');
                string temp2 = "";
                for (int j = 0; j < temp.Length - i; j++)
                {
                    temp2 += temp[j] + "\\";
                }

                //result = temp2.TrimEnd('\\');
                return temp2;
            }
        }
    }
}