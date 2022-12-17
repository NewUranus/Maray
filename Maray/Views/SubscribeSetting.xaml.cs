using Maray.ViewModels;

namespace Maray.Views;

public partial class SubscribeSetting : ContentPage
{

    public SubscribeSetting( )
    {
        InitializeComponent();
        InitVM();
    }

    void InitVM()
    {

        //var vm= MauiApp.CreateBuilder().Services.sing
        //BindingContext =new SubscribeSettingVM();
    }
    //public SubscribeSetting(SubscribeSettingVM subscribeSettingVM)
    //{
    //    InitializeComponent();
    //    BindingContext = subscribeSettingVM;
    //    //CreateMonkeyCollection();
    //    //BindingContext = this;
    //}

    //public ObservableCollection<SubscribeItemVM> SubscribeItemsource { get; set; } = new ObservableCollection<SubscribeItemVM>();

    //private void CreateMonkeyCollection()
    //{
    //    SubscribeItemsource.Add(new SubscribeItemVM()
    //    {
    //        Note = "ceshi1",
    //        SubscribeUrl = "111"
    //    });

    //    SubscribeItemsource.Add(new SubscribeItemVM()
    //    {
    //        Note = "ceshi2",
    //        SubscribeUrl = "222"
    //    });
    //}
}