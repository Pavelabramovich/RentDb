using LilaRent.MobileApp.Components;
using LilaRent.MobileApp.ViewModels;


namespace LilaRent.MobileApp.Views;


public partial class ExitView : BigButterflyPage
{
    bool shouldDissapire = false;

	public ExitView()
	{
		InitializeComponent();

        Appearing += ExitView_Appearing;
	}

    public new ExitViewModel BindingContext => (ExitViewModel)base.BindingContext;

    private async void ExitView_Appearing(object? sender, EventArgs e)
    {
        if (shouldDissapire)
        {
            shouldDissapire = false;
            await Task.Delay(100);
            BindingContext.ToBackCommand.Execute(null);
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        shouldDissapire = true;
    }
}