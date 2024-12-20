﻿using CommunityToolkit.Maui.Views;
using LilaRent.MobileApp.ViewModels;
using System.ComponentModel;
namespace LilaRent.MobileApp.Views;


public partial class NewAnnouncementDurationView : ContentPage
{
    private readonly Func<Popup> _popupFactory;
    private Popup? _currentPopup;


    public NewAnnouncementDurationView()
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

    public new NewAnnouncementDurationViewModel BindingContext => (NewAnnouncementDurationViewModel)base.BindingContext;


    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        BindingContext.PropertyChanged += BindingContext_PropertyChanged;
    }

    private async void BindingContext_PropertyChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == nameof(NewAnnouncementDurationViewModel.IsServerRequested))
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

    //private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    //{
    //    CheckBox.IsChecked = !CheckBox.IsChecked;
    //}

    private void TapGestureRecognizer2_Tapped(object sender, TappedEventArgs e)
    {
        CheckBox2.IsChecked = !CheckBox2.IsChecked;
    }
}
