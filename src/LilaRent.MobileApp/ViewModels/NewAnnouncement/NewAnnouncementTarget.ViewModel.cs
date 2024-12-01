using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using System.ComponentModel.DataAnnotations;
using LilaRent.MobileApp.Core.Validation;
using LilaRent.Application.Validation;
using LilaRent.MobileApp.Core.Builders;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Builder), nameof(Builder))]
internal partial class NewAnnouncementTargetViewModel : ObservableValidator
{
    private readonly INavigationService _navigationService;

    public AnnouncementCreatingDtoBuilder? Builder { get; set; }


    [ObservableProperty]
    [Required(ErrorMessage = "rent object name is required")]
    [Use<NameValidator, string>("rent object name")]
    private string _rentObjectName;

    [ObservableProperty]
    [Required(ErrorMessage = "address is required")]
    [Use<AddressValidator, string>("address")]
    private string _address;

    [ObservableProperty]
    [Required(ErrorMessage = "description is required")]
    [Use<DescriptionValidator, string>("description")]
    private string _description;


    [ObservableProperty]
    private string? _rentObjectNameError;

    [ObservableProperty]
    private string? _addressError;

    [ObservableProperty]
    private string? _descriptionError;


    public NewAnnouncementTargetViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        RentObjectName = string.Empty;
        Description = string.Empty;
        Address = string.Empty;

        RentObjectNameError = null;
        AddressError = null;
        DescriptionError = null;
    }


    [RelayCommand]
    private void Next()
    {
        if (!ValidateFields()) 
            return;

        if (Builder is null)
            throw new InvalidOperationException("Announcement builder is not set");

        Builder.RentObjectName = RentObjectName;
        Builder.Address = Address;
        Builder.Description = Description;

        _navigationService.CurrentTabs.Navigation.Push<NewAnnouncementFilesViewModel>(new { Builder });
    }

    [RelayCommand]
    private void Back()
    {
        _navigationService.CurrentTabs.Navigation.Pop();
    }


    partial void OnRentObjectNameChanged(string value)
    {
        RentObjectNameError = null;
    }

    partial void OnAddressChanged(string value)
    {
        AddressError = null;
    }

    partial void OnDescriptionChanged(string value)
    {
        DescriptionError = null;
    }


    private bool ValidateFields()
    {
        ValidateAllProperties();

        var rentObjectNameErrors = GetErrors(nameof(RentObjectName));
        var addressErrors = GetErrors(nameof(Address));
        var descriptionErrors = GetErrors(nameof(Description));

        var isValid = true;

        if (rentObjectNameErrors.Any())
        {
            RentObjectNameError = rentObjectNameErrors.First()!.ErrorMessage!;
            isValid = false;
        }

        if (addressErrors.Any())
        {
            AddressError = addressErrors.First()!.ErrorMessage!;
            isValid = false;
        }

        if (descriptionErrors.Any())
        {
            DescriptionError = descriptionErrors.First()!.ErrorMessage!;
            isValid = false;
        }

        return isValid;
    }
}
