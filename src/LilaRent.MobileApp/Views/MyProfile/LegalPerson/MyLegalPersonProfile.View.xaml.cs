using CommunityToolkit.Maui.Views;
using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.ViewModels;
using System.ComponentModel;


namespace LilaRent.MobileApp.Views;


public partial class MyLegalPersonProfileView : ContentPage
{
    private readonly Func<Popup> _popupFactory;
    private Popup? _currentPopup;


    public MyLegalPersonProfileView()
    {
        InitializeComponent();

        var template = (DataTemplate)App.Current.Resources.MergedDictionaries.Skip(2).First()["LoadingPopupFactory"];

        _popupFactory = () =>
        {
            var popup = (Popup)template.LoadTemplate();
            popup.CanBeDismissedByTappingOutsideOfPopup = false;
            return popup;
        };
    }

    public new MyLegalPersonProfileViewModel BindingContext => (MyLegalPersonProfileViewModel)base.BindingContext;


    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        BindingContext.PropertyChanged += BindingContext_PropertyChanged;
    }

    private async void BindingContext_PropertyChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == nameof(MyAnnouncementsViewModel.IsServerRequested))
        {
            var isRequested = BindingContext.IsServerRequested;

            if (isRequested)
            {
                _currentPopup = _popupFactory();

                await this.ShowPopupAsync(_currentPopup);
            }
            else
            {
                await _currentPopup!.CloseAsync();
                _currentPopup = null;
            }
        }
    }
}
