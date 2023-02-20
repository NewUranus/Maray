using Maray.Defines;
using Maray.Models.V2rayConfig;

namespace Maray.Models.XrayConfigs
{
    /// <summary> https://xtls.github.io/config/ </summary>
    public class XrayConfig
    {
        public Log log { get; set; } = new Log();

        public List<Inbound> inbounds { get; set; } = new List<Inbound>();

        public List<Outbound> outbounds { get; set; } = new List<Outbound>();
        public Routing routing { get; set; }
        public Dns dns { get; set; }

        public XrayConfig()
        {
            inbounds.Add(new Inbound()
            {
                tag = "socks",
                listen = "127.0.0.1",
                port = 10808,
                protocol = "socks",

                settings = new InboundSocksSettings()
                {
                    udp = true,
                },
            });

            inbounds.Add(new Inbound()
            {
                tag = "http",
                listen = "127.0.0.1",
                port = 10801,
                protocol = "http",

                settings = new InboundSocksSettings()
                {
                    udp = true,
                },
            });
        }
    }

    #region Log

    public class Log
    {
        public string loglevel { get; set; }

        public Log()
        {
            loglevel = "warning";
        }
    }

    #endregion Log

    #region Routing

    public class Routing
    {
        /// <summary>
        /// 域名解析策略，根据不同的设置使用不同的策略。
        /// <para> "AsIs" | "IPIfNonMatch" | "IPOnDemand" </para>
        /// </summary>
        public string domainStrategy { get; set; }

        /// <summary>
        /// 域名匹配算法，根据不同的设置使用不同的算法。此处选项会影响所有未单独指定匹配算法的 RuleObject。
        /// <para> "hybrid" | "linear" </para>
        /// </summary>
        public string domainMatcher { get; set; }

        /// <summary>
        /// 对应一个数组，数组中每一项是一个规则。
        /// <para> 对于每一个连接，路由将根据这些规则依次进行判断，当一个规则生效时，即将这个连接转发至它所指定的 outboundTag或 balancerTag。 </para>
        /// </summary>
        public List<Rule> rules { get; set; }

        public Routing()
        {
            //只使用域名进行路由选择。默认值。
            domainStrategy = "AsIs";
            //使用新的域名匹配算法，速度更快且占用更少。默认值。
            domainMatcher = "hybrid";
        }
    }

    public class Rule
    {
        public List<string> domain { get; set; }
        public List<string> ip { get; set; }
        public string outboundTag { get; set; }
        public string type { get; set; }
    }

    #endregion Routing

    #region Inbound

    public class Inbound
    {
        /// <summary>
        /// number | "env:variable" | string
        /// <para> 当只有一个端口时，Xray 会在此端口监听入站连接。当指定了一个端口范围时，取决于 allocate 设置。 </para>
        /// </summary>
        public int port { get; set; }

        /// <summary> 具体的配置内容，视协议不同而不同。详见每个协议中的 InboundConfigurationObject。 </summary>
        public object settings { get; set; }

        public Sniffing sniffing { get; set; }
        public string listen { get; set; }

        public string protocol { get; set; }
        public string tag { get; set; }

        public Inbound()
        {
            sniffing = new Sniffing();
            sniffing.enabled = true;
            sniffing.destOverride = new List<string>() { "http", "tls" };
        }
    }

    #endregion Inbound

    #region InboundSettings

    #region InboundHttpSettings

    internal class InboundHttpSettings
    {
        public int timeout { get; set; }
        public List<Account> accounts { get; set; }
        public bool allowTransparent { get; set; }
        public int userLevel { get; set; }

        public InboundHttpSettings()
        {
            timeout = 300;
        }
    }

    #endregion InboundHttpSettings

    #region InboundSocksSettings

    public class InboundSocksSettings
    {
        /// <summary> Socks 协议的认证方式，支持 "noauth" 匿名方式和 "password" 用户密码方式。 </summary>
        public string auth { get; set; }

        /// <summary> 是否开启 UDP 协议的支持。 </summary>
        public bool udp { get; set; }

