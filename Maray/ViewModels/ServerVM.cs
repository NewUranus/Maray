using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Maray.Helpers;
using Maray.Models;

namespace Maray.ViewModels
{
    public partial class ServerVM : ObservableObject
    {
        [ObservableProperty]
        private ServerM serverM;

        /// <summary> 延迟测试 </summary>
        [ObservableProperty]
        private int ping;

        public ServerVM()
        {
        }

        [RelayCommand]
        private async void TestServer()
        {
        }

        [RelayCommand]
        private async void PingTest()
        {
            var res = ServerHelper.Ping(serverM.address);
            Ping = (int)res;
        }
    }
}