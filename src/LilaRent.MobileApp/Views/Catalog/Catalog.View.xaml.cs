using LilaRent.MobileApp.ViewModels;
using LilaRent.MobileApp.Resources.DataTemplates;


namespace LilaRent.MobileApp.Views;


public partial class CatalogView : ContentPage
{
	public CatalogView()
	{
		InitializeComponent();

        AnnouncementsCollectionView.ItemTemplate = new IsCrownedAnnouncementTemplate("ClickCommand", this);

        Appearing += CatalogView_Appearing;
    }

    private async void CatalogView_Appearing(object? sender, EventArgs e)
    {
        await ((CatalogViewModel)BindingContext).LoadCommand.ExecuteAsync(null);
    }
}
