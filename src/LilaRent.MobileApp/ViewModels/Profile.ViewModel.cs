using CommunityToolkit.Mvvm.ComponentModel;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Extensions;
using LilaRent.Application.Dto;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Profile), nameof(Profile))]
public partial class ProfileViewModel : ObservableObject
{
	private readonly IProfileService _profileService;
	private readonly IFakeAnnouncementsService _announcementsService;
	private readonly INavigationService _navigationService;

	public ProfileViewModel(IProfileService profileService, IFakeAnnouncementsService announcementsService, INavigationService navigationService)
	{
		_profileService = profileService;
		_announcementsService = announcementsService;
		_navigationService = navigationService;
	}

	[ObservableProperty]
	private LegalPersonProfileDto _profile;

	[ObservableProperty]
	private IEnumerable<AnnouncementInfo> _announcements;


	[RelayCommand]
	public void ToAppointment()
	{

	}

	[RelayCommand]
	public void Like()
	{

	}

	[RelayCommand]
	private void Share()
	{

	}

	[RelayCommand]
	public void Click(AnnouncementInfo announcement)
	{
        ArgumentNullException
            .ThrowIfNull(announcement, nameof(announcement));

        _navigationService.CurrentTabs.Navigation.Push<AnnouncementViewModel>(new { announcement.Id });
    }
}
