using CommunityToolkit.Maui.Views;
using System.ComponentModel;
using PopupState = (System.Func<CommunityToolkit.Maui.Views.Popup> Factory, CommunityToolkit.Maui.Views.Popup? Popup);


namespace LilaRent.MobileApp.Behaviors;


public partial class LoadingBehaviour : Behavior<Page>
{
    private static readonly BindableProperty PopupStateProperty = BindableProperty.CreateAttached(
        nameof(GetPopupState).Replace("Get", string.Empty),
        typeof(PopupState),
        typeof(LoadingBehaviour),
        defaultValue: default(PopupState)
    );

    public static readonly BindableProperty IsLoadingProperty = BindableProperty.CreateAttached(
        nameof(GetIsLoading).Replace("Get", string.Empty),
        typeof(bool),
        typeof(LoadingBehaviour),
        defaultValue: false,
        propertyChanged: IsLoadingPropertyChanged
    );

    private static PopupState GetPopupState(BindableObject page)
    {
        return (PopupState)page.GetValue(PopupStateProperty);
    }

    private static void SetPopupState(BindableObject page, PopupState value)
    {
        page.SetValue(PopupStateProperty, value);
    }


    public static bool GetIsLoading(BindableObject page)
    {
        return (bool)page.GetValue(IsLoadingProperty);
    }

    public static void SetIsLoading(BindableObject page, bool value)
    {
        page.SetValue(IsLoadingProperty, value);
    }



    private static async void IsLoadingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var page = (Page)bindable;
        var isLoading = (bool)newValue;

        var (popupFactory, currentPopup) = GetPopupState(page);

        if (popupFactory is null)
        {
            var template = (DataTemplate)App.Current.Resources.MergedDictionaries.Skip(2).First()["LoadingPopupFactory"];

            popupFactory = () =>
            {
                var popup = (Popup)template.LoadTemplate();
                popup.CanBeDismissedByTappingOutsideOfPopup = false;
                return popup;
            };
        }

        if (isLoading)
        {
            var newPopup = popupFactory();

            SetPopupState(page, (popupFactory, newPopup));
            await page.ShowPopupAsync(newPopup);
        }
        else
        {       
            await currentPopup!.CloseAsync();
            SetPopupState(page, (popupFactory, null));
        }
    }


    //protected override void OnAttachedTo(Page page)
    //{
    //    if (page.BindingContext is INotifyPropertyChanged notify)
    //    {
    //        notify.PropertyChanged += BindingContext_PropertyChanged;
    //    }

    //    page.BindingContextChanged += Page_BindingContextChanged;
    //    page.PropertyChanging += Page_PropertyChanging;
    //}

    //private void Page_BindingContextChanged(object? sender, EventArgs e)
    //{
    //    if (sender not null && ((Page)sender).BindingContext is INotifyPropertyChanged notify)
    //    {
    //        notify.PropertyChanged += BindingContext_PropertyChanged;
    //    }
    //}

    //private void Page_PropertyChanging(object sender, Microsoft.Maui.Controls.PropertyChangingEventArgs e)
    //{
    //    if (e.PropertyName == nameof(Page.BindingContext))
    //    {
    //        if (((Page)sender).BindingContext is INotifyPropertyChanged notify)
    //        {
    //            notify.PropertyChanged -= BindingContext_PropertyChanged;
    //        }
    //    }
    //}



    //protected override void OnDetachingFrom(Page page)
    //{
    //    if (page.BindingContext is INotifyPropertyChanged notify)
    //    {
    //        notify.PropertyChanged -= BindingContext_PropertyChanged;
    //    }

    //    page.BindingContextChanged -= Page_BindingContextChanged;
    //    page.PropertyChanging -= Page_PropertyChanging;
    //}


    //private async void BindingContext_PropertyChanged(object? sender, PropertyChangedEventArgs args)
    //{
    //    if (args.PropertyName == nameof(NewAnnouncementDurationViewModel.IsServerRequested))
    //    {
           
    //    }
    //}
}
