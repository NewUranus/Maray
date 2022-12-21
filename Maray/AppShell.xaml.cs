using Maray.Views;

namespace Maray;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
        Routing.RegisterRoute(nameof(Config), typeof(Config));
        Routing.RegisterRoute(nameof(ServerPage), typeof(ServerPage));
        Routing.RegisterRoute(nameof(Setting), typeof(Setting));

        Routing.RegisterRoute(nameof(SubscribeSettingPage), typeof(SubscribeSettingPage));
    }
}