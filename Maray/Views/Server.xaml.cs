namespace Maray.Views;
using System;
using System.Text;

public partial class Server : ContentPage
{
	public Server()
	{
		InitializeComponent();
	}

    void OnEntryCompleted(object sender, EventArgs e)
    {
        ((Entry)sender).Text = Encoding.Default.GetString(Convert.FromBase64String(((Entry)sender).Text));
    }

    void OnEntryTextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
    }
}
