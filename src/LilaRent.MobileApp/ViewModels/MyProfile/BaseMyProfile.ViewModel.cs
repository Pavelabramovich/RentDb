using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.Application.Dto;
using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;


namespace LilaRent.MobileApp.ViewModels;


public partial class BaseMyProfileViewModel : IViewModel
{
    protected readonly INavigationService _navigationService;
    private readonly IProfileManager _profileService;

    public BaseMyProfileViewModel(INavigationService navigationService, IProfileManager profileService)
    {
        _navigationService = navigationService;
        _profileService = profileService;

        MyProfile = profileService.CurrentProfile;
    }

    [ObservableProperty]
    protected ProfileSummaryDto _myProfile;


    [RelayCommand]
    private void ToNotifications()
    {
        // _navigationService.CurrentTabs.Navigation.Push<NotificationsViewModel>();
    }

    [RelayCommand]
    private void ToFeedback()
    {
        // _navigationService.CurrentTabs.Navigation.Push<FeedbackViewModel>();
    }

    [RelayCommand]
    private void ToAboutUs()
    {
        _navigationService.CurrentTabs.Navigation.Push<AboutUsViewModel>();
    }

    [RelayCommand]
    private void Exit()
    {
        _navigationService.CurrentTabs.Navigation.Push<ExitViewModel>();
    }
}
