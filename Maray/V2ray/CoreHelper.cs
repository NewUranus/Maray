using Maray.Configs;
using Maray.Defines;
using Maray.Enum;
using Maray.Models.Configs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.V2ray
{
    internal class CoreHelper
    {
        public static CoreHelper Instance = new Lazy<CoreHelper>(new CoreHelper()).Value;

        private static List<CoreInfo> coreInfos;

        /// <summary> 获取核心信息 </summary>
        /// <param name="coreType"> </param>
        /// <returns> </returns>
        public CoreInfo GetCoreInfo(CoreType coreType)
        {
            if (coreInfos == null)
            {
                InitCoreInfo();
            }
            return coreInfos.Where(t => t.coreType == coreType).FirstOrDefault();
        }

        private static void InitCoreInfo()
        {
            coreInfos = new List<CoreInfo>();

            coreInfos.Add(new CoreInfo
            {
                coreType = CoreType.v2rayN,
                coreUrl = StringDefines.NUrl,
                coreReleaseApiUrl = StringDefines.NUrl.Replace(@"https://github.com", @"https://api.github.com/repos"),
                coreDownloadUrl32 = StringDefines.NUrl + "/download/{0}/v2rayN.zip",
                coreDownloadUrl64 = StringDefines.NUrl + "/download/{0}/v2rayN.zip",
            });

            coreInfos.Add(new CoreInfo
            {
                coreType = CoreType.v2fly,
                coreExes = new List<string> { "wv2ray", "v2ray" },
                arguments = "",
                coreUrl = StringDefines.v2flyCoreUrl,
                coreReleaseApiUrl = StringDefines.v2flyCoreUrl.Replace(@"https://github.com", @"https://api.github.com/repos"),
                coreDownloadUrl32 = StringDefines.v2flyCoreUrl + "/download/{0}/v2ray-windows-{1}.zip",
                coreDownloadUrl64 = StringDefines.v2flyCoreUrl + "/download/{0}/v2ray-windows-{1}.zip",
                match = "V2Ray",
                versionArg = "-version",
                redirectInfo = true,
            });

            coreInfos.Add(new CoreInfo
            {
                coreType = CoreType.SagerNet,
                coreExes = new List<string> { "SagerNet", "v2ray" },
                arguments = "run",
                coreUrl = StringDefines.SagerNetCoreUrl,
                coreReleaseApiUrl = StringDefines.SagerNetCoreUrl.Replace(@"https://github.com", @"https://api.github.com/repos"),
                coreDownloadUrl32 = StringDefines.SagerNetCoreUrl + "/download/{0}/v2ray-windows-{1}.zip",
                coreDownloadUrl64 = StringDefines.SagerNetCoreUrl + "/download/{0}/v2ray-windows-{1}.zip",
                match = "V2Ray",
                versionArg = "version",
                redirectInfo = true,
            });

            coreInfos.Add(new CoreInfo
            {
                coreType = CoreType.v2fly_v5,
                coreExes = new List<string> { "v2ray" },
                arguments = "run",
                coreUrl = StringDefines.v2flyCoreUrl,
                coreReleaseApiUrl = StringDefines.v2flyCoreUrl.Replace(@"https://github.com", @"https://api.github.com/repos"),
                coreDownloadUrl32 = StringDefines.v2flyCoreUrl + "/download/{0}/v2ray-windows-{1}.zip",
                coreDownloadUrl64 = StringDefines.v2flyCoreUrl + "/download/{0}/v2ray-windows-{1}.zip",
                match = "V2Ray",
                versionArg = "version",
                redirectInfo = true,
            });

            coreInfos.Add(new CoreInfo
            {
                coreType = CoreType.Xray,
                exePath = PathConfig.XrayExePath,
                exeDirectory = PathConfig.GetParentDirectory(PathConfig.XrayExePath),
                coreExes = new List<string> { "xray" },
                arguments = "",
                coreUrl = StringDefines.xrayCoreUrl,
                coreReleaseApiUrl = StringDefines.xrayCoreUrl.Replace(@"https://github.com", @"https://api.github.com/repos"),
                coreDownloadUrl32 = StringDefines.xrayCoreUrl + "/download/{0}/Xray-windows-{1}.zip",
                coreDownloadUrl64 = StringDefines.xrayCoreUrl + "/download/{0}/Xray-windows-{1}.zip",
                match = "Xray",
                versionArg = "-version",
                redirectInfo = true,
            });

            coreInfos.Add(new CoreInfo
            {
                coreType = CoreType.clash,
                coreExes = new List<string> { "clash-windows-amd64-v3", "clash-windows-amd64", "clash-windows-386", "clash" },
                arguments = "-f config.json",
                coreUrl = StringDefines.clashCoreUrl,
                coreReleaseApiUrl = StringDefines.clashCoreUrl.Replace(@"https://github.com", @"https://api.github.com/repos"),
                coreDownloadUrl32 = StringDefines.clashCoreUrl + "/download/{0}/clash-windows-386-{0}.zip",
                coreDownloadUrl64 = StringDefines.clashCoreUrl + "/download/{0}/clash-windows-amd64-{0}.zip",
                match = "v",
                versionArg = "-v",
                redirectInfo = true,
            });

            coreInfos.Add(new CoreInfo
            {
                coreType = CoreType.clash_meta,
                coreExes = new List<string> { "Clash.Meta-windows-amd64-compatible", "Clash.Meta-windows-amd64", "Clash.Meta-windows-386", "Clash.Meta", "clash" },
                arguments = "-f config.json",
                coreUrl = StringDefines.clashMetaCoreUrl,
                coreReleaseApiUrl = StringDefines.clashMetaCoreUrl.Replace(@"https://github.com", @"https://api.github.com/repos"),
                coreDownloadUrl32 = StringDefines.clashMetaCoreUrl + "/download/{0}/Clash.Meta-windows-386-{0}.zip",
                coreDownloadUrl64 = StringDefines.clashMetaCoreUrl + "/download/{0}/Clash.Meta-windows-amd64-compatible-{0}.zip",
                match = "v",
                versionArg = "-v",
                redirectInfo = true,
            });

            coreInfos.Add(new CoreInfo
            {
                coreType = CoreType.hysteria,
                coreExes = new List<string> { "hysteria-windows-amd64", "hysteria-windows-386", "hysteria" },
                arguments = "",
                coreUrl = StringDefines.hysteriaCoreUrl,
                coreReleaseApiUrl = StringDefines.hysteriaCoreUrl.Replace(@"https://github.com", @"https://api.github.com/repos"),
                coreDownloadUrl32 = StringDefines.hysteriaCoreUrl + "/download/{0}/hysteria-windows-386.exe",
                coreDownloadUrl64 = StringDefines.hysteriaCoreUrl + "/download/{0}/hysteria-windows-amd64.exe",
                redirectInfo = true,
            });

            coreInfos.Add(new CoreInfo
            {
                coreType = CoreType.naiveproxy,
                coreExes = new List<string> { "naiveproxy", "naive" },
                arguments = "config.json",
                coreUrl = StringDefines.naiveproxyCoreUrl,
                redirectInfo = false,
            });

            coreInfos.Add(new CoreInfo
            {
                coreType = CoreType.tuic,
                coreExes = new List<string> { "tuic-client", "tuic" },
                arguments = "-c config.json",
                coreUrl = StringDefines.tuicCoreUrl,
                redirectInfo = true,
            });

            coreInfos.Add(new CoreInfo
            {
                coreType = CoreType.sing_box,
                coreExes = new List<string> { "sing-box-client", "sing-box" },
                arguments = "run",
                coreUrl = StringDefines.singboxCoreUrl,
                redirectInfo = true,
            });
        }
    }
}