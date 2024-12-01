using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Core.Builders;
using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;
using LilaRent.Requests.Services;



namespace LilaRent.MobileApp.ViewModels;


public partial class MyLegalPersonProfileViewModel : BaseMyProfileViewModel
{
    private readonly IAnnouncementService _announcementService;
    private readonly IAuthService _authService;

    [ObservableProperty]
    private bool _isServerRequested;


    public MyLegalPersonProfileViewModel(
        INavigationService navigationService, 
        IAnnouncementService announcementService,
        IProfileManager profileService, 
        IAuthService authService)
        : base(navigationService, profileService)
    {
        _announcementService = announcementService;
        _authService = authService;

        ClientsCount = 15;
    }

    [ObservableProperty]
    private int _clientsCount;


    [RelayCommand]
    private async Task ToMyAnnouncements()
    {
        var profileId = MyProfile.Id; 

        try
        {
            IsServerRequested = true;

            var accessToken = await SecureStorage.Default.GetAsync("AccessToken");

            if (accessToken is null)
            {
                IsServerRequested = false;
                return;
            }

            var announcements = await _announcementService.GetByProfileIdAsync(profileId, accessToken);

            IsServerRequested = false;

            _navigationService.CurrentTabs.Navigation.Push<MyAnnouncementsViewModel>(new { Announcements = announcements });
        }
        catch (Exception ex)
        {
            try
            {
                var accessToken = await SecureStorage.Default.GetAsync("AccessToken");
                var refreshToken = await SecureStorage.Default.GetAsync("RefreshToken");

                var newTokens = await _authService.RefreshAsync(new Application.Dto.TokensDto() { AccessToken = accessToken, RefreshToken = refreshToken });

                await SecureStorage.Default.SetAsync("AccessToken", newTokens.AccessToken);
                await SecureStorage.Default.SetAsync("RefreshToken", newTokens.RefreshToken);


                var announcements = await _announcementService.GetByProfileIdAsync(profileId, newTokens.AccessToken);

                IsServerRequested = false;

                _navigationService.CurrentTabs.Navigation.Push<MyAnnouncementsViewModel>(new { Announcements = announcements });
            }
            catch (Exception ex2)
            {
                IsServerRequested = false;

                // Error handling...
            }
        }
    }

    [RelayCommand]
    private void ToClients()
    {
        // _navigationService.CurrentTabs.Navigation.Push<ClientsViewModel>();
    }

    [RelayCommand]
    private void ToCreateAnnouncement()
    {
        var announcementBuilder = new AnnouncementCreatingDtoBuilder();

        _navigationService.CurrentTabs.Navigation.Push<NewAnnouncementTargetViewModel>(new { Builder = announcementBuilder });
    }
}
