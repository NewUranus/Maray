using Maray.Defines;
using Maray.Enum;
using Maray.Models;

using System.Collections.Specialized;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Web;

namespace Maray.Helpers
{
    internal class ServerHelper
    {
        #region ParseUrl

        private static readonly Regex StdVmessUserInfo = new Regex(
       @"^(?<network>[a-z]+)(\+(?<streamSecurity>[a-z]+))?:(?<id>[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12})$");

        public static ServerM ParseUrlToServerItem(string result)
        {
            ServerM vmessItem = new ServerM();

            if (result.StartsWith(StringDefines.vmessProtocol))
            {
                int indexSplit = result.IndexOf("?");
                if (indexSplit > 0)
                {
                    vmessItem = ResolveStdVmess(result) ?? ResolveVmess4Kitsunebi(result);
                }
                else
                {
                    vmessItem = ResolveVmess(result);
                }

                //ConfigHandler.UpgradeServerVersion(ref vmessItem);
            }
            else if (result.StartsWith(StringDefines.ssProtocol))
            {
                vmessItem = ResolveSSLegacy(result) ?? ResolveSip002(result);
                if (vmessItem == null)
                {
                    return null;
                }
                if (vmessItem.address.Length == 0 || vmessItem.port == 0 || vmessItem.security.Length == 0 || vmessItem.id.Length == 0)
                {
                    return null;
                }

                vmessItem.configType = ProtocolEnum.Shadowsocks;
            }
            else if (result.StartsWith(StringDefines.socksProtocol))
            {
                vmessItem = ResolveSocksNew(result) ?? ResolveSocks(result);
                if (vmessItem == null)
                {
                    return null;
                }
                if (vmessItem.address.Length == 0 || vmessItem.port == 0)
                {
                    return null;
                }

                vmessItem.configType = ProtocolEnum.Socks;
            }
            else if (result.StartsWith(StringDefines.trojanProtocol))
            {
                vmessItem = ResolveTrojan(result);
            }
            else if (result.StartsWith(StringDefines.vlessProtocol))
            {
                vmessItem = ResolveStdVLESS(result);

                //ConfigHandler.UpgradeServerVersion(ref vmessItem);
            }
            else
            {
                Shell.Current.DisplayAlert("Waring", "ParseUrlToServerItem fail!", "OK");
                return null;
            }

            return vmessItem;
        }

        private static ServerM ResolveVmess(string result)
        {
            var vmessItem = new ServerM
            {
                configType = ProtocolEnum.VMess
            };

            result = result.Substring(StringDefines.vmessProtocol.Length);
            result = Base64Helper.Base64Decode(result);

            //转成Json
            VmessQRCode vmessQRCode = JsonHelper.FromJsonString<VmessQRCode>(result);
            if (vmessQRCode == null)
            {
                return null;
            }

            vmessItem.network = StringDefines.DefaultNetwork;
            vmessItem.headerType = StringDefines.None;

            vmessItem.configVersion = NumberHelper.ToInt(vmessQRCode.v);
            vmessItem.remarks = StringHelper.ToString(vmessQRCode.ps);
            vmessItem.address = StringHelper.ToString(vmessQRCode.add);
            vmessItem.port = NumberHelper.ToInt(vmessQRCode.port);
            vmessItem.id = StringHelper.ToString(vmessQRCode.id);
            vmessItem.alterId = NumberHelper.ToInt(vmessQRCode.aid);
            vmessItem.security = StringHelper.ToString(vmessQRCode.scy);

            vmessItem.security = !string.IsNullOrEmpty(vmessQRCode.scy) ? vmessQRCode.scy : StringDefines.DefaultSecurity;
            if (!string.IsNullOrEmpty(vmessQRCode.net))
            {
                vmessItem.network = vmessQRCode.net;
            }
            if (!string.IsNullOrEmpty(vmessQRCode.type))
            {
                vmessItem.headerType = vmessQRCode.type;
            }

            vmessItem.requestHost = StringHelper.ToString(vmessQRCode.host);
            vmessItem.path = StringHelper.ToString(vmessQRCode.path);
            vmessItem.streamSecurity = StringHelper.ToString(vmessQRCode.tls);
            vmessItem.sni = StringHelper.ToString(vmessQRCode.sni);
            vmessItem.alpn = StringHelper.String2List(vmessQRCode.alpn);

            return vmessItem;
        }

