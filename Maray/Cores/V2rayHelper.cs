using Maray.Configs;
using Maray.Cores;
using Maray.Defines;
using Maray.Enum;
using Maray.ExtensionMethods;
using Maray.Helpers;
using Maray.Models;
using Maray.Models.Configs;
using Maray.Models.V2rayConfig;
using Maray.Models.XrayConfigs;
using Maray.Services;

namespace Maray.V2ray
{
    /// <summary> 底层隔离 </summary>
    internal class V2rayHelper
    {
        private CoreInfo coreInfo;

        public bool InitCore(MarayConfigM marayConfig)
        {
            if (!SetCore(marayConfig, marayConfig.DefaultServer))
            {
                NLogHelper.WriteExceptionLog("CheckServerSettings");
                return false;
            }

            var res = GenerateClientConfig(marayConfig);
            if (!res)
            {
                NLogHelper.WriteLog("GenerateClientConfig fail.");
                return false;
            }
            else
            {
            }

            return true;
        }

        private void LoadCore(MarayConfigM marayConfig)
        {
            //start a socks service
            if (marayConfig.DefaultServer.configType == ProtocolEnum.Custom && marayConfig.DefaultServer.preSocksPort > 0)
            {
                var itemSocks = new ServerM()
                {
                    configType = ProtocolEnum.Socks,
                    address = StringDefines.Loopback,
                    port = marayConfig.DefaultServer.preSocksPort
                };

                //var config2 = GenerateClientConfig(serverM, itemSocks);
                //if (config2)
                //{
                //    V2rayStartNew();
                //}
            }
        }

        /// <summary> V2ray重启 </summary>
        private void V2rayRestart(V2rayConfig config)
        {
            V2rayStop();
            StartCore();
        }

        /// <summary> V2ray启动 </summary>
        public void StartCore()
        {
            ProcessHelper processHelper = new ProcessHelper(null);
            processHelper.StartCore(coreInfo);
        }

        private void V2rayStartNew()
        {
        }

        public void V2rayStop()
        {
        }

        private bool SetCore(MarayConfigM config, ServerM item)
        {
            if (item == null)
            {
                return false;
            }
            var coreType = GetCoreType(config, item, item.configType);

            coreInfo = CoreHelper.Instance.GetCoreInfo(coreType);

            if (coreInfo == null)
            {
                return false;
            }
            return true;
        }

        /// <summary> 生成core.exe的配置文件 </summary>
        /// <param name="node">     </param>
        /// <param name="fileName"> </param>
        /// <param name="msg">      </param>
        /// <returns> </returns>
        private static bool GenerateClientConfig(MarayConfigM config)
        {
            try
            {
                var coreType = GetCoreType(config, config.DefaultServer, config.DefaultServer.configType);
                //var coreType = LazyConfig.Instance.GetCoreType(node, node.configType);
                switch (coreType)
                {
                    case CoreType.v2fly:

                        GenerateClientConfigContent(config.DefaultServer, true);
                        break;

                    case CoreType.SagerNet:

                        break;

                    case CoreType.Xray:

                        XrayHelper.GenerateClientXrayConfig(config);
                        break;

                    case CoreType.v2fly_v5:
                        break;

                    case CoreType.clash:
                        break;

                    case CoreType.clash_meta:

                        break;

                    default:
                        return false;
                }

                return true;

                var localconfig = GenerateClientConfigContent(config.DefaultServer, false);
                JsonHelper.WriteToJsonFile(PathConfig.v2rayExeConfigPath, localconfig);

                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.WriteExceptionLog(ex);
                return false;
            }
        }

        private static V2rayConfig GenerateClientConfigContent(ServerM node, bool blExport)
        {
            try
            {
                //取得默认配置

                var v2rayConfig = DownloadHelper.GetEmbedText<V2rayConfig>(PathConfig.v2raySampleClient);

                var config = Helpers.ServiceProviderHelper.GetService<ConfigService>().GetMarayConfig();

                //开始修改配置

                //log
                log(config, ref v2rayConfig);

                //本地端口
                inbound(config, ref v2rayConfig);

                //路由
                routing(config, ref v2rayConfig);

                //outbound
                outbound(config, node, ref v2rayConfig);

                //dns
                dns(config, ref v2rayConfig);

                //stat
                statistic(config, ref v2rayConfig);

                NLogHelper.WriteLog("GenerateClientConfigContent success.");
                return v2rayConfig;
            }
            catch (Exception ex)
            {
                NLogHelper.WriteExceptionLog(ex);
                return null;
            }
        }

