using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Maray.Configs;
using Maray.Helpers;
using Maray.Models;

using System.Collections.ObjectModel;

namespace Maray.ViewModels
{
    public partial class SubscribeSettingVM : ObservableObject
    {
       
        public ObservableCollection<SubscribeItemVM> SubscribeItemsource { get; set; } = new ObservableCollection<SubscribeItemVM>();

        public SubscribeSettingVM()
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
            if (File.Exists(PathConfig.SettingFilePath))
            {
                var temp = JsonHelper.ReadFromJsonFile<List<SubscribeItemM>>(PathConfig.SettingFilePath);
                SubscribeItemsource.Clear();
                foreach (var item in temp)
                {
                    SubscribeItemsource.Add(item.ToVM());
                }
            }
        }

        [RelayCommand]
        private void Save()
        {
            if (File.Exists(PathConfig.SettingFilePath))
            {
                File.Delete(PathConfig.SettingFilePath);
            }


            List<SubscribeItemM> list = new List<SubscribeItemM>();
            foreach (var item in SubscribeItemsource)
            {
                list.Add(item.ToModel());
            }


            JsonHelper.WriteToJsonFile(PathConfig.SettingFilePath, list);
        }
    }
}