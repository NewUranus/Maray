using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Maray.Helpers;
using Maray.Models;
using Maray.Services;

namespace Maray.ViewModels
{
    /// <summary> 订阅页面 </summary>
    public partial class SubscribeItemVM : ObservableObject
    {
        [ObservableProperty]
        private int index;

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

        #region Command

        [RelayCommand]
        public async void UpdateSubscribe()
        {
            if (string.IsNullOrEmpty(SubscribeUrl))
            {
                return;
            }
            DownloadHelper downloadHelper = new DownloadHelper();
            var serverListBase64 = await downloadHelper.DownloadStringAsync(SubscribeUrl, false, null);

            var serverList = Base64Helper.Base64Decode(serverListBase64);

            string[] arrData = serverList.Split(Environment.NewLine.ToCharArray());

            this.ServerList = arrData.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        [RelayCommand]
        private void Remove(object target)
        {
            SubscribeItemVM subscribeItemVM = (SubscribeItemVM)target;

            if (subscribeItemVM != null)
            {
                ServiceProviderHelper.GetService<SubscribeService>().GetSubscribeList().RemoveAt(subscribeItemVM.Index);
                ServiceProviderHelper.GetService<SubscribeService>().SaveSubscribeList();
                ServiceProviderHelper.GetService<SubscribeSettingPageVM>().InitData();
            }
        }

        #endregion Command

        public SubscribeItemM ToModel()
        {
            SubscribeItemM model = new SubscribeItemM();

            model.Index = Index;
            model.IsEnable = IsEnable;
            model.Note = Note;
            model.SubscribeUrl = SubscribeUrl;
            model.ServerList = ServerList;
            return model;
        }
    }
}