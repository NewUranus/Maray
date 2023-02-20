using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Maray.ViewModels.SettingViewModels
{
    public partial class RoutingPageVM : BaseSettingVM
    {
        public RoutingPageVM()
        {
            Header = "Routing";
        }
    }
}