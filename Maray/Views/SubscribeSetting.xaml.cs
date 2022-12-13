using Maray.ViewModels;

using System.Collections.ObjectModel;

namespace Maray.Views;

public partial class SubscribeSetting : ContentPage
{
	public SubscribeSetting()
	{
		InitializeComponent();
        CreateMonkeyCollection();
        BindingContext = this;
    }
    public ObservableCollection<SubscribeItemVM> SubscribeItemsource { get; set; } = new ObservableCollection<SubscribeItemVM>();
    void CreateMonkeyCollection()
    {

        SubscribeItemsource.Add(new SubscribeItemVM()
        {
            Note="ceshi1",
            SubscribeUrl = "111"
        });

        SubscribeItemsource.Add(new SubscribeItemVM()
        {
            Note = "ceshi2",
            SubscribeUrl = "222"
        });
    }
    }