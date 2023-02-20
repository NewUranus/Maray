using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.ViewModels.SettingViewModels
{
    public partial class BaseSettingVM : ObservableObject
    {
        [ObservableProperty]
        private string header;

        public BaseSettingVM()
        { }
    }
}