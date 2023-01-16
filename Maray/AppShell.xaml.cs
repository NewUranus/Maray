using Maray.Services;
using Maray.Views;

using Microsoft.Extensions.DependencyInjection;

using System.Diagnostics;

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
        Routing.RegisterRoute(nameof(SettingPage), typeof(SettingPage));

        Routing.RegisterRoute(nameof(SubscribeSettingPage), typeof(SubscribeSettingPage));

        if (!isSetup)
        {
            isSetup = true;

            SetupTrayIcon();
        }
    }

    protected override void OnNavigated(ShellNavigatedEventArgs args)
    {
        if (CurrentPage is ServerPage serverPage)
        {
            serverPage.serverPageVM.InitData();
        }
        else if (CurrentPage is SubscribeSettingPage subscribeSettingPage)
        {
        }

        base.OnNavigated(args);
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