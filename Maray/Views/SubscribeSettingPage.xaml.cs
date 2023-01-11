using Maray.ViewModels;

using System.Diagnostics;

namespace Maray.Views;

public partial class SubscribeSettingPage : ContentPage
{
    public SubscribeSettingPage(SubscribeSettingPageVM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}