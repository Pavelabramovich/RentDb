using CommunityToolkit.Maui.Views;
using LilaRent.MobileApp.ViewModels;
using System.ComponentModel;

namespace LilaRent.MobileApp.Views;


public partial class RemovingAnnouncementReasonView : ContentPage
{
    private readonly Func<Popup> _popupFactory;
    private Popup? _currentPopup;


    public RemovingAnnouncementReasonView()
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

    public new RemovingAnnouncementReasonViewModel BindingContext => (RemovingAnnouncementReasonViewModel)base.BindingContext;


    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        BindingContext.PropertyChanged += BindingContext_PropertyChanged;
    }

    private async void BindingContext_PropertyChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == nameof(RemovingAnnouncementReasonViewModel.IsServerRequested))
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
