using CommunityToolkit.Mvvm.ComponentModel;

using Maray.Configs;
using Maray.Services;
using Maray.V2ray;
using Maray.Views;

namespace Maray.ViewModels
{
    public partial class MainPageVM : ObservableObject
    {
        [ObservableProperty]
        public int aa = 10;

        public MainPageVM()
        {
            Test();
        }

        private void Test()
        {
            var config = ServicesProvider.GetService<ConfigService>().GetConfig();
            V2rayHelper.GenerateClientConfig(config.DefaultServer, PathConfig.v2rayConfig);
        }
    }
}