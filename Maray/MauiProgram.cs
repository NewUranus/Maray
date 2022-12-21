using Maray.Services;
using Maray.ViewModels;
using Maray.Views;

using System;

namespace Maray;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).ConfigureEssentials(essentials =>
            {
                essentials.UseVersionTracking();
            });

        var services = builder.Services;

        //Will create a single instance of the object which will be remain for the lifetime of the application.
        services.AddSingleton<MainPageVM>();
        services.AddSingleton<ServerVM>();
        services.AddSingleton<Server>();

        services.AddSingleton<SubscribeService>();

        services.AddSingleton<SubscribeSettingPage>();
        services.AddSingleton<SubscribeSettingPageVM>();
        //Will create a new instance of the object when requested during resolution. Transient objects do not have a pre-defined lifetime, but will typically follow the lifetime of their host.
        //builder.Services.AddTransient<SubscribeSettingVM>();
        //builder.Services.AddTransient<SubscribeSetting>();

        services.AddTransient<AboutPage>();
        services.AddTransient<AboutVM>();
        return builder.Build();
    }
}