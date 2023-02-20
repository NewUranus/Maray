using Maray.Configs;
using Maray.Defines;
using Maray.Enum;
using Maray.Helpers;
using Maray.Models.Configs;
using Maray.Models.XrayConfigs;

namespace Maray.Cores
{
    internal class XrayHelper
    {
        public static bool GenerateClientXrayConfig(MarayConfigM config)
        {
            try
            {
                //取得默认配置
                XrayConfig xrayConfig = new XrayConfig();

                //开始修改配置
                //log
                if (config.logEnabled)
                {
                    xrayConfig.log.loglevel = config.Loglevel;
                }

                #region inbound

                if (config.inbound.Count > 0)
                {
                    xrayConfig.inbounds.Clear();
                    foreach (var item in config.inbound)
                    {
                        Models.XrayConfigs.Inbound inbound = new Models.XrayConfigs.Inbound();
                        inbound.tag = item.protocol;

                        inbound.listen = item.listen;
                        inbound.port = item.localPort;
                        inbound.protocol = item.protocol;

                        inbound.sniffing.enabled = item.sniffingEnabled;

                        switch (inbound.protocol)
                        {
                            case StringDefines.socksProtocolLite:

                                inbound.settings = new InboundSocksSettings()
                                {
                                    udp = true,
                                };
                                break;

                            case StringDefines.ssProtocolLite:

                                break;

                            case StringDefines.vlessProtocolLite:

                                break;

                            case StringDefines.vmessProtocolLite:
                                inbound.settings = new InboundVMessSettings()
                                {
                                };
                                break;

                            case "http":
                                inbound.settings = new InboundHttpSettings()
                                {
                                };
                                break;

                            default:
                                break;
                        }

                        xrayConfig.inbounds.Add(inbound);
                    }
                }

                #endregion inbound

                #region outbound

                xrayConfig.outbounds.Clear();
                Outbound outbound = new Outbound();

                outbound.streamSettings = new Models.XrayConfigs.StreamSettings()
                {
                    network = config.DefaultServer.GetNetwork(),
                };
                if (outbound.streamSettings.network == "ws")
                {
                    outbound.streamSettings.wsSettings = new WebSocketObject()
                    {
                        path = config.DefaultServer.path,
                        headers = new Headers()
                        {
                            Host = config.DefaultServer.requestHost,
                        },
                    };
                }

                switch (config.DefaultServer.configType)
                {
                    case ProtocolEnum.VMess:

                        outbound.tag = "proxy";
                        outbound.protocol = "vmess";
                        outbound.settings = new OutboundVMessSettings()
                        {
                            vnext = new List<Vnext>()
                            {
                                new Vnext()
                                {
                                    address=config.DefaultServer.address,
                                    port=config.DefaultServer.port,
                                    users=new List<User>()
                                    {
                                      new User()
                                      {
                                          id=config.DefaultServer.id,
                                          security = config.DefaultServer.security,
                                          alterId=config.DefaultServer.alterId,
                                      }
                                    }
                                }
                            }
                        };

                        break;

                    case ProtocolEnum.Custom:
                        break;

                    case ProtocolEnum.Shadowsocks:
                        break;

                    case ProtocolEnum.Socks:
                        break;

                    case ProtocolEnum.VLESS:

                        outbound.protocol = "vless";
                        outbound.settings = new OutboundVLessSettings()
                        {
                            vnext = new List<Vnext>()
                            {
                                new Vnext()
                                {
                                    address=config.DefaultServer.address,
                                    port=config.DefaultServer.port,
                                    users=new List<User>()
                                    {
                                      new User()
                                      {
                                          id=config.DefaultServer.id,
                                          encryption = config.DefaultServer.security,
                                          flow=config.DefaultServer.flow,
                                      }
                                    }
                                }
                            }
                        };

                        break;

                    case ProtocolEnum.Trojan:
                        break;

                    default:
                        break;
                }

                #endregion outbound

                #region routing

                xrayConfig.routing = new Models.XrayConfigs.Routing()
                {
                };

                #endregion routing

                xrayConfig.outbounds.Add(outbound);

                JsonHelper.WriteToJsonFile(PathConfig.XrayExeConfigPath, xrayConfig);
                NLogHelper.WriteLog("GenerateClientXrayConfig success.");
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.WriteExceptionLog(ex);
                return false;
            }
        }
    }
}