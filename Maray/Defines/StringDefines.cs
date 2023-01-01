using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Defines
{
    internal class StringDefines
    {
        public const string Loopback = "127.0.0.1";

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

        /// <summary> 默认传输协议 </summary>
        public const string DefaultNetwork = "tcp";

        public const string GrpcgunMode = "gun";
        public const string RoutingRuleComma = "<COMMA>";

        /// <summary> email </summary>
        public const string userEMail = "t@t.tt";

        public const string StreamSecurity = "tls";
        public const string StreamSecurityX = "xtls";
        public const string GrpcmultiMode = "multi";

        /// <summary> 默认加密方式 </summary>
        public const string DefaultSecurity = "auto";

        #region Inbound

        public const string InboundSocks = "socks";
        public const string InboundSocks2 = "socks2";
        public const string InboundHttp = "http";
        public const string InboundHttp2 = "http2";
        public const string InboundAPITagName = "api";
        public const string InboundAPIProtocal = "dokodemo-door";

        #endregion Inbound

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

        /// <summary> trojan </summary>
        public const string trojanProtocolLite = "trojan";

        #endregion Protocol

        #region List<string>

        public static readonly List<string> vmessSecuritys = new List<string> { "aes-128-gcm", "chacha20-poly1305", "auto", "none", "zero" };
        public static readonly List<string> ssSecuritys = new List<string> { "aes-256-gcm", "aes-128-gcm", "chacha20-poly1305", "chacha20-ietf-poly1305", "none", "plain" };
        public static readonly List<string> ssSecuritysInSagerNet = new List<string> { "none", "2022-blake3-aes-128-gcm", "2022-blake3-aes-256-gcm", "2022-blake3-chacha20-poly1305", "aes-128-gcm", "aes-192-gcm", "aes-256-gcm", "chacha20-ietf-poly1305", "xchacha20-ietf-poly1305", "rc4", "rc4-md5", "aes-128-ctr", "aes-192-ctr", "aes-256-ctr", "aes-128-cfb", "aes-192-cfb", "aes-256-cfb", "aes-128-cfb8", "aes-192-cfb8", "aes-256-cfb8", "aes-128-ofb", "aes-192-ofb", "aes-256-ofb", "bf-cfb", "cast5-cfb", "des-cfb", "idea-cfb", "rc2-cfb", "seed-cfb", "camellia-128-cfb", "camellia-192-cfb", "camellia-256-cfb", "camellia-128-cfb8", "camellia-192-cfb8", "camellia-256-cfb8", "salsa20", "chacha20", "chacha20-ietf", "xchacha20" };
        public static readonly List<string> ssSecuritysInXray = new List<string> { "aes-256-gcm", "aes-128-gcm", "chacha20-poly1305", "chacha20-ietf-poly1305", "xchacha20-poly1305", "xchacha20-ietf-poly1305", "none", "plain", "2022-blake3-aes-128-gcm", "2022-blake3-aes-256-gcm", "2022-blake3-chacha20-poly1305" };
        public static readonly List<string> xtlsFlows = new List<string> { "", "xtls-rprx-origin", "xtls-rprx-origin-udp443", "xtls-rprx-direct", "xtls-rprx-direct-udp443", "xtls-rprx-vision", "xtls-rprx-vision-udp443" };
        public static readonly List<string> networks = new List<string> { "tcp", "kcp", "ws", "h2", "quic", "grpc" };
        public static readonly List<string> kcpHeaderTypes = new List<string> { "srtp", "utp", "wechat-video", "dtls", "wireguard" };
        public static readonly List<string> coreTypes = new List<string> { "v2fly", "SagerNet", "Xray", "v2fly_v5" };
        public static readonly List<string> domainMatchers = new List<string> { "linear", "mph", "" };
        public static readonly List<string> fingerprints = new List<string> { "chrome", "firefox", "safari", "randomized", "" };
        public static readonly List<string> domainStrategy4Freedoms = new List<string> { "AsIs", "UseIP", "UseIPv4", "UseIPv6", "" };

        public const string IEProxyExceptions = "localhost;127.*;10.*;172.16.*;172.17.*;172.18.*;172.19.*;172.20.*;172.21.*;172.22.*;172.23.*;172.24.*;172.25.*;172.26.*;172.27.*;172.28.*;172.29.*;172.30.*;172.31.*;192.168.*";

        public static readonly List<string> IEProxyProtocols = new List<string> {
                        "{ip}:{http_port}",
                        "socks={ip}:{socks_port}",
                        "http={ip}:{http_port};https={ip}:{http_port};ftp={ip}:{http_port};socks={ip}:{socks_port}",
                        "http=http://{ip}:{http_port};https=http://{ip}:{http_port}",
                        ""
                    };

        #endregion List<string>
    }
}