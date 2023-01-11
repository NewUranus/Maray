namespace Maray.Views;

using Maray.Models;
using Maray.ViewModels;

using System;
using System.Diagnostics;
using System.Text;

public partial class ServerPage : ContentPage
{
    public ServerPageVM serverPageVM;

    public ServerPage(ServerPageVM vm)
    {
        InitializeComponent();

        BindingContext = serverPageVM = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Debug.WriteLine("OnAppearing");

        //serverPageVM.InitData();
    }
}