using Maray.Configs;
using Maray.Helpers;
using Maray.Models.Configs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Services
{
    internal class ConfigService
    {
        public ConfigService()
        {
        }

        public MarayConfigM GetMarayConfig()
        {
            if (File.Exists(PathConfig.MaraySettingFilePath))
            {
                var config = JsonHelper.ReadFromJsonFile<MarayConfigM>(PathConfig.MaraySettingFilePath);
                return config;
            }
            else
            {
                var defaultConfig = new MarayConfigM();
                defaultConfig.InitDefaultData();

                return defaultConfig;
            }
        }

        public void SetMarayConfig(MarayConfigM marayConfigM)
        {
            if (File.Exists(PathConfig.MaraySettingFilePath))
            {
                File.Delete(PathConfig.MaraySettingFilePath);
            }
            JsonHelper.WriteToJsonFile(PathConfig.MaraySettingFilePath, marayConfigM);
        }
    }
}