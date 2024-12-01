using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core.Extensions;


namespace LilaRent.MobileApp.ViewModels;


partial class HistoryAnnouncementsViewModel : ObservableObject
{
	private readonly IFakeAnnouncementsService _announcementsService;
	private readonly INavigationService _navigationService;

	public ObservableCollection<AnnouncementInfo> Announcements { get; }

    [ObservableProperty]
    private string _title = "Working places";


    public HistoryAnnouncementsViewModel(INavigationService navigationService, IFakeAnnouncementsService announcementsService)
	{
		_announcementsService = announcementsService;
		_navigationService = navigationService;

		Announcements = _announcementsService.GetHistoryAsync().Result.ToObservableCollection();
	}

	[RelayCommand]
	public void Click(object arg)
	{
		if (arg is not AnnouncementInfo announcement)
			throw new ArgumentException("Arg must be Announcement Basics");

		_navigationService.CurrentTabs.Navigation.Push<AnnouncementViewModel>(new { announcement.Id });
	}
}
