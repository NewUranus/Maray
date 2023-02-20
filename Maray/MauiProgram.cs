using Maray.Services;
using Maray.ViewModels;
using Maray.ViewModels.SettingViewModels;
using Maray.Views;
using Maray.Views.SettingViews;

using Microsoft.Maui.LifecycleEvents;

using NLog;

using SkiaSharp.Views.Maui.Controls.Hosting;

namespace Maray;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;

using Windows.Graphics;

#endif

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseSkiaSharp(true)
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).ConfigureEssentials(essentials =>
            {
                essentials.UseVersionTracking();
            });

        NLog.LogManager.Setup().LoadConfiguration(builder =>
        {
            // builder.ForLogger().FilterMinLevel(LogLevel.Info).WriteToConsole();
            builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToFile(fileName: "Logs\\" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt");
        });

        var services = builder.Services;

        //Services
        services.AddSingleton<SubscribeService>();
        services.AddSingleton<ConfigService>();
        services.AddSingleton<CoreService>();

        //Will create a single instance of the object which will be remain for the lifetime of the application.
        services.AddSingleton<MainPage>();
        services.AddSingleton<MainPageVM>();

        //ServerPage
        services.AddSingleton<ServerPageVM>();
        services.AddSingleton<ServerPage>();

        services.AddSingleton<SubscribeSettingPage>();
        services.AddSingleton<SubscribeSettingPageVM>();
        //Will create a new instance of the object when requested during resolution. Transient objects do not have a pre-defined lifetime, but will typically follow the lifetime of their host.
        //builder.Services.AddTransient<SubscribeSettingVM>();
        //builder.Services.AddTransient<SubscribeSetting>();

        services.AddTransient<SettingPage>();
        services.AddTransient<SettingPageVM>();

        services.AddTransient<AboutPageVM>();
        services.AddTransient<AboutPage>();

        services.AddTransient<NetSpeedPageVM>();
        services.AddTransient<NetSpeedPage>();

        services.AddTransient<RoutingPageVM>();
        services.AddTransient<RoutingPage>();

#if WINDOWS

        services.AddSingleton<ITrayService, Maray.Platforms.Windows.TrayService>();
        services.AddSingleton<INotificationService, Maray.Platforms.Windows.NotificationService>();
        builder.ConfigureLifecycleEvents(lifecycleEvents =>
        {
            lifecycleEvents.AddWindows(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.OnWindowCreated(window =>
                {
                    if (window.Title == "mini")
                    {
                        IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                        AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);

                        const int width = 110;
                        const int height = 50;
                        int x = Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Width) - 300; //Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Width)
                        int y = Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Height) - 200; //Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Height)

                        if (winuiAppWindow.Presenter is OverlappedPresenter p)
                        {
                            p.IsResizable = false;
                            p.SetBorderAndTitleBar(false, false);
                        }

                        winuiAppWindow.MoveAndResize(new RectInt32(x, y, width, height));

                        window.ExtendsContentIntoTitleBar = false;

                        //SetTitleBar(winuiAppWindow);

                        void SetTitleBar(AppWindow window)
                        {
                            var titleBar = window.TitleBar;
                            if (titleBar == null)
                            {
                                return;
                            }

                            titleBar.ExtendsContentIntoTitleBar = true;
                        }
                    }
                    else
                    {
                        IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                        AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);

                        const int width = 1200;
                        const int height = 800;
                        int x = 1920 / 2 - width / 2; //Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Width)
                        int y = 1080 / 2 - height / 2; //Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Height)

                        winuiAppWindow.MoveAndResize(new RectInt32(x, y, width, height));

                        //winuiAppWindow.TitleBar.IconShowOptions = IconShowOptions.HideIconAndSystemMenu;
                        window.ExtendsContentIntoTitleBar = false;
                    }
                });

                wndLifeCycleBuilder.OnResumed(del =>
                {
                });
                wndLifeCycleBuilder.OnActivated((window, args) =>
                {
                });
            });
        });
#endif

        ModifyEntry();

        return builder.Build();
    }

    public static void ModifyEntry()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoMoreBorders", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#elif IOS || MACCATALYST
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#elif WINDOWS
            handler.PlatformView.FontWeight = Microsoft.UI.Text.FontWeights.Thin;
#endif
        });
    }
}