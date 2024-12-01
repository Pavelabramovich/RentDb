using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Resources.DataTemplates;
using Microsoft.Maui.Graphics.Text;


namespace LilaRent.MobileApp.Views;


public partial class AnnouncementView : ContentPage
{
	public AnnouncementView()
	{
        InitializeComponent();

        RecommendationsCollectionView.ItemTemplate = new IsCrownedAnnouncementTemplate("ClickCommand", this);
    }
}