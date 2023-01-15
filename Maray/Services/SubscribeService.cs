using Maray.Configs;
using Maray.Helpers;
using Maray.Models;
using Maray.Models.Configs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Services
{
    public class SubscribeService
    {
        public SubscribeSettingM SubscribeSetting { get; set; } = new SubscribeSettingM();

        public SubscribeService()
        {
        }

        public List<SubscribeItemM> GetSubscribeList()
        {
            if (File.Exists(PathConfig.SubscribeSettingFilePath))
            {
                var config = JsonHelper.ReadFromJsonFile<SubscribeSettingM>(PathConfig.SubscribeSettingFilePath);
                return config.Subscribe;
            }
            else
            {
                var defaultConfig = new SubscribeSettingM();

                return defaultConfig.Subscribe;
            }
        }

        public void Remove(SubscribeItemM subscribeItemM)
        {
            SubscribeSetting.Subscribe.Remove(subscribeItemM);

            SaveSubscribeList(SubscribeSetting);
        }

        public void SaveSubscribeList(SubscribeSettingM marayConfigM)
        {
            if (File.Exists(PathConfig.SubscribeSettingFilePath))
            {
                File.Delete(PathConfig.SubscribeSettingFilePath);
            }
            JsonHelper.WriteToJsonFile(PathConfig.SubscribeSettingFilePath, marayConfigM);
        }

        public void SetSubscribeList(List<SubscribeItemM> marayConfigM)
        {
            SubscribeSetting.Subscribe = marayConfigM;

            SaveSubscribeList(SubscribeSetting);
        }
    }
}