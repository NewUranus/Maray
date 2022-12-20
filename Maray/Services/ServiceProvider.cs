using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Services
{
    //调用平台的容器服务
    public static class ServicesProvider
    {
        public static TService GetService<TService>()
            => Current.GetService<TService>();

        public static IServiceProvider Current
            =>
#if WINDOWS10_0_19041_0_OR_GREATER
			MauiWinUIApplication.Current.Services;

#elif ANDROID
			MauiApplication.Current.Services;
#elif IOS || MACCATALYST
            MauiUIApplicationDelegate.Current.Services;

#else
			null;
#endif
    }
}