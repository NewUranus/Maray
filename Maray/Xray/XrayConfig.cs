namespace Maray.Xray
{
    /// <summary> https://xtls.github.io/config/#%E6%A6%82%E8%BF%B0 </summary>
    public class XrayConfig
    {
        public List<Inbound> inbounds { get; set; }
        public List<Outbound> outbounds { get; set; }
        public Routing routing { get; set; }
    }

    public class Routing
    {
        public string domainStrategy { get; set; }
        public List<Rule> rules { get; set; }
    }

    public class Rule
    {
        public string type { get; set; }
        public string[] ip { get; set; }
        public string outboundTag { get; set; }
    }

    public class Inbound
    {
        public int port { get; set; }
        public string listen { get; set; }
        public string protocol { get; set; }

        /// <summary> 具体的配置内容，视协议不同而不同。详见每个协议中的 InboundConfigurationObject。 </summary>
        public object settings { get; set; }
    }

    public class Settings
    {
        public bool udp { get; set; }
    }

    public class Outbound
    {
        public string protocol { get; set; }
        public object settings { get; set; }
        public string tag { get; set; }
    }

    public class Vnext
    {
        public string address { get; set; }
        public int port { get; set; }
        public List<User> users { get; set; }
    }

    public class User
    {
        public string id { get; set; }
    }
}