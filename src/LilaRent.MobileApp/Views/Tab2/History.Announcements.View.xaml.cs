using LilaRent.MobileApp.Components.Core;
using LilaRent.MobileApp.Resources.DataTemplates;
using LilaRent.MobileApp.ViewModels;


namespace LilaRent.MobileApp.Views;


public partial class HistoryAnnouncementsView : ContentPage
{
	public HistoryAnnouncementsView()
	{
		InitializeComponent();

        Appearing += HistoryAnnouncementsView_Appearing;
    }

    public new HistoryAnnouncementsViewModel BindingContext => (HistoryAnnouncementsViewModel)base.BindingContext;


    private async void HistoryAnnouncementsView_Appearing(object? sender, EventArgs e)
    {
        await BindingContext.InitCommand.ExecuteAsync(parameter: null);
    }
}
