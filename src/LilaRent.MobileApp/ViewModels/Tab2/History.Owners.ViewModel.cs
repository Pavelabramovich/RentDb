using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using System.Collections.ObjectModel;


namespace LilaRent.MobileApp.ViewModels;


partial class HistoryOwnersViewModel : ObservableObject
{
	private readonly IProfileService _profileService;

	private readonly INavigationService _navigationService;

	[ObservableProperty]
	private string _title = "Koworkings";

	public ObservableCollection<Profile> Profiles { get; }

	public HistoryOwnersViewModel(IProfileService announcementsService, INavigationService navigationService)
	{
		_profileService = announcementsService;
		_navigationService = navigationService;

		Profiles = [.. _profileService.GetHistory()];
	}

	[RelayCommand]
	void ToProfile(Profile? profile)
	{
		ArgumentNullException
			.ThrowIfNull(profile, nameof(profile));

		_navigationService.CurrentTabs.Navigation.Push<ProfileViewModel>(new { profile.Id });
	}
}