        #region log

        /// <summary> 日志 </summary>
        /// <param name="config">      </param>
        /// <param name="v2rayConfig"> </param>
        /// <returns> </returns>
        private static void log(MarayConfigM marayConfigM, ref V2rayConfig v2rayConfig)
        {
            v2rayConfig.log.loglevel = marayConfigM.Loglevel;
            v2rayConfig.log.access = PathConfig.LogPath;
            v2rayConfig.log.error = PathConfig.LogPath;
        }

        #endregion log

        #region inbound

        /// <summary> 本地端口 </summary>
        /// <param name="config">      </param>
        /// <param name="v2rayConfig"> </param>
        /// <returns> </returns>
        private static void inbound(MarayConfigM config, ref V2rayConfig v2rayConfig)
        {
            try
            {
                v2rayConfig.inbounds = new List<Inbounds>();

                Inbounds inbound = GetInbound(config.inbound[0], StringDefines.InboundSocks, 0, true);
                v2rayConfig.inbounds.Add(inbound);

                //http
                Inbounds inbound2 = GetInbound(config.inbound[0], StringDefines.InboundHttp, 1, false);
                v2rayConfig.inbounds.Add(inbound2);

                if (config.inbound[0].allowLANConn)
                {
                    Inbounds inbound3 = GetInbound(config.inbound[0], StringDefines.InboundSocks2, 2, true);
                    inbound3.listen = "0.0.0.0";
                    v2rayConfig.inbounds.Add(inbound3);

                    Inbounds inbound4 = GetInbound(config.inbound[0], StringDefines.InboundHttp2, 3, false);
                    inbound4.listen = "0.0.0.0";
                    v2rayConfig.inbounds.Add(inbound4);

                    //auth
                    if (!string.IsNullOrEmpty(config.inbound[0].user) && !string.IsNullOrEmpty(config.inbound[0].pass))
                    {
                        inbound3.settings.auth = "password";
                        inbound3.settings.accounts = new List<AccountsItem> { new AccountsItem() { user = config.inbound[0].user, pass = config.inbound[0].pass } };

                        inbound4.settings.auth = "password";
                        inbound4.settings.accounts = new List<AccountsItem> { new AccountsItem() { user = config.inbound[0].user, pass = config.inbound[0].pass } };
                    }
                }
            }
            catch (Exception ex)
            {
                NLogHelper.WriteExceptionLog(ex);
            }
        }

        private static Inbounds GetInbound(InItem inItem, string tag, int offset, bool bSocks)
        {
            var inbound = DownloadHelper.GetEmbedText<Inbounds>(PathConfig.v2raySampleInbound);

            inbound.tag = tag;
            inbound.port = inItem.localPort + offset;
            inbound.protocol = bSocks ? StringDefines.InboundSocks : StringDefines.InboundHttp;
            inbound.settings.udp = inItem.udpEnabled;
            inbound.sniffing.enabled = inItem.sniffingEnabled;

            return inbound;
        }

        #endregion inbound

        #region routing

