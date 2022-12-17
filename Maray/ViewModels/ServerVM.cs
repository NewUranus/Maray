using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Maray.Models;

namespace Maray.ViewModels
{
	public class ServerVM : INotifyPropertyChanged
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

        public ObservableCollection<ServerM> servers { get; } = new();
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public ServerVM()
		{
            title = "Server Page";

            servers.Add(new ServerM() { v = "server test 1" });
            servers.Add(new ServerM() { v = "server test 2" });
        }
	}
}

