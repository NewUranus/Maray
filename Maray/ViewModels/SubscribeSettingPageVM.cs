using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Maray.Helpers;
using Maray.Models;
using Maray.Services;

using System.Collections.ObjectModel;

namespace Maray.ViewModels
{
    public partial class SubscribeSettingPageVM : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<SubscribeItemVM> subscribeItemsource;

        public SubscribeSettingPageVM()
        {
            SubscribeItemsource = new ObservableCollection<SubscribeItemVM>();
            InitData();
        }

        #region Command

        [RelayCommand]
        private void AddNew()
        {
            SubscribeItemsource.Add(new SubscribeItemVM()
            {
                Index = SubscribeItemsource.Count,
                Note = "待定"
            });
        }

        [RelayCommand]
        private void Save()
        {
            List<SubscribeItemM> list = new List<SubscribeItemM>();
            foreach (var item in SubscribeItemsource)
            {
                item.UpdateSubscribe();
                list.Add(item.ToModel());
            }

            var subscribeService = ServiceProviderHelper.GetService<SubscribeService>();
            subscribeService.SetSubscribeList(list);
            subscribeService.SaveSubscribeList();

            ServiceProviderHelper.GetService<ServerPageVM>().NeedRefresh = true;
        }

        #endregion Command

        public void InitData()
        {
            SubscribeItemsource.Clear();
            var service = Helpers.ServiceProviderHelper.GetService<SubscribeService>();

            foreach (var item in service.GetSubscribeList())
            {
                SubscribeItemsource.Add(item.ToVM());
            }
        }
    }
}