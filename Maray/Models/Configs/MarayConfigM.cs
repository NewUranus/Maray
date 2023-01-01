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
        { }

        public string Loglevel { get; set; }
        public ServerM DefaultServer { get; set; }

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
    }
}