using Maray.Platforms.Windows.NativeWindowing;
using Maray.Services;

namespace Maray.Platforms.Windows
{
    public class TrayService : ITrayService
    {
        WindowsTrayIcon tray;

        public Action ClickHandler { get; set; }

        public void Initialize()
        {
            tray = new WindowsTrayIcon("Platforms/Windows/helicopter.ico");
            tray.LeftClick = () =>
            {
                WindowExtensions.BringToFront();
                ClickHandler?.Invoke();
            };

            tray.RightClick = () =>
            {
                WindowExtensions.ShowMiniMenu();
            };
        }
    }
}