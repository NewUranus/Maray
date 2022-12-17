

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Helpers
{
    
    class DownloadHelper
    {

        /// <summary>
        /// DownloadString
        /// </summary> 
        /// <param name="url"></param>
        public async Task<string> DownloadStringAsync(string url, bool blProxy, string userAgent)
        {
            try
            {
                //Utils.SetSecurityProtocol(LazyConfig.Instance.GetConfig().enableSecurityProtocolTls13);
               
               // var result = await HttpClientHelper.GetInstance().GetAsync(client, url, cts.Token);
                return null;
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }
    }
    
}
