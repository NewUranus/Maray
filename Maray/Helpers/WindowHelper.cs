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

        private Microsoft.Maui.Controls.Window secondWindow;
#if WINDOWS
        //private Microsoft.UI.Xaml.Window secondwindow;
#endif

        public void ShowNetSpeedWindow()
        {
            secondWindow = new Window(new NetSpeedPage());

            secondWindow.Title = "mini";
#if WINDOWS

#endif

            Application.Current.OpenWindow(secondWindow);
        }

        public void CloseNetSpeedWindow()
        {
            Application.Current.CloseWindow(secondWindow);
        }
    }
}