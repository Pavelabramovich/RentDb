using CommunityToolkit.Maui.Views;
using LilaRent.MobileApp.Resources.DataTemplates;
using LilaRent.MobileApp.ViewModels;
using System.ComponentModel;


namespace LilaRent.MobileApp.Views;


public partial class MyAnnouncementsView : ContentPage
{
    private readonly Func<Popup> _popupFactory;
    private Popup? _currentPopup;


    public MyAnnouncementsView()
    {
        InitializeComponent();

        AnnouncementsCollectionView.ItemTemplate = new IsCrownedAnnouncementTemplate("ClickCommand", this);

        var template = (DataTemplate)App.Current.Resources.MergedDictionaries.Skip(2).First()["LoadingPopupFactory"];

        _popupFactory = () =>
        {
            var popup = (Popup)template.LoadTemplate();
            popup.CanBeDismissedByTappingOutsideOfPopup = false;
            return popup;
        };
    }

    public new MyAnnouncementsViewModel BindingContext => (MyAnnouncementsViewModel)base.BindingContext;


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