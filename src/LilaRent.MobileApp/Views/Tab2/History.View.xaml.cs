using LilaRent.MobileApp.Components.Core;
using LilaRent.MobileApp.ViewModels;


namespace LilaRent.MobileApp.Views;


public partial class HistoryView : TabbedViewView
{
	public HistoryView(IServiceProvider serviceProvider): base(serviceProvider)
	{
		InitializeComponent();

        Appearing += HistoryView_Appearing;
	}

    private async void HistoryView_Appearing(object? sender, EventArgs e)
    {
        await ((HistoryViewModel)BindingContext).LoadDataCommand.ExecuteAsync(null);
    }
}