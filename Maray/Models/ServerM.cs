using Maray.Datas;
using Maray.Enum;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Models
{
    public class ServerM
    {
        public int indexId { get; set; }
        public ProtocolType configType { get; set; }
        public int configVersion { get; set; }
        public int sort { get; set; }

        /// <summary> 远程服务器地址 </summary>
        public string address { get; set; }

        /// <summary> 远程服务器端口 </summary>
        public int port { get; set; }

        /// <summary> 远程服务器ID </summary>
        public string id { get; set; }

        /// <summary> 远程服务器额外ID </summary>
        public int alterId { get; set; }

        /// <summary> 本地安全策略 </summary>
        public string security { get; set; }

        /// <summary> tcp,kcp,ws,h2,quic </summary>
        public string network { get; set; }

        /// <summary> 备注或别名 </summary>
        public string remarks { get; set; }

        /// <summary> 伪装类型 </summary>
        public string headerType { get; set; }

        /// <summary> 伪装的域名 </summary>
        public string requestHost { get; set; }

        /// <summary> ws h2 path </summary>
        public string path { get; set; }

        /// <summary> 传输层安全 </summary>
        public string streamSecurity { get; set; }

        /// <summary> 是否允许不安全连接（用于客户端） </summary>
        public string allowInsecure { get; set; }

        public string testResult { get; set; }
        public string subid { get; set; }

        /// <summary> VLESS flow </summary>
        public string flow { get; set; }

        public string groupId { get; set; }

        /// <summary> tls sni </summary>
        public string sni { get; set; }

        /// <summary> tls alpn </summary>
        public List<string> alpn { get; set; }
    }
}