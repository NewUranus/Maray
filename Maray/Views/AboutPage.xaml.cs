using Maray.ViewModels;

namespace Maray.Views;

public partial class AboutPage : ContentPage
{
    public AboutPage(AboutVM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void LearnMore_Clicked(object sender, EventArgs e)
    {
        // Navigate to the specified URL in the system browser.
        await Launcher.Default.OpenAsync("https://github.com/NewUranus/Maray");
    }
}