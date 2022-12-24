using Maray.ViewModels;

namespace Maray.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageVM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}