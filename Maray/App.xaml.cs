using Maray.Views;

namespace Maray;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
        Routing.RegisterRoute(nameof(Config), typeof(Config));
        Routing.RegisterRoute(nameof(Server), typeof(Server));
        Routing.RegisterRoute(nameof(Setting), typeof(Setting));
        Routing.RegisterRoute(nameof(SubscribeSettingPage), typeof(SubscribeSettingPage));
    }
}