using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.Application.Validation;
using LilaRent.MobileApp.Core.Validation;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;
using LilaRent.Requests.Services;
using System.ComponentModel.DataAnnotations;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Id), nameof(Id))]
public partial class RemovingAnnouncementDetailsViewModel : ObservableValidator
{
    private readonly INavigationService _navigationService;
    private readonly IAnnouncementService _announcementService;


    [ObservableProperty]
    [Required(ErrorMessage = "removing announcement detatils is required")]
    [Use<DescriptionValidator, string>("removing announcement detatils")]
    private string _details;

    [ObservableProperty]
    private string _detailsError;

    [ObservableProperty]
    private bool _isServerRequested;


    public Guid Id { get; set; }


    public RemovingAnnouncementDetailsViewModel(INavigationService navigationService, IAnnouncementService announcementService)
    {
        _announcementService = announcementService;
        _navigationService = navigationService;
        
        Details = string.Empty;
        DetailsError = string.Empty;
    }


    [RelayCommand]
    private async Task Remove()
    {
        ValidateAllProperties();

        var detailsErrors = GetErrors(nameof(Details));

        if (detailsErrors.Any())
        {
            DetailsError = detailsErrors.First().ErrorMessage!;
            return;
        }

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

    partial void OnDetailsChanged(string value)
    {
        DetailsError = string.Empty;
    }
}
