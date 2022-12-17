using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Maray.Models;

using System.Collections.ObjectModel;

namespace Maray.ViewModels
{
    public partial class SubscribeSettingVM : ObservableObject
    {
        public ObservableCollection<SubscribeItemM> SubscribeItemsource { get; set; } = new ObservableCollection<SubscribeItemM>();
         
        public SubscribeSettingVM()
        {
            
        }

        [RelayCommand]
        void AddNew()
        {
            SubscribeItemsource.Add(new SubscribeItemM()
            { Note="待定"});
        }

        void InitData()
        {

        }

         
    }
}