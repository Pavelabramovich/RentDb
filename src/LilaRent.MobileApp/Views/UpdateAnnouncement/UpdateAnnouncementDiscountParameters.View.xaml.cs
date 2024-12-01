using LilaRent.MobileApp.ViewModels;
using System.ComponentModel;


namespace LilaRent.MobileApp.Views;


public partial class UpdateAnnouncementDiscountParametersView : ContentPage
{
    public UpdateAnnouncementDiscountParametersView()
    {
        InitializeComponent();
    }

    public new UpdateAnnouncementDiscountParametersViewModel BindingContext => (UpdateAnnouncementDiscountParametersViewModel)base.BindingContext;


    private void FromEntryChanged(object sender, PropertyChangedEventArgs args)
    {
        BindingContext.IsMinTimeForDiscountEmpty = ((ITextInput)sender).Text.Length == 0;
    }

    private void ToEntryChanged(object sender, PropertyChangedEventArgs args)
    {
        BindingContext.IsMaxTimeForDiscountEmpty = ((ITextInput)sender).Text.Length == 0;
    }
}
