namespace Maray.Views;

using Maray.Models;
using Maray.ViewModels;
using System;
using System.Text;

public partial class Server : ContentPage
{
    ServerVM vm => BindingContext as ServerVM;
    public Server(ServerVM vm)
    {
        BindingContext = vm;

        InitializeComponent();
    }

    void OnEntryCompleted(object sender, EventArgs e)
    {
        try
        {
            ((Entry)sender).Text = Encoding.Default.GetString(Convert.FromBase64String(((Entry)sender).Text));
        }
        catch
        { }
    }

    void OnEntryTextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AboutPage));
    }
}
