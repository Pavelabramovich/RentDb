using LilaRent.MobileApp.ViewModels;
using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Components;


namespace LilaRent.MobileApp.Views;


public partial class FavoritesTabbedView : TabbedViewView
{ 
    public FavoritesTabbedView(IServiceProvider serviceProvider)
		: base(serviceProvider)
	{
		InitializeComponent();
	}
}