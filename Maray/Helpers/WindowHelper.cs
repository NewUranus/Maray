using Maray.Views;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void ShowNetSpeedWindow()
        {
            Window secondWindow = new Window(new NetSpeedPage());
            secondWindow.Created += SecondWindow_Created;

            Application.Current.OpenWindow(secondWindow);
        }

        private void SecondWindow_Created(object sender, EventArgs e)
        {
            var window = (Window)sender;

#if WINDOWS

            IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window.Handler);
            WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
            AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);

            const int width = 1200;
            const int height = 800;
            int x = 1920 / 2 - width / 2; //Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Width)
            int y = 1080 / 2 - height / 2; //Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Height)

            winuiAppWindow.MoveAndResize(new RectInt32(x, y, width, height));

#endif
        }
    }
}