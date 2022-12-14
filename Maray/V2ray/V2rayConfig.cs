namespace Maray.V2ray
{
    /// <summary> 配置文件格式 https://www.v2fly.org/config/overview.html </summary>
    public class V2rayConfig
    {
        /// <summary> 日志配置 </summary>
        public Log log { get; set; }

        /// <summary> 一个数组，每个元素是一个入站连接配置。 </summary>
        public List<Inbounds> inbounds { get; set; }

        /// <summary> 一个数组，每个元素是一个出站连接配置。列表中的第一个元素作为主出站协议。当路由匹配不存在或没有匹配成功时，流量由主出站协议发出。 </summary>
        public List<Outbounds> outbounds { get; set; }

        /// <summary> 统计需要， 空对象 </summary>
        public Stats stats { get; set; }

        /// <summary> 远程控制 </summary>
        public API api { get; set; }

        /// <summary> 本地策略，可进行一些权限相关的配置。 </summary>
        public Policy policy;

        /// <summary> 内置的 DNS 服务器，若此项不存在，则默认使用本机的 DNS 设置。 </summary>
        public object dns { get; set; }

        /// <summary> 路由功能。 </summary>
        public Routing routing { get; set; }
    }

    public class Routing
    {
        /// <summary> </summary>
        public string domainStrategy { get; set; }

        /// <summary> </summary>
        public string domainMatcher { get; set; }

        /// <summary> </summary>
        public List<RulesItem> rules { get; set; }
    }

    public class RulesItem
    {
        public string type { get; set; }

        public string port { get; set; }

        public List<string> inboundTag { get; set; }

        public string outboundTag { get; set; }

        public List<string> ip { get; set; }

        public List<string> domain { get; set; }

        public List<string> protocol { get; set; }

        public bool enabled { get; set; } = true;
    }

    public class Policy
    {
        public SystemPolicy system;
    }

    public class SystemPolicy
    {
        public bool statsOutboundUplink;
        public bool statsOutboundDownlink;
    }

    public class Outbounds
    {
        /// <summary> 默认值agentout </summary>
        public string tag { get; set; }

        /// <summary> </summary>
        public string protocol { get; set; }

        /// <summary> </summary>
        public Outboundsettings settings { get; set; }

        /// <summary> </summary>
        public StreamSettings streamSettings { get; set; }

        /// <summary> </summary>
        public Mux mux { get; set; }
    }

    public class Mux
    {
        /// <summary> </summary>
        public bool enabled { get; set; }

        /// <summary> </summary>
        public int concurrency { get; set; }
    }

    public class VnextItem
    {
        /// <summary> </summary>
        public string address { get; set; }

        /// <summary> </summary>
        public int port { get; set; }

        /// <summary> </summary>
        public List<UsersItem> users { get; set; }
    }

    public class Response
    {
        /// <summary> </summary>
        public string type { get; set; }
    }

    public class Outboundsettings
    {
        /// <summary> </summary>
        public List<VnextItem> vnext { get; set; }

        /// <summary> </summary>
        public List<ServersItem> servers { get; set; }

        /// <summary> </summary>
        public Response response { get; set; }

        /// <summary> </summary>
        public string domainStrategy { get; set; }

        /// <summary> </summary>
        public int? userLevel { get; set; }
    }

    public class API
    {
        public string tag { get; set; }
        public List<string> services { get; set; }
    }

    public class ServersItem
    {
        /// <summary> </summary>
        public string email { get; set; }

        /// <summary> </summary>
        public string address { get; set; }

        /// <summary> </summary>
        public string method { get; set; }

        /// <summary> </summary>
        public bool ota { get; set; }

        /// <summary> </summary>
        public string password { get; set; }

        /// <summary> </summary>
        public int port { get; set; }

        /// <summary> </summary>
        public int level { get; set; }

        /// <summary> trojan </summary>
        public string flow { get; set; }

        /// <summary> </summary>
        public List<SocksUsersItem> users { get; set; }
    }

    public class SocksUsersItem
    {
        /// <summary> </summary>
        public string user { get; set; }

        /// <summary> </summary>
        public string pass { get; set; }

        /// <summary> </summary>
        public int level { get; set; }
    }

    public class Stats
    { };

    public class Log
    {
        /// <summary> </summary>
        public string access { get; set; }

        /// <summary> </summary>
        public string error { get; set; }

        /// <summary> </summary>
        public string loglevel { get; set; }
    }

    /// <summary> https://www.v2fly.org/config/inbounds.html </summary>
    public class Inbounds
    {
        public string tag { get; set; }

        /// <summary> </summary>
        public int port { get; set; }

        /// <summary> </summary>
        public string listen { get; set; }

        /// <summary> </summary>
        public string protocol { get; set; }

        /// <summary> </summary>
        public Sniffing sniffing { get; set; }

        /// <summary> </summary>
        public Inboundsettings settings { get; set; }

        /// <summary> </summary>
        public StreamSettings streamSettings { get; set; }
    }

    public class Sniffing
    {
        /// <summary> </summary>
        public bool enabled { get; set; }

        /// <summary> </summary>
        public List<string> destOverride { get; set; }
    }

    public class Inboundsettings
    {
        /// <summary> </summary>
        public string auth { get; set; }

        /// <summary> </summary>
        public bool udp { get; set; }

        /// <summary> </summary>
        public string ip { get; set; }

        /// <summary> api 使用 </summary>
        public string address { get; set; }

        /// <summary> </summary>
        public List<UsersItem> clients { get; set; }

        /// <summary> VLESS </summary>
        public string decryption { get; set; }

        public bool allowTransparent { get; set; }

        public List<AccountsItem> accounts { get; set; }
    }

    public class AccountsItem
    {
        /// <summary> </summary>
        public string user { get; set; }

        /// <summary> </summary>
        public string pass { get; set; }
    }

    public class UsersItem
    {
        /// <summary> </summary>
        public string id { get; set; }

        /// <summary> </summary>
        public int alterId { get; set; }

        /// <summary> </summary>
        public string email { get; set; }

        /// <summary> </summary>
        public string security { get; set; }

        /// <summary> VLESS </summary>
        public string encryption { get; set; }

        /// <summary> VLESS </summary>
        public string flow { get; set; }
    }

    public class TlsSettings
    {
        /// <summary> 是否允许不安全连接（用于客户端） </summary>
        public bool allowInsecure { get; set; }

        /// <summary> </summary>
        public string serverName { get; set; }

        /// <summary> </summary>
        public List<string> alpn
        {
            get; set;
        }

        /// <summary> "chrome" | "firefox" | "safari" | "randomized" </summary>
        public string fingerprint { get; set; }
    }

    public class Header
    {
        /// <summary> 伪装 </summary>
        public string type { get; set; }

        /// <summary> 结构复杂，直接存起来 </summary>
        public object request { get; set; }

        /// <summary> 结构复杂，直接存起来 </summary>
        public object response { get; set; }
    }

    public class TcpSettings
    {
        /// <summary> 数据包头部伪装设置 </summary>
        public Header header { get; set; }
    }

    public class StreamSettings
    {
        /// <summary> </summary>
        public string network { get; set; }

        /// <summary> </summary>
        public string security { get; set; }

        /// <summary> </summary>
        public TlsSettings tlsSettings { get; set; }

        /// <summary> Tcp传输额外设置 </summary>
        public TcpSettings tcpSettings { get; set; }

        /// <summary> Kcp传输额外设置 </summary>
        public KcpSettings kcpSettings { get; set; }

        /// <summary> ws传输额外设置 </summary>
        public WsSettings wsSettings { get; set; }

        /// <summary> h2传输额外设置 </summary>
        public HttpSettings httpSettings { get; set; }

        /// <summary> QUIC </summary>
        public QuicSettings quicSettings { get; set; }

        /// <summary> VLESS xtls </summary>
        public TlsSettings xtlsSettings { get; set; }

        /// <summary> grpc </summary>
        public GrpcSettings grpcSettings { get; set; }
    }

    public class QuicSettings
    {
        /// <summary> </summary>
        public string security { get; set; }

        /// <summary> </summary>
        public string key { get; set; }

        /// <summary> </summary>
        public Header header { get; set; }
    }

    public class GrpcSettings
    {
        /// <summary> </summary>
        public string serviceName { get; set; }

        /// <summary> </summary>
        public bool multiMode { get; set; }
    }

    public class HttpSettings
    {
        /// <summary> </summary>
        public string path { get; set; }

        /// <summary> </summary>
        public List<string> host { get; set; }
    }

    public class WsSettings
    {
        /// <summary> </summary>
        public string path { get; set; }

        /// <summary> </summary>
        public Headers headers { get; set; }
    }

    public class Headers
    {
        /// <summary> </summary>
        public string Host { get; set; }
    }

    public class KcpSettings
    {
        /// <summary> </summary>
        public int mtu { get; set; }

        /// <summary> </summary>
        public int tti { get; set; }

        /// <summary> </summary>
        public int uplinkCapacity { get; set; }

        /// <summary> </summary>
        public int downlinkCapacity { get; set; }

        /// <summary> </summary>
        public bool congestion { get; set; }

        /// <summary> </summary>
        public int readBufferSize { get; set; }

        /// <summary> </summary>
        public int writeBufferSize { get; set; }

        /// <summary> </summary>
        public Header header { get; set; }

        /// <summary> </summary>
        public string seed { get; set; }
    }

    public class Dns
    {
        /// <summary> </summary>
        public List<string> servers { get; set; }
    }
}