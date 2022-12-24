using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maray.Enum;
using Maray.Models;
using Maray.Services;
using Newtonsoft.Json;

namespace Maray.ViewModels
{
    public partial class ServerPageVM : ObservableObject
    {
        [ObservableProperty]
        public string title;

        private readonly SubscribeService subscribeService;

        [ObservableProperty]
        private ObservableCollection<ServerM> servers = new ObservableCollection<ServerM>();

        [RelayCommand]
        public void AddServer(string e)
        {
            try
            {
                ServerM server = new ServerM();
                if (e.StartsWith("vmess"))
                {
                    server.type = ProtocolType.VMESS;
                }
                var t = Encoding.Default.GetString(Convert.FromBase64String(e.Replace("vmess://", "")));
                server.node = JsonConvert.DeserializeObject<Node>(t);
                servers.Add(server);
            }
            catch
            { }
        }

        public ServerPageVM()
        {
            subscribeService = ServicesProvider.GetService<SubscribeService>();
            title = "Server Page";
            UpdateList();
        }

        private void UpdateList()
        {
            var list = subscribeService.GetSubscribeList();
            if (list != null)
            {
                foreach (var item in list)
                {
                    foreach (var itemInner in item.ServerList)
                    {
                        servers.Add(new ServerM());
                    }
                }
            }
        }
    }
}