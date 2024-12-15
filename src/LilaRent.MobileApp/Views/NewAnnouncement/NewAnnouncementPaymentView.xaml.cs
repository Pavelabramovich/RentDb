using LilaRent.MobileApp.ViewModels;


namespace LilaRent.MobileApp.Views;


public partial class NewAnnouncementPaymentView : ContentPage
{
    private bool navigated = false;

	public NewAnnouncementPaymentView()
	{
		InitializeComponent();

        PaymentWebView.Navigating += PaymentWebView_Navigating;
	}

    public new NewAnnouncementPaymentViewModel BindingContext => (NewAnnouncementPaymentViewModel)base.BindingContext;

    private async void PaymentWebView_Navigating(object? sender, WebNavigatingEventArgs e)
    {
        if (e.Url.Contains("stack") && !navigated)
        {
            navigated = true;
            BindingContext.BackCommand.Execute(null);
        }
        else if (e.Url.Contains("google") && !navigated)
        {
            navigated = true;
            await BindingContext.CompleteCommand.ExecuteAsync(null);
        }
    }
}
