using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Models.Configs
{
    public class InItem
    {
        /// <summary> 本地监听端口 </summary>
        public int localPort { get; set; }

        /// <summary> 协议，默认为socks </summary>
        public string protocol { get; set; }

        /// <summary> 允许udp </summary>
        public bool udpEnabled { get; set; }

        /// <summary> 开启流量探测 </summary>
        public bool sniffingEnabled { get; set; } = true;

        public bool allowLANConn { get; set; }

        public string user { get; set; }

        public string pass { get; set; }

        public string listen { get; set; }
    }
}