﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Services;


namespace LilaRent.MobileApp.ViewModels;


public partial class UpdateAnnouncementCompletedViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;


    public UpdateAnnouncementCompletedViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }


    [RelayCommand]
    private void Continue()
    {
        _navigationService.CurrentTabs.Navigation.PopToRoot();
    }
}