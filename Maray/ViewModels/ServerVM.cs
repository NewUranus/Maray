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
    [INotifyPropertyChanged]
    public partial class ServerVM
    {
        private string title;
        public string Title
        {
            get => title;
            set
            {
                if (title == value)
                    return;
                title = value;
                OnPropertyChanged(nameof(title));
            }
        }

        readonly SubscribeService subscribeService;
        public ObservableCollection<ServerM> servers;//{ get; } = new();
        //public event PropertyChangedEventHandler PropertyChanged;
        //public void OnPropertyChanged([CallerMemberName] string name = "") =>
        //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public ServerVM(SubscribeService subService)
        {
            subscribeService = subService;

            UpdateList();
        }

        private void UpdateList()
        {
            var list = subscribeService.GetSubscribeList();
            if (list != null)
            {
                servers = new ObservableCollection<ServerM>(list);
                OnPropertyChanged("servers");
            }
        }
    }
}

