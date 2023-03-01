﻿using CommunityToolkit.Mvvm.ComponentModel;

using Maray.Models.V2rayConfig;
using Maray.ViewModels.SettingViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Maray.ViewModels
{
    public partial class NetSpeedPageVM : BaseSettingVM
    {
        [ObservableProperty]
        private NetSpeedItemVM netSpeedSelectItemVM;

        public List<NetSpeedItemVM> NetSpeedItems { get; set; }

        public NetSpeedPageVM()
        {
            Header = "sds";

            CancellationTokenSource cts = new CancellationTokenSource();

            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            NetSpeedItems = interfaces.Select(i => new NetSpeedItemVM(i, 1000, cts.Token)).ToList();

            NetSpeedSelectItemVM = NetSpeedItems.FirstOrDefault();
        }
    }
}