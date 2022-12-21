using Maray.Services;
using Maray.Views;

namespace Maray;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    private void InitData()
    {
    }
}