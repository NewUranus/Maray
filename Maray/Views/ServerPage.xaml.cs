namespace Maray.Views;

using Maray.Models;
using Maray.ViewModels;

using System;
using System.Text;

public partial class ServerPage : ContentPage
{
    public ServerPage(ServerPageVM vm)
    {
        BindingContext = vm;

        InitializeComponent();
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        try
        {
            ((Entry)sender).Text = Encoding.Default.GetString(Convert.FromBase64String(((Entry)sender).Text));
        }
        catch
        { }
    }

    private void OnEntryTextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AboutPage));
    }
}