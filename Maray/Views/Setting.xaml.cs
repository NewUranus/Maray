using Maray.ViewModels;

namespace Maray.Views;

public partial class Setting : ContentPage
{
    public Setting(SettingVM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        var s = sender as Switch;
        App.Current.UserAppTheme = s.IsToggled ? AppTheme.Dark : AppTheme.Light;
    }
}