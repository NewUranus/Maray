using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Maray.Configs;
using Maray.Services;
using Maray.V2ray;
using Maray.Views;

namespace Maray.ViewModels
{
    public partial class MainPageVM : ObservableObject
    {
        public MainPageVM()
        {
            ShowRunningServer();
            Test();
        }

        private void Test()
        {
            //var config = Helpers.ServiceProviderHelper.GetService<ConfigService>().GetMarayConfig();
            //V2rayHelper.GenerateClientConfig(config.DefaultServer, PathConfig.v2rayConfig);
        }

        [RelayCommand]
        private void ShowRunningServer()
        {
        }
    }
}