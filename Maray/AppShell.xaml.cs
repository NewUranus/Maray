using Maray.Services;
using Maray.Views;

using Microsoft.Extensions.DependencyInjection;

namespace Maray;

public partial class AppShell : Shell
{
    private static bool isSetup = false;

    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
        Routing.RegisterRoute(nameof(Config), typeof(Config));
        Routing.RegisterRoute(nameof(ServerPage), typeof(ServerPage));
        Routing.RegisterRoute(nameof(Setting), typeof(Setting));

        Routing.RegisterRoute(nameof(SubscribeSettingPage), typeof(SubscribeSettingPage));

        if (!isSetup)
        {
            isSetup = true;

            SetupTrayIcon();
        }
    }

    private void SetupTrayIcon()
    {
        var trayService = Helpers.ServiceProviderHelper.GetService<ITrayService>();

        if (trayService != null)
        {
            trayService.Initialize();
            trayService.ClickHandler = () =>
                Helpers.ServiceProviderHelper.GetService<INotificationService>()
                    ?.ShowNotification("Hello Build! 😻 From .NET MAUI", "How's your weather?  It's sunny where we are 🌞");
        }
    }
}