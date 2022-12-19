using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using Maray.Models;
using Maray.Services;

namespace Maray.ViewModels
{
    public partial class ServerVM: ObservableObject
    {
        //private string title;
        [ObservableProperty]
        public string ttitle;

        readonly SubscribeService subscribeService;

        [ObservableProperty]
        ObservableCollection<ServerM> servers;

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