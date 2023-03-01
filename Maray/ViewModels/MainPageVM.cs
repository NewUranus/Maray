using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System.Collections.ObjectModel;

namespace Maray.ViewModels
{
    public partial class MainPageVM : ObservableObject
    {
        public MainPageVM()
        {
            ShowRunningServer();
        }

        [RelayCommand]
        private void ShowRunningServer()
        {
        }
    }
}