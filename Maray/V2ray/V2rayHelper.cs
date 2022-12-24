using System;
using System.Collections.Generic;
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
        public void LoadV2ray(Config config)
        {
            if (Global.reloadV2ray)
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
        }*/
    }
}