        /// <summary> 路由 </summary>
        /// <param name="config">      </param>
        /// <param name="v2rayConfig"> </param>
        /// <returns> </returns>
        private static void routing(MarayConfigM config, ref V2rayConfig v2rayConfig)
        {
            try
            {
                if (v2rayConfig.routing != null
                  && v2rayConfig.routing.rules != null)
                {
                    v2rayConfig.routing.domainStrategy = config.domainStrategy;
                    v2rayConfig.routing.domainMatcher = string.IsNullOrEmpty(config.domainMatcher) ? null : config.domainMatcher;

                    if (config.enableRoutingAdvanced)
                    {
                        if (config.routings != null && config.routingIndex < config.routings.Count)
                        {
                            foreach (var item in config.routings[config.routingIndex].rules)
                            {
                                if (item.enabled)
                                {
                                    routingUserRule(item, ref v2rayConfig);
                                }
                            }
                        }
                    }
                    else
                    {
                        var lockedItem = GetLockedRoutingItem(ref config);
                        if (lockedItem != null)
                        {
                            foreach (var item in lockedItem.rules)
                            {
                                routingUserRule(item, ref v2rayConfig);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NLogHelper.WriteExceptionLog(ex);
            }
        }

        public static RoutingItem GetLockedRoutingItem(ref MarayConfigM config)
        {
            if (config.routings == null)
            {
                return null;
            }
            return config.routings.Find(it => it.locked == true);
        }

        private static void routingUserRule(RulesItem rules, ref V2rayConfig v2rayConfig)
        {
            try
            {
                if (rules == null)
                {
                    return;
                }
                if (string.IsNullOrEmpty(rules.port))
                {
                    rules.port = null;
                }
                if (rules.domain != null && rules.domain.Count == 0)
                {
                    rules.domain = null;
                }
                if (rules.ip != null && rules.ip.Count == 0)
                {
                    rules.ip = null;
                }
                if (rules.protocol != null && rules.protocol.Count == 0)
                {
                    rules.protocol = null;
                }

                var hasDomainIp = false;
                if (rules.domain != null && rules.domain.Count > 0)
                {
                    var it = JsonHelper.DeepCopy(rules);
                    it.ip = null;
                    it.type = "field";
                    for (int k = it.domain.Count - 1; k >= 0; k--)
                    {
                        if (it.domain[k].StartsWith("#"))
                        {
                            it.domain.RemoveAt(k);
                        }
                        it.domain[k] = it.domain[k].Replace(StringDefines.RoutingRuleComma, ",");
                    }
                    //if (Utils.IsNullOrEmpty(it.port))
                    //{
                    //    it.port = null;
                    //}
                    //if (it.protocol != null && it.protocol.Count == 0)
                    //{
                    //    it.protocol = null;
                    //}
                    v2rayConfig.routing.rules.Add(it);
                    hasDomainIp = true;
                }
                if (rules.ip != null && rules.ip.Count > 0)
                {
                    var it = JsonHelper.DeepCopy(rules);
                    it.domain = null;
                    it.type = "field";
                    //if (Utils.IsNullOrEmpty(it.port))
                    //{
                    //    it.port = null;
                    //}
                    //if (it.protocol != null && it.protocol.Count == 0)
                    //{
                    //    it.protocol = null;
                    //}
                    v2rayConfig.routing.rules.Add(it);
                    hasDomainIp = true;
                }
                if (!hasDomainIp)
                {
                    if (!string.IsNullOrEmpty(rules.port))
                    {
                        var it = JsonHelper.DeepCopy(rules);
                        //it.domain = null;
                        //it.ip = null;
                        //if (it.protocol != null && it.protocol.Count == 0)
                        //{
                        //    it.protocol = null;
                        //}
                        it.type = "field";
                        v2rayConfig.routing.rules.Add(it);
                    }
                    else if (rules.protocol != null && rules.protocol.Count > 0)
                    {
                        var it = JsonHelper.DeepCopy(rules);
                        //it.domain = null;
                        //it.ip = null;
                        //if (Utils.IsNullOrEmpty(it.port))
                        //{
                        //    it.port = null;
                        //}
                        it.type = "field";
                        v2rayConfig.routing.rules.Add(it);
                    }
                }
            }
            catch (Exception ex)
            {
                NLogHelper.WriteExceptionLog(ex);
            }
        }

        #endregion routing

        #region outbound

        /// <summary> vmess协议服务器配置 </summary>
        /// <param name="node">        </param>
        /// <param name="v2rayConfig"> </param>
        /// <returns> </returns>
        private static void outbound(MarayConfigM config, ServerM node, ref V2rayConfig v2rayConfig)
        {
            try
            {
                Outbounds outbound = v2rayConfig.outbounds[0];
                if (node.configType == Enum.ProtocolEnum.VMess)
                {
                    VnextItem vnextItem;
                    if (outbound.settings.vnext.Count <= 0)
                    {
                        vnextItem = new VnextItem();
                        outbound.settings.vnext.Add(vnextItem);
                    }
                    else
                    {
                        vnextItem = outbound.settings.vnext[0];
                    }
                    //远程服务器地址和端口
                    vnextItem.address = node.address;
                    vnextItem.port = node.port;

                    UsersItem usersItem;
                    if (vnextItem.users.Count <= 0)
                    {
                        usersItem = new UsersItem();
                        vnextItem.users.Add(usersItem);
                    }
                    else
                    {
                        usersItem = vnextItem.users[0];
                    }
                    //远程服务器用户ID
                    usersItem.id = node.id;
                    usersItem.alterId = node.alterId;
                    usersItem.email = StringDefines.userEMail;
                    if (StringDefines.vmessSecuritys.Contains(node.security))
                    {
                        usersItem.security = node.security;
                    }
                    else
                    {
                        usersItem.security = StringDefines.DefaultSecurity;
                    }

                    //Mux
                    outbound.mux.enabled = config.muxEnabled;
                    outbound.mux.concurrency = config.muxEnabled ? 8 : -1;

                    boundStreamSettings(config, node, "out", outbound.streamSettings);

                    outbound.protocol = StringDefines.vmessProtocolLite;
                    outbound.settings.servers = null;
                }
                else if (node.configType == Enum.ProtocolEnum.Shadowsocks)
                {
                    ServersItem serversItem;
                    if (outbound.settings.servers.Count <= 0)
                    {
                        serversItem = new ServersItem();
                        outbound.settings.servers.Add(serversItem);
                    }
                    else
                    {
                        serversItem = outbound.settings.servers[0];
                    }
                    //远程服务器地址和端口
                    serversItem.address = node.address;
                    serversItem.port = node.port;
                    serversItem.password = node.id;
                    serversItem.method = GetShadowsocksSecuritys(config, node).Contains(node.security) ? node.security : "none";

                    serversItem.ota = false;
                    serversItem.level = 1;

                    outbound.mux.enabled = false;
                    outbound.mux.concurrency = -1;

                    boundStreamSettings(config, node, "out", outbound.streamSettings);

                    outbound.protocol = StringDefines.ssProtocolLite;
                    outbound.settings.vnext = null;
                }
                else if (node.configType == ProtocolEnum.Socks)
                {
                    ServersItem serversItem;
                    if (outbound.settings.servers.Count <= 0)
                    {
                        serversItem = new ServersItem();
                        outbound.settings.servers.Add(serversItem);
                    }
                    else
                    {
                        serversItem = outbound.settings.servers[0];
                    }
                    //远程服务器地址和端口
                    serversItem.address = node.address;
                    serversItem.port = node.port;
                    serversItem.method = null;
                    serversItem.password = null;

                    if (!string.IsNullOrEmpty(node.security)
                        && !string.IsNullOrEmpty(node.id))
                    {
                        SocksUsersItem socksUsersItem = new SocksUsersItem
                        {
                            user = node.security,
                            pass = node.id,
                            level = 1
                        };

                        serversItem.users = new List<SocksUsersItem>() { socksUsersItem };
                    }

                    outbound.mux.enabled = false;
                    outbound.mux.concurrency = -1;

                    outbound.protocol = StringDefines.socksProtocolLite;
                    outbound.settings.vnext = null;
                }
                else if (node.configType == ProtocolEnum.VLESS)
                {
                    VnextItem vnextItem;
                    if (outbound.settings.vnext.Count <= 0)
                    {
                        vnextItem = new VnextItem();
                        outbound.settings.vnext.Add(vnextItem);
                    }
                    else
                    {
                        vnextItem = outbound.settings.vnext[0];
                    }
                    //远程服务器地址和端口
                    vnextItem.address = node.address;
                    vnextItem.port = node.port;

                    UsersItem usersItem;
                    if (vnextItem.users.Count <= 0)
                    {
                        usersItem = new UsersItem();
                        vnextItem.users.Add(usersItem);
                    }
                    else
                    {
                        usersItem = vnextItem.users[0];
                    }
                    //远程服务器用户ID
                    usersItem.id = node.id;
                    usersItem.flow = string.Empty;
                    usersItem.email = StringDefines.userEMail;
                    usersItem.encryption = node.security;

                    //Mux
                    outbound.mux.enabled = config.muxEnabled;
                    outbound.mux.concurrency = config.muxEnabled ? 8 : -1;

                    boundStreamSettings(config, node, "out", outbound.streamSettings);

                    //if xtls
                    if (node.streamSecurity == StringDefines.StreamSecurityX)
                    {
                        if (string.IsNullOrEmpty(node.flow))
                        {
                            usersItem.flow = StringDefines.xtlsFlows[1];
                        }
                        else
                        {
                            usersItem.flow = node.flow.Replace("splice", "direct");
                        }

                        outbound.mux.enabled = false;
                        outbound.mux.concurrency = -1;
                    }
                    else if (node.streamSecurity == StringDefines.StreamSecurity)
                    {
                        if (!string.IsNullOrEmpty(node.flow))
                        {
                            usersItem.flow = node.flow;

                            outbound.mux.enabled = false;
                            outbound.mux.concurrency = -1;
                        }
                    }

                    outbound.protocol = StringDefines.vlessProtocolLite;
                    outbound.settings.servers = null;
                }
                else if (node.configType == ProtocolEnum.Trojan)
                {
                    ServersItem serversItem;
                    if (outbound.settings.servers.Count <= 0)
                    {
                        serversItem = new ServersItem();
                        outbound.settings.servers.Add(serversItem);
                    }
                    else
                    {
                        serversItem = outbound.settings.servers[0];
                    }
                    //远程服务器地址和端口
                    serversItem.address = node.address;
                    serversItem.port = node.port;
                    serversItem.password = node.id;
                    serversItem.flow = string.Empty;

                    serversItem.ota = false;
                    serversItem.level = 1;

                    //if xtls
                    if (node.streamSecurity == StringDefines.StreamSecurityX)
                    {
                        if (string.IsNullOrEmpty(node.flow))
                        {
                            serversItem.flow = StringDefines.xtlsFlows[1];
                        }
                        else
                        {
                            serversItem.flow = node.flow.Replace("splice", "direct");
                        }

                        outbound.mux.enabled = false;
                        outbound.mux.concurrency = -1;
                    }

                    outbound.mux.enabled = false;
                    outbound.mux.concurrency = -1;

                    boundStreamSettings(config, node, "out", outbound.streamSettings);

                    outbound.protocol = StringDefines.trojanProtocolLite;
                    outbound.settings.vnext = null;
                }
            }
            catch (Exception ex)
            {
                NLogHelper.WriteExceptionLog(ex);
            }
        }

        /// <summary> 底层传输配置 </summary>
        /// <param name="node">           </param>
        /// <param name="iobound">        </param>
        /// <param name="streamSettings"> </param>
        /// <returns> </returns>
        private static int boundStreamSettings(MarayConfigM config, ServerM node, string iobound, Models.V2rayConfig.StreamSettings streamSettings)
        {
            try
            {
                streamSettings.network = node.GetNetwork();
                string host = node.requestHost.TrimEx();
                string sni = node.sni;

                //if tls
                if (node.streamSecurity == StringDefines.StreamSecurity)
                {
                    streamSettings.security = node.streamSecurity;

                    Models.V2rayConfig.TlsSettings tlsSettings = new Models.V2rayConfig.TlsSettings
                    {
                        allowInsecure = StringHelper.ToBool(node.allowInsecure),
                        alpn = node.GetAlpn(),
                        fingerprint = node.fingerprint
                    };
                    if (!string.IsNullOrWhiteSpace(sni))
                    {
                        tlsSettings.serverName = sni;
                    }
                    else if (!string.IsNullOrWhiteSpace(host))
                    {
                        tlsSettings.serverName = StringHelper.String2List(host)[0];
                    }
                    streamSettings.tlsSettings = tlsSettings;
                }

                //if xtls
                if (node.streamSecurity == StringDefines.StreamSecurityX)
                {
                    streamSettings.security = node.streamSecurity;

                    Models.V2rayConfig.TlsSettings xtlsSettings = new Models.V2rayConfig.TlsSettings
                    {
                        allowInsecure = StringHelper.ToBool(node.allowInsecure),
                        alpn = node.GetAlpn(),
                        fingerprint = node.fingerprint
                    };
                    if (!string.IsNullOrWhiteSpace(sni))
                    {
                        xtlsSettings.serverName = sni;
                    }
                    else if (!string.IsNullOrWhiteSpace(host))
                    {
                        xtlsSettings.serverName = StringHelper.String2List(host)[0];
                    }
                    streamSettings.xtlsSettings = xtlsSettings;
                }

                //streamSettings
                switch (node.GetNetwork())
                {
                    //kcp基本配置暂时是默认值，用户能自己设置伪装类型
                    case "kcp":
                        KcpSettings kcpSettings = new KcpSettings
                        {
                            mtu = config.kcpItem.mtu,
                            tti = config.kcpItem.tti
                        };
                        if (iobound.Equals("out"))
                        {
                            kcpSettings.uplinkCapacity = config.kcpItem.uplinkCapacity;
                            kcpSettings.downlinkCapacity = config.kcpItem.downlinkCapacity;
                        }
                        else if (iobound.Equals("in"))
                        {
                            kcpSettings.uplinkCapacity = config.kcpItem.downlinkCapacity; ;
                            kcpSettings.downlinkCapacity = config.kcpItem.downlinkCapacity;
                        }
                        else
                        {
                            kcpSettings.uplinkCapacity = config.kcpItem.uplinkCapacity;
                            kcpSettings.downlinkCapacity = config.kcpItem.downlinkCapacity;
                        }

                        kcpSettings.congestion = config.kcpItem.congestion;
                        kcpSettings.readBufferSize = config.kcpItem.readBufferSize;
                        kcpSettings.writeBufferSize = config.kcpItem.writeBufferSize;
                        kcpSettings.header = new V2rayHeader
                        {
                            type = node.headerType
                        };
                        if (!string.IsNullOrEmpty(node.path))
                        {
                            kcpSettings.seed = node.path;
                        }
                        streamSettings.kcpSettings = kcpSettings;
                        break;
                    //ws
                    case "ws":
                        WsSettings wsSettings = new WsSettings
                        {
                        };

                        string path = node.path;
                        if (!string.IsNullOrWhiteSpace(host))
                        {
                            wsSettings.headers = new V2rayHeaders
                            {
                                Host = host
                            };
                        }
                        if (!string.IsNullOrWhiteSpace(path))
                        {
                            wsSettings.path = path;
                        }
                        streamSettings.wsSettings = wsSettings;

                        //TlsSettings tlsSettings = new TlsSettings();
                        //tlsSettings.allowInsecure = config.allowInsecure();
                        //if (!string.IsNullOrWhiteSpace(host))
                        //{
                        //    tlsSettings.serverName = host;
                        //}
                        //streamSettings.tlsSettings = tlsSettings;
                        break;
                    //h2
                    case "h2":
                        HttpSettings httpSettings = new HttpSettings();

                        if (!string.IsNullOrWhiteSpace(host))
                        {
                            httpSettings.host = StringHelper.String2List(host);
                        }
                        httpSettings.path = node.path;

                        streamSettings.httpSettings = httpSettings;

                        //TlsSettings tlsSettings2 = new TlsSettings();
                        //tlsSettings2.allowInsecure = config.allowInsecure();
                        //streamSettings.tlsSettings = tlsSettings2;
                        break;
                    //quic
                    case "quic":
                        QuicSettings quicsettings = new QuicSettings
                        {
                            security = host,
                            key = node.path,
                            header = new V2rayHeader
                            {
                                type = node.headerType
                            }
                        };
                        streamSettings.quicSettings = quicsettings;
                        if (node.streamSecurity == StringDefines.StreamSecurity)
                        {
                            if (!string.IsNullOrWhiteSpace(sni))
                            {
                                streamSettings.tlsSettings.serverName = sni;
                            }
                            else
                            {
                                streamSettings.tlsSettings.serverName = node.address;
                            }
                        }
                        break;

                    case "grpc":
                        var grpcSettings = new GrpcSettings
                        {
                            serviceName = node.path,
                            multiMode = (node.headerType == StringDefines.GrpcmultiMode)
                        };

                        streamSettings.grpcSettings = grpcSettings;
                        break;

                    default:
                        //tcp带http伪装
                        if (node.headerType.Equals(StringDefines.TcpHeaderHttp))
                        {
                            TcpSettings tcpSettings = new TcpSettings
                            {
                                header = new V2rayHeader
                                {
                                    type = node.headerType
                                }
                            };

                            if (iobound.Equals("out"))
                            {
                                //request填入自定义Host
                                string request = DownloadHelper.GetEmbedText<string>(PathConfig.v2raySampleHttprequestFileName);

                                string[] arrHost = host.Split(',');
                                string host2 = string.Join("\",\"", arrHost);
                                request = request.Replace("$requestHost$", $"\"{host2}\"");

                                //填入自定义Path
                                string pathHttp = @"/";
                                if (!string.IsNullOrEmpty(node.path))
                                {
                                    string[] arrPath = node.path.Split(',');
                                    pathHttp = string.Join("\",\"", arrPath);
                                }
                                request = request.Replace("$requestPath$", $"\"{pathHttp}\"");
                                tcpSettings.header.request = JsonHelper.FromJsonString<object>(request);
                            }
                            else if (iobound.Equals("in"))
                            {
                                //string response = Utils.GetEmbedText(Global.v2raySampleHttpresponseFileName);
                                //tcpSettings.header.response = Utils.FromJson<object>(response);
                            }

                            streamSettings.tcpSettings = tcpSettings;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                NLogHelper.WriteExceptionLog(ex);
            }
            return 0;
        }

        public static List<string> GetShadowsocksSecuritys(MarayConfigM config, ServerM vmessItem)
        {
            if (GetCoreType(config, vmessItem, ProtocolEnum.Shadowsocks) == CoreType.v2fly)
            {
                return StringDefines.ssSecuritys;
            }
            if (GetCoreType(config, vmessItem, ProtocolEnum.Shadowsocks) == CoreType.Xray)
            {
                return StringDefines.ssSecuritysInXray;
            }

            return StringDefines.ssSecuritysInSagerNet;
        }

        public static CoreType GetCoreType(MarayConfigM config, ServerM vmessItem, Enum.ProtocolEnum eConfigType)
        {
            if (vmessItem != null && vmessItem.coreType != null)
            {
                return (CoreType)vmessItem.coreType;
            }

            if (config.coreTypeItem == null)
            {
                return CoreType.Xray;
            }
            var item = config.coreTypeItem.FirstOrDefault(it => it.configType == eConfigType);
            if (item == null)
            {
                return CoreType.Xray;
            }
            return item.coreType;
        }

        #endregion outbound

        #region dns

        /// <summary> remoteDNS </summary>
        /// <param name="config">      </param>
        /// <param name="v2rayConfig"> </param>
        /// <returns> </returns>
        private static int dns(MarayConfigM config, ref V2rayConfig v2rayConfig)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(config.remoteDNS))
                {
                    return 0;
                }

                //Outbound Freedom domainStrategy
                if (!string.IsNullOrWhiteSpace(config.domainStrategy4Freedom))
                {
                    var outbound = v2rayConfig.outbounds[1];
                    outbound.settings.domainStrategy = config.domainStrategy4Freedom;
                    outbound.settings.userLevel = 0;
                }
                //待测试
                var obj = JsonHelper.FromJsonString<object>(config.remoteDNS);
                if (obj != null && obj.ToString().Contains("servers"))
                {
                    v2rayConfig.dns = obj;
                }
                else
                {
                    List<string> servers = new List<string>();

                    string[] arrDNS = config.remoteDNS.Split(',');
                    foreach (string str in arrDNS)
                    {
                        //if (Utils.IsIP(str))
                        //{
                        servers.Add(str);
                        //}
                    }
                    //servers.Add("localhost");
                    v2rayConfig.dns = new Models.V2rayConfig.Dns
                    {
                        servers = servers
                    };
                }
            }
            catch (Exception ex)
            {
                NLogHelper.WriteExceptionLog(ex);
            }
            return 0;
        }

        #endregion dns

        #region statistic

        private static int statistic(MarayConfigM config, ref V2rayConfig v2rayConfig)
        {
            if (config.enableStatistics)
            {
                string tag = StringDefines.InboundAPITagName;
                //API apiObj = new API();
                Policy policyObj = new Policy();
                SystemPolicy policySystemSetting = new SystemPolicy();

                string[] services = { "StatsService" };

                v2rayConfig.stats = new Stats();

                //apiObj.tag = tag;
                //apiObj.services = services.ToList();
                //v2rayConfig.api = apiObj;

                policySystemSetting.statsOutboundDownlink = true;
                policySystemSetting.statsOutboundUplink = true;
                policyObj.system = policySystemSetting;
                v2rayConfig.policy = policyObj;

                if (!v2rayConfig.inbounds.Exists(item => item.tag == tag))
                {
                    Inbounds apiInbound = new Inbounds();
                    Inboundsettings apiInboundSettings = new Inboundsettings();
                    apiInbound.tag = tag;
                    apiInbound.listen = StringDefines.Loopback;
                    apiInbound.port = GlobalVariable.statePort;
                    apiInbound.protocol = StringDefines.InboundAPIProtocal;
                    apiInboundSettings.address = StringDefines.Loopback;
                    apiInbound.settings = apiInboundSettings;
                    v2rayConfig.inbounds.Add(apiInbound);
                }

                if (!v2rayConfig.routing.rules.Exists(item => item.outboundTag == tag))
                {
                    RulesItem apiRoutingRule = new RulesItem
                    {
                        inboundTag = new List<string> { tag },
                        outboundTag = tag,
                        type = "field"
                    };
                    v2rayConfig.routing.rules.Add(apiRoutingRule);
                }
            }
            return 0;
        }

        #endregion statistic
    }
}