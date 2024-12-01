using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Services;
using System.Collections.ObjectModel;
using LilaRent.MobileApp.Extensions;
using LilaRent.Requests.Services;
using LilaRent.Application.Dto;
using System.ComponentModel;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Filters), "Filters")]
public partial class CatalogViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
	private readonly IFakeAnnouncementsService _announcementsListService;
	private readonly IAnnouncementService _announcementService;

	private Func<AnnouncementInfo, bool>? _filters;
	public Func<AnnouncementInfo, bool>? Filters 
	{ 
		get => _filters; 
		set
		{
			_filters = value;
			//Announcements.Clear();

			//IEnumerable<AnnouncementInfo> announcements;

			//if (value is null)
			//{
			//	announcements = _announcementsListService.GetAnnouncements(a => true).Result;
   //         }
			//else
			//{
   //             announcements = _announcementsListService.GetAnnouncements(value).Result;
   //         }

   //         foreach (var a in announcements)
			//{
			//	Announcements.Add(a);
			//}
		}
	}

	public ObservableCollection<AnnouncementSummaryDto> Announcements { get; }

    [ObservableProperty]
    private AnnouncementInfo _recommendation;

	[ObservableProperty]
	private bool _isServerRequested;


    public CatalogViewModel(INavigationService navigationService, IFakeAnnouncementsService announcementsListService, IAnnouncementService announcementService)
    {
        _navigationService = navigationService;
		_announcementsListService = announcementsListService;

		_announcementService = announcementService;

		Announcements = [];
        Recommendation = _announcementsListService.GetAnnouncements(a => true).Result.First();
	}

	[RelayCommand]
	public async Task Load()
	{
		IsServerRequested = true;

		Announcements.Clear();

		var announcements = await _announcementService.GetAllAsync();

		foreach (var a in announcements)
		{
			Announcements.Add(a);
		}

		IsServerRequested = false;
	}


	[RelayCommand]
	public async Task Click(AnnouncementSummaryDto announcement)
	{
		var id = announcement.Id;

		try
		{
			IsServerRequested = true;

			var announcementDetails = await _announcementService.GetById2Async(id);

			IsServerRequested = false;

			_navigationService.CurrentTabs.Navigation.Push<AnnouncementViewModel>(new { Announcement = announcementDetails });
		}
		catch (Exception ex)
		{
			IsServerRequested = false;

			// Handle error
		}
	}

	[RelayCommand]
	public void ToFilters() 
	{
		_navigationService.CurrentTabs.Navigation.Push<FilterChoiceViewModel>();
	}
}
