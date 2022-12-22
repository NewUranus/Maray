using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Maray.Models;
using Maray.Services;

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
            servers.Add(new ServerM("add one", ""));
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
                        servers.Add(new ServerM("aa", itemInner));
                    }
                }
            }
        }
    }
}