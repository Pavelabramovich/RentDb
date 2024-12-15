using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;
using LilaRent.Requests.Services;
using LilaRent.MobileApp.Core;
using LilaRent.Application.Dto;


namespace LilaRent.MobileApp.ViewModels;


public partial class HistoryViewModel : TabbedViewModel
{
    private readonly IFakeAnnouncementsService _announcementsService;
    private readonly INavigationService _navigationService;
    private readonly IReservationService _reservationService;
    private readonly IProfileManager _profileManager;


   // public ObservableCollection<AnnouncementSummaryDto> Announcements { get; }

    [ObservableProperty]
    private bool _isServerRequested;


    public HistoryViewModel(
        IServiceProvider serviceProvider, 
		IFakeAnnouncementsService announcementsService, 
		INavigationService navigationService,
        IReservationService reservationService,
        IProfileManager profileManager)
    {
        _announcementsService = announcementsService;
        _navigationService = navigationService;
        _reservationService = reservationService;
        _profileManager = profileManager;

      //  Announcements = [];// _announcementsService.GetAnnouncementsAsync(a => a.Id % 2 != 0).Result.ToObservableCollection();

		Type[] tabs = [/*typeof(HistoryOwnersViewModel),*/ typeof(HistoryAnnouncementsViewModel)];

		foreach(var vm in tabs)
		{
			Tabs.Add((IViewModel)serviceProvider.GetRequiredService(vm));
		}

		CurrentTab = Tabs[0];
    }

    [RelayCommand]
    private async Task LoadData()
    {
        try
        {
            IsServerRequested = true;

            var currentProfileId = _profileManager.CurrentProfile.Id;

            var previos = await _reservationService.GetPrevios(currentProfileId);

            ((HistoryAnnouncementsViewModel)Tabs[0]).Announcements.Clear();

            foreach (var p in previos)
            {
                ((HistoryAnnouncementsViewModel)Tabs[0]).Announcements.Add(p);
            }

            IsServerRequested = false;
        }
        catch (Exception ex)
        {
            IsServerRequested =false;
        }
    }


    //[RelayCommand]
    //public void Click(object arg)
    //{
    //    if (arg is not AnnouncementInfo announcement)
    //        throw new ArgumentException($"Arg must be {typeof(AnnouncementInfo).Name}");

    //    _navigationService.CurrentTabs.Navigation.Push<AnnouncementViewModel>(new { announcement.Id });
    //}
}
