using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Maray.Helpers;
using Maray.Models;

namespace Maray.ViewModels
{
    public partial class SubscribeItemVM : ObservableObject
    {
        [ObservableProperty]
        public bool isEnable;

        [ObservableProperty]
        public string note;

        [ObservableProperty]
        public string subscribeUrl;

        [ObservableProperty]
        public List<string> subscribeServerList;

        public SubscribeItemVM()
        {
        }

        [RelayCommand]
        private async void UpdateSubscribe()
        {
            //https://api.ndsxfkjfvhzdsfio.quest/link/18tGVscS1aAAfTg8?sub=3&extend=1
            if (string.IsNullOrEmpty(SubscribeUrl))
            {
                return;
            }
            DownloadHelper downloadHelper = new DownloadHelper();
            var serverListBase64 = await downloadHelper.DownloadStringAsync(SubscribeUrl, false, null);

            var serverList = Base64Helper.Base64Decode(serverListBase64);

            string[] arrData = serverList.Split(Environment.NewLine.ToCharArray());

            subscribeServerList = arrData.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

       public SubscribeItemM ToModel()
        {
            SubscribeItemM model=new SubscribeItemM();
          
            model.IsEnable = isEnable;
            model.Note = note;
            model.SubscribeUrl = subscribeUrl;

            return model;


        }
    }
}