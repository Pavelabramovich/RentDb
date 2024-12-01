using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.Application.Dto;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;
using LilaRent.Requests.Services;
using System.Collections.ObjectModel;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Announcements), nameof(Announcements))]
public partial class MyAnnouncementsViewModel : IViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IAnnouncementService _announcementService;


    private readonly ObservableCollection<AnnouncementSummaryDto> _announcements;

    public IEnumerable<AnnouncementSummaryDto> Announcements
    {
        get => _announcements;
        set
        {
            _announcements.Clear();

            foreach (var announcement in value)
            {
                _announcements.Add(announcement);
            }
        }
    }


    [ObservableProperty]
    private bool _isServerRequested;

    private readonly Dictionary<Guid, AnnouncementUpdatingDetailsDto> _cashe;


    public MyAnnouncementsViewModel(
        INavigationService navigationService, 
        IProfileService profileService, 
        IFakeAnnouncementsService announcementsService,
        IAnnouncementService announcementService)
    {
        _navigationService = navigationService;
        _announcementService = announcementService;

        _cashe = [];

        _announcements = [];

      //  var currentProfile = profileService.CurrentProfile;
       // Announcements = announcementsService.GetAnnouncements(a => a.Profile.Id == currentProfile.Id).Result.ToObservableCollection();
    }

    [RelayCommand]
    public async Task Click(AnnouncementSummaryDto announcement)
    {
        var id = announcement.Id;
            
            // Guid.Parse("64690b33-01a1-4fe7-b389-d2fcef769e13");

        try
        {
            IsServerRequested = true;

            //if (!_cashe.TryGetValue(id, out var announcementDetails))
            //{
            var    announcementDetails = await _announcementService.GetByIdAsync(id);
            //    _cashe[id] = announcementDetails;
            //}

            IsServerRequested = false;

            _navigationService.CurrentTabs.Navigation.Push<MyAnnouncementViewModel>(new { AnnouncementDetails = announcementDetails });
        }
        catch (Exception ex)
        {
            IsServerRequested = false;

            // Other error handling...
        }
    }
}
