using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Configs
{
    internal class PathConfig
    {

        public static string SettingFilePath { get; set; }

         
        static PathConfig() 
        {
            var startPath = FileSystem.AppDataDirectory;
            var configPath = startPath + "\\Configs\\";
            CreateDirectoryIfNotExist(configPath);

            SettingFilePath = configPath + "Subscribe.json";
        }

        

        static void CreateDirectoryIfNotExist(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}
