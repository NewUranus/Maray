using CommunityToolkit.Mvvm.ComponentModel;

namespace Maray.ViewModels
{
    public partial class SubscribeItemVM : ObservableObject
    {
        [ObservableProperty]
        public string note;

        [ObservableProperty]
        public string subscribeUrl;
    }
}