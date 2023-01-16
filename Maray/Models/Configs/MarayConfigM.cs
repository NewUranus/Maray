using Maray.Defines;
using Maray.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Models.Configs
{
    internal class MarayConfigM
    {
        public MarayConfigM()
        {
        }

        public ServerM DefaultServer { get; set; }

        public void InitDefaultData()
        {
            Loglevel = "warning";

            //本地监听
            if (inbound == null)
            {
                inbound = new List<InItem>();
                InItem inItem = new InItem();
                inItem.protocol = StringDefines.InboundSocks;
                inItem.localPort = 10808;
                inItem.udpEnabled = true;
                inItem.sniffingEnabled = true;

                inbound.Add(inItem);
            }

            //路由规则
            if (string.IsNullOrEmpty(domainStrategy))
            {
                domainStrategy = "IPIfNonMatch";
            }

            //kcp
            if (kcpItem == null)
            {
                kcpItem = new KcpItem
                {
                    mtu = 1350,
                    tti = 50,
                    uplinkCapacity = 12,
                    downlinkCapacity = 100,
                    readBufferSize = 2,
                    writeBufferSize = 2,
                    congestion = false
                };
            }
        }

        public bool logEnabled { get; set; }

        public string Loglevel { get; set; }

        /// <summary> 本地监听 </summary>
        public List<InItem> inbound { get; set; }

        public string domainStrategy { get; set; }
        public string domainMatcher { get; set; }
        public bool enableRoutingAdvanced { get; set; }

        public List<RoutingItem> routings { get; set; }

        public int routingIndex { get; set; }

        /// <summary> 允许Mux多路复用 </summary>
        public bool muxEnabled { get; set; }

        /// <summary> 启用实时网速和流量统计 </summary>
        public bool enableStatistics { get; set; }

        public List<CoreTypeItem> coreTypeItem { get; set; }

        public KcpItem kcpItem { get; set; }

        /// <summary> Outbound Freedom domainStrategy </summary>
        public string domainStrategy4Freedom { get; set; }

        /// <summary> 自定义远程DNS </summary>
        public string remoteDNS { get; set; }
    }
}