        private static ServerM ResolveStdVLESS(string result)
        {
            ServerM item = new ServerM
            {
                configType = ProtocolEnum.VLESS,
                security = "none"
            };

            Uri url = new Uri(result);

            item.address = url.IdnHost;
            item.port = url.Port;
            item.remarks = url.GetComponents(UriComponents.Fragment, UriFormat.Unescaped);
            item.id = url.UserInfo;

            var query = HttpUtility.ParseQueryString(url.Query);
            item.security = query["encryption"] ?? "none";
            item.streamSecurity = query["security"] ?? "";
            ResolveStdTransport(query, ref item);

            return item;
        }

        private static ServerM ResolveTrojan(string result)
        {
            ServerM item = new ServerM
            {
                configType = ProtocolEnum.Trojan
            };

            Uri url = new Uri(result);

            item.address = url.IdnHost;
            item.port = url.Port;
            item.remarks = url.GetComponents(UriComponents.Fragment, UriFormat.Unescaped);
            item.id = url.UserInfo;

            var query = HttpUtility.ParseQueryString(url.Query);
            ResolveStdTransport(query, ref item);

            return item;
        }

        private static int ResolveStdTransport(NameValueCollection query, ref ServerM item)
        {
            item.flow = query["flow"] ?? "";
            item.streamSecurity = query["security"] ?? "";
            item.sni = query["sni"] ?? "";
            item.alpn = StringHelper.String2List(HttpUtility.UrlDecode(query["alpn"] ?? ""));
            item.network = query["type"] ?? "tcp";
            switch (item.network)
            {
                case "tcp":
                    item.headerType = query["headerType"] ?? "none";
                    item.requestHost = HttpUtility.UrlDecode(query["host"] ?? "");

                    break;

                case "kcp":
                    item.headerType = query["headerType"] ?? "none";
                    item.path = HttpUtility.UrlDecode(query["seed"] ?? "");
                    break;

                case "ws":
                    item.requestHost = HttpUtility.UrlDecode(query["host"] ?? "");
                    item.path = HttpUtility.UrlDecode(query["path"] ?? "/");
                    break;

                case "http":
                case "h2":
                    item.network = "h2";
                    item.requestHost = HttpUtility.UrlDecode(query["host"] ?? "");
                    item.path = HttpUtility.UrlDecode(query["path"] ?? "/");
                    break;

                case "quic":
                    item.headerType = query["headerType"] ?? "none";
                    item.requestHost = query["quicSecurity"] ?? "none";
                    item.path = HttpUtility.UrlDecode(query["key"] ?? "");
                    break;

                case "grpc":
                    item.path = HttpUtility.UrlDecode(query["serviceName"] ?? "");
                    item.headerType = HttpUtility.UrlDecode(query["mode"] ?? StringDefines.GrpcgunMode);
                    break;

                default:
                    break;
            }
            return 0;
        }

        private static ServerM ResolveStdVmess(string result)
        {
            ServerM i = new ServerM
            {
                configType = ProtocolEnum.VMess,
                security = "auto"
            };

            Uri u = new Uri(result);

            i.address = u.IdnHost;
            i.port = u.Port;
            i.remarks = u.GetComponents(UriComponents.Fragment, UriFormat.Unescaped);
            var q = HttpUtility.ParseQueryString(u.Query);

            var m = StdVmessUserInfo.Match(u.UserInfo);
            if (!m.Success) return null;

            i.id = m.Groups["id"].Value;

            if (m.Groups["streamSecurity"].Success)
            {
                i.streamSecurity = m.Groups["streamSecurity"].Value;
            }
            switch (i.streamSecurity)
            {
                case "tls":
                    // TODO tls config
                    break;

                default:
                    if (!string.IsNullOrWhiteSpace(i.streamSecurity))
                        return null;
                    break;
            }

            i.network = m.Groups["network"].Value;
            switch (i.network)
            {
                case "tcp":
                    string t1 = q["type"] ?? "none";
                    i.headerType = t1;
                    // TODO http option

                    break;

                case "kcp":
                    i.headerType = q["type"] ?? "none";
                    // TODO kcp seed
                    break;

                case "ws":
                    string p1 = q["path"] ?? "/";
                    string h1 = q["host"] ?? "";
                    i.requestHost = HttpUtility.UrlDecode(h1);
                    i.path = p1;
                    break;

                case "http":
                case "h2":
                    i.network = "h2";
                    string p2 = q["path"] ?? "/";
                    string h2 = q["host"] ?? "";
                    i.requestHost = HttpUtility.UrlDecode(h2);
                    i.path = p2;
                    break;

                case "quic":
                    string s = q["security"] ?? "none";
                    string k = q["key"] ?? "";
                    string t3 = q["type"] ?? "none";
                    i.headerType = t3;
                    i.requestHost = HttpUtility.UrlDecode(s);
                    i.path = k;
                    break;

                default:
                    return null;
            }

            return i;
        }

