using CommunityToolkit.Mvvm.ComponentModel;

using Maray.Services;
using Maray.Views;

namespace Maray.ViewModels
{
    public partial class MainPageVM : ObservableObject
    {
        [ObservableProperty]
        public int aa = 10;

        public MainPageVM()
        {
        }

        private void Test()
        {
        }
    }
}