using Maray.Services;
using Maray.ViewModels;
using Maray.Views;

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
			});

        var services = builder.Services;
        services.AddSingleton<ServerVM>();
        services.AddSingleton<Server>();
        services.AddSingleton<SubscribeSettingVM>();
        services.AddSingleton<SubscribeService>();
        //builder.Services.AddTransient<SubscribeSettingVM>();
        //builder.Services.AddTransient<SubscribeSetting>();
        return builder.Build();
	}
}