        /// <summary> 当开启 UDP 时，Xray 需要知道本机的 IP 地址。 </summary>
        public string ip { get; set; }

        /// <summary>
        /// 一个数组，数组中每个元素为一个用户帐号。
        /// <para> 此选项仅当 auth 为 password 时有效。 </para>
        /// </summary>
        public List<Account> accounts { get; set; }

        /// <summary> 用户等级，连接会使用这个用户等级对应的 本地策略。 </summary>
        public int userLevel { get; set; }

        public InboundSocksSettings()
        {
            auth = "noauth";
            ip = "127.0.0.1";
        }
    }

    public class Account
    {
        /// <summary> 用户名，字符串类型。必填。 </summary>
        public string user { get; set; }

        /// <summary> 密码，字符串类型。必填。 </summary>
        public string pass { get; set; }
    }

    #endregion InboundSocksSettings

    #region InboundVMessSettings

    public class InboundVMessSettings
    {
        /// <summary>
        /// 一个数组，代表一组服务端认可的用户.
        /// <para> 其中每一项是一个用户ClientObject。 </para>
        /// </summary>
        public Clients clients { get; set; }

        /// <summary> 可选，clients 的默认配置。仅在配合detour时有效。 </summary>
        public Default @default { get; set; }

        public Detour detour { get; set; }

        public InboundVMessSettings()
        {
        }
    }

    public class Detour
    {
        /// <summary> 一个 inbound 的tag, 指定的 inbound 的必须是使用 VMess 协议的 inbound. </summary>
        public string to { get; set; }
    }

    public class Default
    {
        /// <summary> 用户等级，连接会使用这个用户等级对应的 本地策略。 </summary>
        public int level { get; set; }

        /// <summary> 动态端口的默认alterId，默认值为0。 </summary>
        public int alterId { get; set; }
    }

    public class Clients
    {
        /// <summary> Vmess 的用户 ID，可以是任意小于 30 字节的字符串, 也可以是一个合法的 UUID. </summary>
        public string id { get; set; }

        /// <summary> 用户等级，连接会使用这个用户等级对应的 本地策略。 </summary>
        public int level { get; set; }

        /// <summary> 为了进一步防止被探测，一个用户可以在主 ID 的基础上，再额外生成多个 ID。这里只需要指定额外的 ID 的数量，推荐值为 0 代表启用 VMessAEAD。 最大值 65535。这个值不能超过服务器端所指定的值。 </summary>
        public int alterId { get; set; }

        /// <summary> 用户邮箱地址，用于区分不同用户的流量。 </summary>
        public string email { get; set; }
    }

    #endregion InboundVMessSettings

    #endregion InboundSettings

    #region Outbounds

    public class Outbound
    {
        /// <summary> 此出站连接的标识，用于在其它的配置中定位此连接。 </summary>
        public string tag { get; set; }

        /// <summary> 用于发送数据的 IP 地址，当主机有多个 IP 地址时有效，默认值为 "0.0.0.0"。 </summary>
        public string sendThrough { get; set; }

        /// <summary> 连接协议名称，可选的协议类型见 出站协议。 </summary>
        public string protocol { get; set; }

        public object settings { get; set; }

        /// <summary> 底层传输方式（transport）是当前 Xray 节点和其它节点对接的方式 </summary>
        public StreamSettings streamSettings { get; set; }

        /// <summary> 出站代理配置。当出站代理生效时，此 outbound 的 streamSettings 将不起作用。 </summary>
        public ProxySettings proxySettings { get; set; }

        /// <summary> Mux 相关的具体配置。 </summary>
        public Mux mux { get; set; } = new Mux();

        public Outbound()
        {
            sendThrough = "0.0.0.0";
            streamSettings = new StreamSettings()
            {
                network = "tcp",
                security = "none",
            };
        }
    }

    /// <summary> Mux 功能是在一条 TCP 连接上分发多个 TCP 连接的数据。实现细节详见 Mux.Cool。 Mux 是为了减少 TCP 的握手延迟而设计，而非提高连接的吞吐量。使用 Mux 看视频、下载或者测速通常都有反效果。 Mux 只需要在客户端启用，服务器端自动适配。 </summary>
    public class Mux
    {
        /// <summary> 是否启用 Mux 转发请求，默认值 false。 </summary>
        public bool enabled { get; set; }

