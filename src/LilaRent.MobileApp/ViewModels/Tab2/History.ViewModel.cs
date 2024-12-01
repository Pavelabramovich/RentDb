using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;


namespace LilaRent.MobileApp.ViewModels;


public partial class HistoryViewModel : TabbedViewModel
{
    private readonly IFakeAnnouncementsService _announcementsService;
    private readonly INavigationService _navigationService;


    public ObservableCollection<AnnouncementInfo> Announcements { get; }
		

    public HistoryViewModel(
        IServiceProvider serviceProvider, 
		IFakeAnnouncementsService announcementsService, 
		INavigationService navigationService)
    {
        _announcementsService = announcementsService;
        _navigationService = navigationService;

        Announcements = _announcementsService.GetAnnouncementsAsync(a => a.Id % 2 != 0).Result.ToObservableCollection();

		Type[] tabs = [typeof(HistoryOwnersViewModel), typeof(HistoryAnnouncementsViewModel)];

		foreach(var vm in tabs)
		{
			Tabs.Add((IViewModel)serviceProvider.GetRequiredService(vm));
		}

		CurrentTab = Tabs[0];
    }

    [RelayCommand]
    public void Click(object arg)
    {
        if (arg is not AnnouncementInfo announcement)
            throw new ArgumentException($"Arg must be {typeof(AnnouncementInfo).Name}");

        _navigationService.CurrentTabs.Navigation.Push<AnnouncementViewModel>(new { announcement.Id });
    }
}
