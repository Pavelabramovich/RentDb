using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;
using LilaRent.Application.Dto;
using LilaRent.Requests.Services;
using LilaRent.MobileApp.Core;


namespace LilaRent.MobileApp.ViewModels;


public partial class HistoryAnnouncementsViewModel : ObservableObject
{
	private readonly IFakeAnnouncementsService _announcementsService;
	private readonly INavigationService _navigationService;
	private readonly IUserService _userService;
	private readonly IProfileManager _profileManager;

	public ObservableCollection<AnnouncementSummaryDto> Announcements { get; }

	public ObservableCollection<ReservationSummaryDto> Reservations { get; }


    [ObservableProperty]
    private string _title = "Working places";


    public HistoryAnnouncementsViewModel(
		INavigationService navigationService, IFakeAnnouncementsService announcementsService, IUserService userService, IProfileManager profileManager)
	{
		_announcementsService = announcementsService;
		_navigationService = navigationService;
		_userService = userService;
		_profileManager = profileManager;

		Announcements = [];// _announcementsService.GetHistoryAsync().Result.ToObservableCollection();
		Reservations = [];
	}


	[RelayCommand]
	private async Task Init()
	{
		var profileId = _profileManager.CurrentProfile.Id;
		var prfileType = _profileManager.CurrentProfile.LegalEntityType;

		var reservations = await _userService.GetProfileReservations(profileId, prfileType);

		Reservations.Clear();

		foreach (var reservation in reservations)
		{
			Reservations.Add(reservation);
		}
	}
	
	[RelayCommand]
	public void Click(object arg)
	{
		if (arg is not AnnouncementSummaryDto announcement)
			throw new ArgumentException("Arg must be Announcement Basics");




		//_navigationService.CurrentTabs.Navigation.Push<AnnouncementViewModel>(new { announcement.Id });
	}
}
