using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.Application.Dto;
using LilaRent.Domain.Entities;
using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Core.Builders;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;
using LilaRent.Requests.Services;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Builder), nameof(Builder))]
[QueryProperty(nameof(PaymentUrl), nameof(PaymentUrl))] 
public partial class NewAnnouncementPaymentViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IAnnouncementService _announcementService;
    private readonly IProfileManager _profileService;

    public AnnouncementCreatingDtoBuilder Builder  { get; set; }

    [ObservableProperty]
    public string? _paymentUrl;

    [ObservableProperty]
    public bool _isServerRequested;


    public NewAnnouncementPaymentViewModel(INavigationService navigationService, IAnnouncementService announcementService, IProfileManager profileManager)
    {
        _navigationService = navigationService;
        _announcementService = announcementService;
        _profileService = profileManager;
    }


    [RelayCommand]
    private async Task Complete()
    {
        var profileId = _profileService.CurrentProfile.Id;
        var announcement = Builder.Build(profileId);

        try
        {
            IsServerRequested = true;

            await _announcementService.AddAsync(announcement);

            IsServerRequested = false;

            _navigationService.CurrentTabs.Navigation.PopOverRoot<NewAnnouncementCompletedViewModel>();
        }
        catch (Exception ex)
        {
            IsServerRequested = false;

            // Handle exception in ui
        }
    }


    [RelayCommand]
    private void Back()
    {
        _navigationService.CurrentTabs.Navigation.Pop();
    }

}
