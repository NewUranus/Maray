using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Maray.Helpers;
using Maray.Views;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.ViewModels
{
    public partial class SettingPageVM : ObservableObject
    {
        /// <summary> 延迟测试 </summary>
        [ObservableProperty]
        private bool showNetSpeed;

        partial void OnShowNetSpeedChanged(bool value)
        {
            if (value)
            {
                Debug.WriteLine(value);
                WindowHelper.Instance.ShowNetSpeedWindow();
            }
            else
            {
            }
        }

        [RelayCommand]
        private async void ShowNetSpeed2()
        {
        }
    }
}