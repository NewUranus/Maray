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
    public partial class ServerVM : ObservableObject
    {
        [ObservableProperty]
        public string ttitle;

        private readonly SubscribeService subscribeService;

        [ObservableProperty]
        private ObservableCollection<ServerM> servers;

        [RelayCommand]
        public void AddServer(string e)
        {
            servers.Add(new ServerM("add one"));
        }

        public ServerVM(SubscribeService subService)
        {
            subscribeService = subService;
            ttitle = "Server Page";
            UpdateList();
        }

        private void UpdateList()
        {
            var list = subscribeService.GetSubscribeList();
            if (list != null)
            {
                servers = new ObservableCollection<ServerM>(list);
            }
        }
    }
}