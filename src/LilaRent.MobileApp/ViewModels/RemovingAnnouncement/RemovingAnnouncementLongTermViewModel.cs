using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;
using LilaRent.Requests.Services;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Id), nameof(Id))]
public partial class RemovingAnnouncementLongTermViewModel : IViewModel
{
    private IAnnouncementService _announcementService;
    private INavigationService _navigationService;


    public Guid Id { get; set; }

    [ObservableProperty]
    private bool _isServerRequested;


    public RemovingAnnouncementLongTermViewModel(INavigationService navigationService, IAnnouncementService announcementService)
    {
        _announcementService = announcementService;
        _navigationService = navigationService;
    }


    [RelayCommand]
    private async Task Remove()
    {
        try
        {
            IsServerRequested = true;

            await _announcementService.DeleteAsync(Id);

            IsServerRequested = false;

            _navigationService.CurrentTabs.Navigation.Push<RemovingAnnouncementCompletedViewModel>();
        }
        catch (Exception ex)
        {
            IsServerRequested = true;

            // Handle exception error
        }
    }
}
