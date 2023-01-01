using Maray.Services;
using Maray.V2ray;
using Maray.Views;

namespace Maray;

public partial class App : Application
{
    private V2rayHelper v2RayHelper = new V2rayHelper();

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
        var config = Helpers.ServiceProviderHelper.GetService<ConfigService>().GetMarayConfig();
        v2RayHelper.LoadV2ray(config.DefaultServer);
    }
}