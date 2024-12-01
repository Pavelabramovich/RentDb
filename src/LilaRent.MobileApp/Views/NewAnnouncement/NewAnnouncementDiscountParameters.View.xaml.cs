using LilaRent.MobileApp.ViewModels;
using System.ComponentModel;


namespace LilaRent.MobileApp.Views;


public partial class NewAnnouncementDiscountParametersView : ContentPage
{
    public NewAnnouncementDiscountParametersView()
    {
        InitializeComponent();
    }

    public new NewAnnouncementDiscountParametersViewModel BindingContext => (NewAnnouncementDiscountParametersViewModel)base.BindingContext;


    private void FromEntryChanged(object sender, PropertyChangedEventArgs args)
    {
        BindingContext.IsMinTimeForDiscountEmpty = ((ITextInput)sender).Text.Length == 0;
    }

    private void ToEntryChanged(object sender, PropertyChangedEventArgs args)
    {
        BindingContext.IsMaxTimeForDiscountEmpty = ((ITextInput)sender).Text.Length == 0;
    }
}
