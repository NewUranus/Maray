using Maray.Configs;
using Maray.Helpers;
using Maray.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Services
{
    public class SubscribeService
    {
        //private List<ServerM> subscribeList = new();

        public SubscribeService()
        {
            InitData();
        }

        //public void Subscribe(ServerM server)
        //{
        //    subscribeList.Add(server);
        //}

        public List<SubscribeItemM> GetSubscribeList()
        {
            return SubscribeItemMs;
        }

        private List<SubscribeItemM> SubscribeItemMs = new();

        private void InitData()
        {
            if (File.Exists(PathConfig.SettingFilePath))
            {
                SubscribeItemMs = JsonHelper.ReadFromJsonFile<List<SubscribeItemM>>(PathConfig.SettingFilePath);
            }
        }
    }
}