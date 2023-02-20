using Maray.ViewModels;

namespace Maray.Views;

public partial class SettingPage : ContentPage
{
    public SettingPage(SettingPageVM vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }

    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        var s = sender as SwitchCell;
        App.Current.UserAppTheme = s.On ? AppTheme.Dark : AppTheme.Light;
    }
}