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
        private List<SubscribeItemM> SubscribeItemMs = new();

        public SubscribeService()
        {
        }

        public List<SubscribeItemM> GetSubscribeList()
        {
            SubscribeItemMs = ServiceProviderHelper.GetService<ConfigService>().GetMarayConfig().Subscribe;

            return SubscribeItemMs;
        }

        public void SetSubscribeList(List<SubscribeItemM> subscribeItemMs)
        {
            this.SubscribeItemMs = subscribeItemMs;
        }

        public void SaveSubscribeList()
        {
            var configService = ServiceProviderHelper.GetService<ConfigService>();

            var config = configService.GetMarayConfig();
            config.Subscribe = SubscribeItemMs;
            configService.SetMarayConfig(config);
        }
    }
}