        /// <summary> 最大并发连接数。最小值 1，最大值 1024，默认值 8。 </summary>
        public int concurrency { get; set; } = 8;
    }

    public class ProxySettings
    {
        /// <summary> 当指定另一个 outbound 的标识时，此 outbound 发出的数据，将被转发至所指定的 outbound 发出。 </summary>
        public string tag { get; set; }
    }

    public class StreamSettings
    {
        /// <summary>
        /// 连接的数据流所使用的传输方式类型，默认值为 "tcp"
        /// <para> "tcp" | "kcp" | "ws" | "http" | "domainsocket" | "quic" | "grpc" </para>
        /// </summary>
        public string network { get; set; }

        /// <summary>
        /// 是否启用传输层加密，支持的选项有
        /// <para> "none" | "tls" | "xtls" </para>
        /// </summary>
        public string security { get; set; }

        /// <summary> TLS 配置。TLS 由 Golang 提供，通常情况下 TLS 协商的结果为使用 TLS 1.3，不支持 DTLS。 </summary>
        public TlsSettings tlsSettings { get; set; }

        public WebSocketObject wsSettings { get; set; }

        public StreamSettings()
        {
            network = "tcp";
            security = "none";
        }
    }

    public class WebSocketObject
    {
        /// <summary> 仅用于 inbound，指示是否接收 PROXY protocol。 </summary>
        public bool acceptProxyProtocol { get; set; }

        /// <summary> WebSocket 所使用的 HTTP 协议路径，默认值为 "/"。 </summary>
        public string path { get; set; }

        public Headers headers { get; set; }

        public WebSocketObject()
        {
            path = "/";
        }
    }

    public class Headers
    {
        public string Host { get; set; }
    }

    public class TlsSettings
    {
        public string serverName { get; set; }

        public string allowInsecure { get; set; }

        public string fingerprint { get; set; }
    }

    #endregion Outbounds

    #region OutboundVLessSettings

    public class OutboundVLessSettings
    {
        public List<Vnext> vnext { get; set; }
    }

    #endregion OutboundVLessSettings

    #region OutboundVMessSettings

    public class OutboundVMessSettings
    {
        public List<Vnext> vnext { get; set; }
    }

    /// <summary> 公用 </summary>
    public class Vnext
    {
        public string address { get; set; }

        public int port { get; set; }

        public List<User> users { get; set; }
    }

    /// <summary> 公用 https://xtls.github.io/config/outbounds/vmess.html#userobject </summary>
    public class User
    {
        /// <summary> VLESS 的用户 ID，可以是任意小于 30 字节的字符串, 也可以是一个合法的 UUID. </summary>
        public string id { get; set; }

        /// <summary> 流控模式，用于选择 XTLS 的算法。 </summary>
        public string flow { get; set; }

        /// <summary> 需要填 "none"，不能留空。 </summary>
        public string encryption { get; set; }

        /// <summary> 用户等级，连接会使用这个用户等级对应的 本地策略。 </summary>
        public int level { get; set; }

        /// <summary>
        /// 为了进一步防止被探测，一个用户可以在主 ID 的基础上，再额外生成多个 ID。这里只需要指定额外的 ID 的数量，推荐值为 0 代表启用 VMessAEAD。 最大值 65535。这个值不能超过服务器端所指定的值。
        /// <para> 不指定的话，默认值是 0。 </para>
        /// </summary>
        public int alterId { get; set; }

        /// <summary> 加密方式，客户端将使用配置的加密方式发送数据，服务器端自动识别，无需配置。 </summary>
        public string security { get; set; }
    }

    #endregion OutboundVMessSettings

    #region Dns

    public class Dns
    {
        public List<Servers> servers { get; set; }
    }

    public class Servers
    {
        public string address { get; set; }

        public List<string> domains { get; set; }
    }

    #endregion Dns
}