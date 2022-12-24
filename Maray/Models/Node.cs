using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Models
{
    public class Node
    {
        public string add { get; set; }
        public int aid { get; set; }
        public string host { get; set; }
        public string id { get; set; }
        public string net { get; set; }
        public string path { get; set; }
        public int port { get; set; }
        public string ps { get; set; }
        public string tls { get; set; }
        public string type { get; set; }
        public string security { get; set; }

        [JsonProperty("skip-cert-verify")]
        public bool skipcertverify { get; set; }
        public string sni { get; set; }
    }
}
