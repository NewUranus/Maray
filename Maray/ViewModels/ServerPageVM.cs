using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Maray.Helpers;
using Maray.Services;

using System.Collections.ObjectModel;

namespace Maray.ViewModels
{
    public partial class ServerPageVM : ObservableObject
    {
        public bool NeedRefresh { get; set; } = true;

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private ServerVM selectedServer;

        //[ObservableProperty]
        //private ObservableCollection<ServerVM> servers = new ObservableCollection<ServerVM>();

        [ObservableProperty]
        private ObservableCollection<ServerVMGroup> serverVMGroupList = new();

        public ServerPageVM()
        {
            Title = "Server Page";
        }

        #region Init

        public async void InitData()
        {
            if (NeedRefresh)
            {
                UpdateList();
                NeedRefresh = false;
            }
        }

        private void UpdateList()
        {
            ServerVMGroupList.Clear();
            var list = Helpers.ServiceProviderHelper.GetService<SubscribeService>().GetSubscribeList();
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item.IsEnable)
                    {
                        AddServerGroupInner(item.Note, item.ServerList);
                    }
                }
            }
        }

        #endregion Init

        #region Command

        [RelayCommand]
        private async void Active()
        {
            if (SelectedServer != null)
            {
                var configServece = Helpers.ServiceProviderHelper.GetService<ConfigService>();

                var config = configServece.GetMarayConfig();
                config.DefaultServer = SelectedServer.ServerM;
                configServece.SetMarayConfig(config);
            }
        }

        [RelayCommand]
        private async void PingTestAll()
        {
            foreach (var item in serverVMGroupList)
            {
                foreach (var item1 in item)
                {
                    item1.PingTest();
                }
            }
        }

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

                var serverVM = GetServerVM(result);
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

        #endregion Command

        private ServerVM GetServerVM(string url)
        {
            ServerVM serverVM = new ServerVM();
            var item = ServerHelper.ParseUrlToServerItem(url);

            serverVM.ServerM = item;

            return serverVM;
        }

        private void AddServerGroupInner(string name, List<string> urlList)
        {
            List<ServerVM> list = new List<ServerVM>();

            foreach (var item in urlList)
            {
                var serverVM = GetServerVM(item);

                serverVM.Index = list.Count;
                serverVM.ServerM.groupId = name;
                list.Add(serverVM);
            }

            ServerVMGroup serverMs = new ServerVMGroup(name, list);
            ServerVMGroupList.Add(serverMs);
        }
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