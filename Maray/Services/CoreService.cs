using Maray.V2ray;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Services
{
    public class CoreService
    {
        private V2rayHelper v2RayHelper = new V2rayHelper();

        public CoreService()
        {
        }

        public void RunCore()
        {
            var config = Helpers.ServiceProviderHelper.GetService<ConfigService>().GetMarayConfig();
            bool res = v2RayHelper.InitCore(config);
            if (res)
            {
                v2RayHelper.StartCore();
            }
        }
    }
}