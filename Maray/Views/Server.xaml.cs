namespace Maray.Views;

using Maray.Model;
using Maray.ViewModels;
using System;
using System.Text;

public partial class Server : ContentPage
{
    private ServerVM ServerVM;
    public Server(ServerVM vm)
    {
        ServerVM = vm;

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
        //Navigation.PushAsync(new SubscribeSetting());
    }
}
