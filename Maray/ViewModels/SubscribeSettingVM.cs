using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System.Collections.ObjectModel;

namespace Maray.ViewModels
{
    public partial class SubscribeSettingVM : ObservableObject
    {
        public ObservableCollection<SubscribeItemVM> SubscribeItemsource { get; set; } = new ObservableCollection<SubscribeItemVM>();
         
        public SubscribeSettingVM()
        {
            
        }

        [RelayCommand]
        void AddNew()
        {
            SubscribeItemsource.Add(new SubscribeItemVM()
            { Note="123"});
        }

        void InitData()
        {

        }

         
    }
}