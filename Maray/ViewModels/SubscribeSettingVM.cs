using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.ViewModels
{
  public  class SubscribeSettingVM
    {

        public ObservableCollection<SubscribeItemVM> SubscribeItemsource { get; set; } = new ObservableCollection<SubscribeItemVM>();

        public SubscribeSettingVM()
        {
            SubscribeItemsource.Add(new SubscribeItemVM()
            {
                SubscribeUrl = "123"
            }); ;
        }
    }
}
