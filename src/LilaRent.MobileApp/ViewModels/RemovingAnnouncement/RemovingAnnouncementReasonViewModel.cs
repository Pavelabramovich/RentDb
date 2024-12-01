using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;
using LilaRent.Requests.Services;


namespace LilaRent.MobileApp.ViewModels;


public enum RemovingAnnouncementReason
{ 
    LongTermRenting = 0,
    RentObjectSelled = 1,
    ClosedBusiness = 2,
    DontLikeApp = 3,
    OtherReason = 4,
    JustDelete = 5,
}


[QueryProperty(nameof(Id), nameof(Id))]
public partial class RemovingAnnouncementReasonViewModel : IViewModel
{
    private readonly IAnnouncementService _announcementService;
    private readonly INavigationService _navigationService;


    [ObservableProperty]
    private RemovingAnnouncementReason _reason;


    public Guid Id { get; set; }


    [ObservableProperty]
    private bool _isServerRequested;


    public RemovingAnnouncementReasonViewModel(INavigationService navigationService, IAnnouncementService announcementService)
    {
        _announcementService = announcementService;
        _navigationService = navigationService;
    }


    [RelayCommand]
    private async Task Send()
    {
        if (Reason == RemovingAnnouncementReason.LongTermRenting)
        {
            _navigationService.CurrentTabs.Navigation.Push<RemovingAnnouncementLongTermViewModel>(new { Id });
        }
        else if (Reason is RemovingAnnouncementReason.RentObjectSelled or RemovingAnnouncementReason.ClosedBusiness)
        {
            _navigationService.CurrentTabs.Navigation.Push<RemovingAnnouncementClosedViewModel>(new { Id });
        }
        else if (Reason is RemovingAnnouncementReason.DontLikeApp or RemovingAnnouncementReason.OtherReason)
        {
            _navigationService.CurrentTabs.Navigation.Push<RemovingAnnouncementDetailsViewModel>(new { Id });
        }
        else
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
}
