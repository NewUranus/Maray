using Maray.Enum;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Models.Configs
{
    public class CoreInfo
    {
        public CoreType coreType { get; set; }

        public List<string> coreExes { get; set; }

        public string arguments { get; set; }

        public string coreUrl { get; set; }

        public string coreReleaseApiUrl { get; set; }

        public string coreDownloadUrl32 { get; set; }

        public string coreDownloadUrl64 { get; set; }

        public string match { get; set; }
        public string versionArg { get; set; }

        public bool redirectInfo { get; set; }

        public string exePath { get; set; }

        public string exeDirectory { get; set; }
    }
}