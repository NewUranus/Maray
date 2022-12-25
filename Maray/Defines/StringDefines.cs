using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Defines
{
    internal class StringDefines
    {
        #region Inbound

        public const string InboundSocks = "socks";
        public const string InboundSocks2 = "socks2";
        public const string InboundHttp = "http";
        public const string InboundHttp2 = "http2";
        public const string InboundAPITagName = "api";
        public const string InboundAPIProtocal = "dokodemo-door";

        #endregion Inbound

        #region 默认值

        public const string Loopback = "127.0.0.1";

        /// <summary> 默认加密方式 </summary>
        public const string DefaultSecurity = "auto";

        /// <summary> 默认传输协议 </summary>
        public const string DefaultNetwork = "tcp";

        #endregion 默认值

        #region MyRegion

        /// <summary> Tcp伪装http </summary>
        public const string TcpHeaderHttp = "http";

        /// <summary> None值 </summary>
        public const string None = "none";

        /// <summary> 代理 tag值 </summary>
        public const string agentTag = "proxy";

        /// <summary> 直连 tag值 </summary>
        public const string directTag = "direct";

        /// <summary> 阻止 tag值 </summary>
        public const string blockTag = "block";

        #endregion MyRegion

        #region Protocol

        /// <summary> vmess </summary>
        public const string vmessProtocol = "vmess://";

        /// <summary> vmess </summary>
        public const string vmessProtocolLite = "vmess";

        /// <summary> shadowsocks </summary>
        public const string ssProtocol = "ss://";

        /// <summary> shadowsocks </summary>
        public const string ssProtocolLite = "shadowsocks";

        /// <summary> socks </summary>
        public const string socksProtocol = "socks://";

        /// <summary> socks </summary>
        public const string socksProtocolLite = "socks";

        /// <summary> http </summary>
        public const string httpProtocol = "http://";

        /// <summary> https </summary>
        public const string httpsProtocol = "https://";

        /// <summary> vless </summary>
        public const string vlessProtocol = "vless://";

        /// <summary> vless </summary>
        public const string vlessProtocolLite = "vless";

        /// <summary> trojan </summary>
        public const string trojanProtocol = "trojan://";

        #endregion Protocol

        public const string GrpcgunMode = "gun";
    }
}