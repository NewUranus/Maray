using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Maray.Helpers;
using Maray.ViewModels.SettingViewModels;

using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Maray.ViewModels
{
    public partial class SettingPageVM : ObservableObject
    {
        [ObservableProperty]
        private bool showNetSpeed;

        [ObservableProperty]
        private string selectedLogLevel;

        public ObservableCollection<string> LogLevelList { get; set; } = new ObservableCollection<string>() { "debug", "info", "warning", "error", "none" };
        public ObservableCollection<BaseSettingVM> SettingList { get; set; } = new ObservableCollection<BaseSettingVM>();

        [ObservableProperty]
        private BaseSettingVM selectedSettingVM;

        public SettingPageVM()
        {
            SelectedLogLevel = "warning";
            SettingList.Add(new RoutingPageVM()
            {
            });

            SettingList.Add(new NetSpeedPageVM()
            {
            });
        }

        partial void OnShowNetSpeedChanged(bool value)
        {
            if (value)
            {
                Debug.WriteLine(value);
                WindowHelper.Instance.ShowNetSpeedWindow();
            }
            else
            {
                WindowHelper.Instance.CloseNetSpeedWindow();
            }
        }

        [RelayCommand]
        private async void ShowNetSpeed2()
        {
        }
    }
}