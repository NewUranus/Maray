﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Maray.Configs;
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
            var service = ServicesProvider.GetService<SubscribeService>();

            foreach (var item in service.GetSubscribeList())
            {
                SubscribeItemsource.Add(item.ToVM());
            }
        }

        [RelayCommand]
        private void Save()
        {
            var service = ServicesProvider.GetService<SubscribeService>();
            service.UpdateSubscribeList(SubscribeItemsource.Select(x => x.ToModel()).ToList());

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