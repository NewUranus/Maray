using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Maray.Helpers;
using Maray.Enum;
using Maray.Models;
using Maray.Services;
using Newtonsoft.Json;

namespace Maray.ViewModels
{
    public partial class ServerPageVM : ObservableObject
    {
        [ObservableProperty]
        private string title;

        private readonly SubscribeService subscribeService;

        [ObservableProperty]
        private ServerVM selectedServer;

        //[ObservableProperty]
        //private ObservableCollection<ServerVM> servers = new ObservableCollection<ServerVM>();

        [ObservableProperty]
        private ObservableCollection<ServerVMGroup> serverVMGroupList = new();

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
                    AddServerGroupInner(item.Note, item.ServerList);
                    //foreach (var itemInner in item.ServerList)
                    //{
                    //    AddServerInner(itemInner);
                    //}
                }
            }
        }

        #region Command

        [RelayCommand]
        public async void AddServer()
        {
            string result = await Shell.Current.DisplayPromptAsync("Add Server", "Server url", keyboard: Keyboard.Url);
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    return;
                }

                AddServerInner(result);
            }
            catch
            { }
        }

        [RelayCommand]
        public void SelectionChanged()
        {
            if (selectedServer != null)
            {
            }
        }

        private void AddServerInner(string url)
        {
            ServerVM serverVM = new ServerVM();
            var item = ServerHelper.ParseUrlToServerItem(url);
            if (item != null)
            {
                serverVM.ServerM = item;
                //serverVM.ServerM.indexId = servers.Count;
                //servers.Add(serverVM);
            }
        }

        private void AddServerGroupInner(string name, List<string> urlList)
        {
            List<ServerVM> list = new List<ServerVM>();

            foreach (var item in urlList)
            {
                ServerVM serverVM = new ServerVM();
                var server = ServerHelper.ParseUrlToServerItem(item);
                if (item != null)
                {
                    serverVM.ServerM = server;
                    serverVM.ServerM.indexId = list.Count;
                    serverVM.ServerM.groupId = name;
                    list.Add(serverVM);
                }
            }

            ServerVMGroup serverMs = new ServerVMGroup(name, list);
            ServerVMGroupList.Add(serverMs);
        }

        #endregion Command
    }

    public class ServerVMGroup : List<ServerVM>
    {
        public string Name { get; private set; }

        public ServerVMGroup(string name, List<ServerVM> animals) : base(animals)
        {
            Name = name;
        }
    }
}