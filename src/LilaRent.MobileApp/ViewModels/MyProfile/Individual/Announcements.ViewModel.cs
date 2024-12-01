using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;


namespace LilaRent.MobileApp.ViewModels;


public partial class AnnouncementsViewModel : ObservableObject
{
    private readonly IFakeAnnouncementsService _announcementsService;
    private readonly INavigationService _navigationService;


    [ObservableProperty]
    private string _title = "Announcements";


    public ObservableCollection<AnnouncementInfo> Announcements { get; }


    public AnnouncementsViewModel(IFakeAnnouncementsService announcementsService, INavigationService navigationService)
    {
        _announcementsService = announcementsService;
        _navigationService = navigationService;

        Announcements = _announcementsService.GetAnnouncements(a => a.Id % 2 == 0).Result.ToObservableCollection();
    }

    [RelayCommand]
    public void Click(object arg)
    {
        if (arg is not AnnouncementInfo announcement)
            throw new ArgumentException("Arg must be Announcement info");

        _navigationService.CurrentTabs.Navigation.Push<AnnouncementViewModel>(new { announcement.Id });
    }
}