        private static ServerM ResolveVmess4Kitsunebi(string result)
        {
            ServerM vmessItem = new ServerM
            {
                configType = ProtocolEnum.VMess
            };
            result = result.Substring(StringDefines.vmessProtocol.Length);
            int indexSplit = result.IndexOf("?");
            if (indexSplit > 0)
            {
                result = result.Substring(0, indexSplit);
            }
            result = Base64Helper.Base64Decode(result);

            string[] arr1 = result.Split('@');
            if (arr1.Length != 2)
            {
                return null;
            }
            string[] arr21 = arr1[0].Split(':');
            string[] arr22 = arr1[1].Split(':');
            if (arr21.Length != 2 || arr21.Length != 2)
            {
                return null;
            }

            vmessItem.address = arr22[0];
            vmessItem.port = NumberHelper.ToInt(arr22[1]);
            vmessItem.security = arr21[0];
            vmessItem.id = arr21[1];

            vmessItem.network = StringDefines.DefaultNetwork;
            vmessItem.headerType = StringDefines.None;
            vmessItem.remarks = "Alien";

            return vmessItem;
        }

        private static readonly Regex UrlFinder = new Regex(@"ss://(?<base64>[A-Za-z0-9+-/=_]+)(?:#(?<tag>\S+))?", RegexOptions.IgnoreCase);
        private static readonly Regex DetailsParser = new Regex(@"^((?<method>.+?):(?<password>.*)@(?<hostname>.+?):(?<port>\d+?))$", RegexOptions.IgnoreCase);

        private static ServerM ResolveSSLegacy(string result)
        {
            var match = UrlFinder.Match(result);
            if (!match.Success)
                return null;

            ServerM server = new ServerM();
            var base64 = match.Groups["base64"].Value.TrimEnd('/');
            var tag = match.Groups["tag"].Value;
            if (!string.IsNullOrEmpty(tag))
            {
                server.remarks = HttpUtility.UrlDecode(tag);
            }
            Match details;
            try
            {
                details = DetailsParser.Match(Base64Helper.Base64Decode(base64));
            }
            catch (FormatException)
            {
                return null;
            }
            if (!details.Success)
                return null;
            server.security = details.Groups["method"].Value;
            server.id = details.Groups["password"].Value;
            server.address = details.Groups["hostname"].Value;
            server.port = int.Parse(details.Groups["port"].Value);
            return server;
        }

        private static ServerM ResolveSocksNew(string result)
        {
            Uri parsedUrl;
            try
            {
                parsedUrl = new Uri(result);
            }
            catch (UriFormatException)
            {
                return null;
            }
            ServerM server = new ServerM
            {
                remarks = parsedUrl.GetComponents(UriComponents.Fragment, UriFormat.Unescaped),
                address = parsedUrl.IdnHost,
                port = parsedUrl.Port,
            };

            // parse base64 UserInfo
            string rawUserInfo = parsedUrl.GetComponents(UriComponents.UserInfo, UriFormat.Unescaped);
            string userInfo = Base64Helper.Base64Decode(rawUserInfo);
            string[] userInfoParts = userInfo.Split(new[] { ':' }, 2);
            if (userInfoParts.Length == 2)
            {
                server.security = userInfoParts[0];
                server.id = userInfoParts[1];
            }

            return server;
        }

