using Maray.Helpers;
using Maray.Services;
using Maray.V2ray;
using Maray.Views;

namespace Maray;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        InitData();
        RunV2ray();
        MainPage = new AppShell();
    }

    private void InitData()
    {
    }

    private void RunV2ray()
    {
        ServiceProviderHelper.GetService<CoreService>().RunCore();
    }
}