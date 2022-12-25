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
        /// <summary> 客户端配置样例文件名 </summary>
        public const string v2raySampleClient = "Maray.Sample.SampleClientConfig.txt";

        public static string MaraySettingFilePath { get; set; }

        public static string SubscribeSettingFilePath { get; set; }

        public static string LogPath { get; set; }

        public static string v2rayConfig { get; set; }

        static PathConfig()
        {
            var appDataPath = FileSystem.AppDataDirectory;
            var startupPath = GetCurrentDictionary(Assembly.GetExecutingAssembly().Location);

            var configPath = appDataPath + "\\Configs\\";
            CreateDirectoryIfNotExist(configPath);

            var cachePath = FileSystem.CacheDirectory;

            var logPath = startupPath + "\\Logs\\";
            CreateDirectoryIfNotExist(logPath);

            MaraySettingFilePath = configPath + "Maray.json";
            SubscribeSettingFilePath = configPath + "Subscribe.json";

            v2rayConfig = configPath + "v2rayConfig.json";
        }

        private static void CreateDirectoryIfNotExist(string dir)
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