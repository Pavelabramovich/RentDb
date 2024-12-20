﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Services;
using LilaRent.Application.Dto;
using LilaRent.Requests.Services;
using LilaRent.Domain.Entities;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Announcement), nameof(Announcement))]
public partial class AnnouncementViewModel : ObservableObject
{
	private readonly IAnnouncementService _announcementService;
	private readonly INavigationService _navigationService;
	private readonly IUserService _userService;


	[ObservableProperty]
	private bool _isServerRequested;


	public AnnouncementViewModel(IAnnouncementService annnouncementsService, INavigationService navigation, IUserService userService)
	{
		_announcementService = annnouncementsService;
		_navigationService = navigation;
		_userService = userService;
	}

	//private long _announcementId;
	//public long AnnouncementId
	//{
	//	get => _announcementId;
	//	set
	//	{
	//		_announcementId = value;
	//		ThisAnnouncementInfo = _announcementsService.GetAnnouncementById(_announcementId);
	//		Recomendantions = _announcementsService.GetAnnouncements(a => true).Result;
	//	}
	//}

	[ObservableProperty]
	private AnnouncementDetailsDto _announcement;

	[RelayCommand]
	private async void ToProfile()
	{
		var profileId = Announcement.ProfileId;

		try
		{
			IsServerRequested = true;

			var profile = await _userService.GetLegalPersonProfile(profileId);

			IsServerRequested = false;

			_navigationService.CurrentTabs.Navigation.Push<ProfileViewModel>(new { Profile = profile });
		}
		catch (Exception ex)
		{
			IsServerRequested = true;

			// Exception handling
		}
	}


	[RelayCommand]
	private async Task ToAppointment()
	{
		try
		{
			IsServerRequested = true;

			var reservations = await _announcementService.GetReservations(Announcement.Id);

			IsServerRequested = false;

			_navigationService.CurrentTabs.Navigation.Push<ConfirmationViewModel>(new { Announcement, Reservations = reservations });
		}
		catch (Exception ex)
		{
			
			IsServerRequested = false;

			// Handle exceptions
		}
	}

	[RelayCommand]
	private void Like()
	{

	}

	[RelayCommand]
	private void Share()
	{

	}


	[ObservableProperty]
	private IEnumerable<AnnouncementInfo> _recomendantions;

	[RelayCommand]
	private void Click(object arg)
	{
		if (arg is not AnnouncementInfo info)
			throw new ArgumentException("Arg must be Announcement Info");

		_navigationService.CurrentTabs.Navigation.Push(typeof(AnnouncementViewModel), new { Id = info!.Id });
	}
}
