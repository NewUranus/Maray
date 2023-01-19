using Maray.Views;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Net.Mime.MediaTypeNames;
using Maray.ViewModels;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;

using Windows.Graphics;

#endif

namespace Maray.Helpers
{
    internal class WindowHelper
    {
        public static WindowHelper Instance = new Lazy<WindowHelper>(() => new WindowHelper()).Value;

        public Microsoft.Maui.Controls.Window secondWindow;

        public void ShowNetSpeedWindow()
        {
            var vm = ServiceProviderHelper.GetService<NetSpeedPageVM>();
            secondWindow = new Microsoft.Maui.Controls.Window(new NetSpeedPage(vm));

            secondWindow.Title = "mini";

            Microsoft.Maui.Controls.Application.Current.OpenWindow(secondWindow);

#if WINDOWS

            //IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(secondWindow);
            // WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
            // AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
#endif
        }

        public void MoveWindow(int x, int y)
        {
            //Microsoft.Maui.Handlers.IWindowHandler windowHandler;
#if WINDOWS

            var nativeWindow = secondWindow.Handler.PlatformView;
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId WindowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = AppWindow.GetFromWindowId(WindowId);

            int realx = Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Width) - x; //Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Width)
            int realy = Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Height) - y; //Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Height)

            appWindow.MoveAndResize(new RectInt32(realx, realy, 110, 50));
            var p = appWindow.Presenter as OverlappedPresenter;

#endif
        }

        public void CloseNetSpeedWindow()
        {
            Microsoft.Maui.Controls.Application.Current.CloseWindow(secondWindow);
        }
    }
}