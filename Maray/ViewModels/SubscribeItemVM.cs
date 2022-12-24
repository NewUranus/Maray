using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Maray.Helpers;
using Maray.Models;

namespace Maray.ViewModels
{
    /// <summary> 订阅页面 </summary>
    public partial class SubscribeItemVM : ObservableObject
    {
        [ObservableProperty]
        private bool isEnable;

        [ObservableProperty]
        private string note;

        [ObservableProperty]
        private string subscribeUrl;

        [ObservableProperty]
        private List<string> serverList;

        public SubscribeItemVM()
        {
        }

        [RelayCommand]
        private async void UpdateSubscribe()
        {
            if (string.IsNullOrEmpty(SubscribeUrl))
            {
                return;
            }
            DownloadHelper downloadHelper = new DownloadHelper();
            var serverListBase64 = await downloadHelper.DownloadStringAsync(SubscribeUrl, false, null);

            var serverList = Base64Helper.Base64Decode(serverListBase64);

            string[] arrData = serverList.Split(Environment.NewLine.ToCharArray());

            this.serverList = arrData.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public SubscribeItemM ToModel()
        {
            SubscribeItemM model = new SubscribeItemM();

            model.IsEnable = isEnable;
            model.Note = note;
            model.SubscribeUrl = subscribeUrl;
            model.ServerList = serverList;
            return model;
        }
    }
}