        private static ServerM ResolveSocks(string result)
        {
            ServerM vmessItem = new ServerM
            {
                configType = ProtocolEnum.Socks
            };
            result = result.Substring(StringDefines.socksProtocol.Length);
            //remark
            int indexRemark = result.IndexOf("#");
            if (indexRemark > 0)
            {
                try
                {
                    vmessItem.remarks = HttpUtility.UrlDecode(result.Substring(indexRemark + 1, result.Length - indexRemark - 1));
                }
                catch { }
                result = result.Substring(0, indexRemark);
            }
            //part decode
            int indexS = result.IndexOf("@");
            if (indexS > 0)
            {
            }
            else
            {
                result = Base64Helper.Base64Decode(result);
            }

            string[] arr1 = result.Split('@');
            if (arr1.Length != 2)
            {
                return null;
            }
            string[] arr21 = arr1[0].Split(':');
            //string[] arr22 = arr1[1].Split(':');
            int indexPort = arr1[1].LastIndexOf(":");
            if (arr21.Length != 2 || indexPort < 0)
            {
                return null;
            }
            vmessItem.address = arr1[1].Substring(0, indexPort);
            vmessItem.port = NumberHelper.ToInt(arr1[1].Substring(indexPort + 1, arr1[1].Length - (indexPort + 1)));
            vmessItem.security = arr21[0];
            vmessItem.id = arr21[1];

            return vmessItem;
        }

        private static ServerM ResolveSip002(string result)
        {
            Uri parsedUrl;
            try
            {
                parsedUrl = new Uri(result);
            }
            catch (UriFormatException)
            {
                return null;
            }
            ServerM server = new ServerM
            {
                remarks = parsedUrl.GetComponents(UriComponents.Fragment, UriFormat.Unescaped),
                address = parsedUrl.IdnHost,
                port = parsedUrl.Port,
            };
            string rawUserInfo = parsedUrl.GetComponents(UriComponents.UserInfo, UriFormat.UriEscaped);
            //2022-blake3
            if (rawUserInfo.Contains(":"))
            {
                string[] userInfoParts = rawUserInfo.Split(new[] { ':' }, 2);
                if (userInfoParts.Length != 2)
                {
                    return null;
                }
                server.security = userInfoParts[0];
                server.id = HttpUtility.UrlDecode(userInfoParts[1]);
            }
            else
            {
                // parse base64 UserInfo
                string userInfo = Base64Helper.Base64Decode(rawUserInfo);
                string[] userInfoParts = userInfo.Split(new[] { ':' }, 2);
                if (userInfoParts.Length != 2)
                {
                    return null;
                }
                server.security = userInfoParts[0];
                server.id = userInfoParts[1];
            }

            NameValueCollection queryParameters = HttpUtility.ParseQueryString(parsedUrl.Query);
            if (queryParameters["plugin"] != null)
            {
                //obfs-host exists
                var obfsHost = queryParameters["plugin"].Split(';').FirstOrDefault(t => t.Contains("obfs-host"));
                if (queryParameters["plugin"].Contains("obfs=http") && !string.IsNullOrEmpty(obfsHost))
                {
                    obfsHost = obfsHost.Replace("obfs-host=", "");
                    server.network = StringDefines.DefaultNetwork;
                    server.headerType = StringDefines.TcpHeaderHttp;
                    server.requestHost = obfsHost;
                }
                else
                {
                    return null;
                }
            }

            return server;
        }

        #endregion ParseUrl

        /// <summary> Ping </summary>
        /// <param name="host"> </param>
        /// <returns> </returns>
        public static long Ping(string host)
        {
            long roundtripTime = -1;
            try
            {
                int timeout = 30;
                int echoNum = 2;
                Ping pingSender = new Ping();
                for (int i = 0; i < echoNum; i++)
                {
                    PingReply reply = pingSender.Send(host, timeout);
                    if (reply.Status == IPStatus.Success)
                    {
                        if (reply.RoundtripTime < 0)
                        {
                            continue;
                        }
                        if (roundtripTime < 0 || reply.RoundtripTime < roundtripTime)
                        {
                            roundtripTime = reply.RoundtripTime;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return roundtripTime;
        }
    }
}