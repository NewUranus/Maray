using Maray.Configs;
using Maray.Defines;
using Maray.Helpers;
using Maray.Models;
using Maray.Models.Configs;
using Maray.Services;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.V2ray
{
    internal class V2rayHelper
    {
        /*
        /// <summary> 载入V2ray </summary>
        public void LoadV2ray(ServerM config)
        {
            var item = ConfigHandler.GetDefaultServer(ref config);
            if (item == null)
            {
                ShowMsg(false, ResUI.CheckServerSettings);
                return;
            }

            if (SetCore(config, item) != 0)
            {
                ShowMsg(false, ResUI.CheckServerSettings);
                return;
            }
            string fileName = Utils.GetPath(v2rayConfigRes);
            if (V2rayConfigHandler.GenerateClientConfig(item, fileName, out string msg, out string content) != 0)
            {
                ShowMsg(false, msg);
            }
            else
            {
                ShowMsg(false, msg);
                ShowMsg(true, $"[{config.GetGroupRemarks(item.groupId)}] {item.GetSummary()}");
                V2rayRestart();
            }

            //start a socks service
            if (_process != null && !_process.HasExited && item.configType == EConfigType.Custom && item.preSocksPort > 0)
            {
                var itemSocks = new VmessItem()
                {
                    configType = EConfigType.Socks,
                    address = Global.Loopback,
                    port = item.preSocksPort
                };
                if (V2rayConfigHandler.GenerateClientConfig(itemSocks, null, out string msg2, out string configStr) == 0)
                {
                    processId = V2rayStartNew(configStr);
                }
            }
        }
        */

        /// <summary> 生成v2ray的客户端配置文件 </summary>
        /// <param name="node">     </param>
        /// <param name="fileName"> </param>
        /// <param name="msg">      </param>
        /// <returns> </returns>
        public static void GenerateClientConfig(ServerM node, string fileName)
        {
            try
            {
                if (node.configType == Enum.ProtocolType.Custom)
                {
                    GenerateClientCustomConfig(node, fileName);
                }
                else
                {
                    V2rayConfig v2rayConfig = null;
                    GenerateClientConfigContent(node, false);

                    //if (Utils.IsNullOrEmpty(fileName))
                    //{
                    //    content = Utils.ToJson(v2rayConfig);
                    //}
                    //else
                    //{
                    //    Utils.ToJsonFile(v2rayConfig, fileName, false);
                    //}
                }
            }
            catch (Exception ex)
            {
                NLogHelper.WriteExceptionLog(ex);
            }
        }

        /// <summary> 生成v2ray的客户端配置文件(自定义配置) </summary>
        /// <param name="node">     </param>
        /// <param name="fileName"> </param>
        /// <param name="msg">      </param>
        /// <returns> </returns>
        private static void GenerateClientCustomConfig(ServerM node, string fileName)
        {
            /*
           try
           {
               if (File.Exists(fileName))
               {
                   File.Delete(fileName);
               }

               string addressFileName = node.address;
               if (!File.Exists(addressFileName))
               {
                   addressFileName = PathConfig.GetConfigPath(addressFileName);
               }
               if (!File.Exists(addressFileName))
               {
                   msg = ResUI.FailedGenDefaultConfiguration;
                   return -1;
               }
               File.Copy(addressFileName, fileName);

               //check again
               if (!File.Exists(fileName))
               {
                   msg = ResUI.FailedGenDefaultConfiguration;
                   return -1;
               }

               //overwrite port
               if (node.preSocksPort <= 0)
               {
                   var fileContent = File.ReadAllLines(fileName).ToList();
                   var coreType = LazyConfig.Instance.GetCoreType(node, node.configType);
                   switch (coreType)
                   {
                       case ECoreType.v2fly:
                       case ECoreType.SagerNet:
                       case ECoreType.Xray:
                       case ECoreType.v2fly_v5:
                           break;

                       case ECoreType.clash:
                       case ECoreType.clash_meta:
                           //remove the original
                           var indexPort = fileContent.FindIndex(t => t.Contains("port:"));
                           if (indexPort >= 0)
                           {
                               fileContent.RemoveAt(indexPort);
                           }
                           indexPort = fileContent.FindIndex(t => t.Contains("socks-port:"));
                           if (indexPort >= 0)
                           {
                               fileContent.RemoveAt(indexPort);
                           }

                           fileContent.Add($"port: {LazyConfig.Instance.GetConfig().GetLocalPort(Global.InboundHttp)}");
                           fileContent.Add($"socks-port: {LazyConfig.Instance.GetConfig().GetLocalPort(Global.InboundSocks)}");
                           break;
                   }
                   File.WriteAllLines(fileName, fileContent);
               }

               //msg = string.Format(ResUI.SuccessfulConfiguration, $"[{LazyConfig.Instance.GetConfig().GetGroupRemarks(node.groupId)}] {node.GetSummary()}");
               msg = string.Format(ResUI.SuccessfulConfiguration, "");
           }
           catch (Exception ex)
           {
               Utils.SaveLog("GenerateClientCustomConfig", ex);
               msg = ResUI.FailedGenDefaultConfiguration;
               return -1;
           }
            return 0;
            */
        }

        private static V2rayConfig GenerateClientConfigContent(ServerM node, bool blExport)
        {
            try
            {
                //取得默认配置

                var v2rayConfig = DownloadHelper.GetEmbedText<V2rayConfig>(PathConfig.v2raySampleClient);

                var config = ServicesProvider.GetService<ConfigService>().GetConfig();

                //开始修改配置

                //log
                log(config, ref v2rayConfig);

                //本地端口
                inbound(config, ref v2rayConfig);

                //路由
                //routing(config, ref v2rayConfig);

                //outbound
                //outbound(node, ref v2rayConfig);

                //dns
                //dns(config, ref v2rayConfig);

                //stat
                //statistic(config, ref v2rayConfig);

                return new V2rayConfig();
            }
            catch (Exception ex)
            {
                NLogHelper.WriteExceptionLog(ex);
                return null;
            }
        }

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
    }
}