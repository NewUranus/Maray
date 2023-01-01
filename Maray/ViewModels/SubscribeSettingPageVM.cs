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
        public ObservableCollection<SubscribeItemVM> SubscribeItemsource { get; set; } = new ObservableCollection<SubscribeItemVM>();

        public SubscribeSettingPageVM()
        {
            InitData();
        }

        [RelayCommand]
        private void AddNew()
        {
            SubscribeItemsource.Add(new SubscribeItemVM()
            { Note = "待定" });
        }

        private void InitData()
        {
            var service = Helpers.ServiceProviderHelper.GetService<SubscribeService>();

            foreach (var item in service.GetSubscribeList())
            {
                SubscribeItemsource.Add(item.ToVM());
            }
        }

        [RelayCommand]
        private void Save()
        {
            List<SubscribeItemM> list = new List<SubscribeItemM>();
            foreach (var item in SubscribeItemsource)
            {
                list.Add(item.ToModel());
            }

            var subscribeService = ServiceProviderHelper.GetService<SubscribeService>();
            subscribeService.SetSubscribeList(list);
            subscribeService.SaveSubscribeList();
        }
    }
}