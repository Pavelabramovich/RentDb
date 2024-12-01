using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Resources.Localization;
using System.Globalization;


namespace LilaRent.MobileApp.Views;


public partial class LandingView : ContentPage
{
    private readonly ILocalizationManager _localizationManager;


	public LandingView(ILocalizationManager localizationManager)
	{
		InitializeComponent();

        _localizationManager = localizationManager;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
        _localizationManager.CurrentCulture = new CultureInfo("RU-ru");
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        _localizationManager.CurrentCulture = new CultureInfo("EN-us");
    }
}