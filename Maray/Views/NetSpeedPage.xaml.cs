using Maray.Helpers;
using Maray.ViewModels;

namespace Maray.Views;

public partial class NetSpeedPage : ContentPage
{
    public NetSpeedPage(NetSpeedPageVM viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
    {
        WindowHelper.Instance.MoveWindow(100, 100);
    }

    private void DragGestureRecognizer_DropCompleted(object sender, DropCompletedEventArgs e)
    {
    }
}