using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Core.Validation;
using LilaRent.Application.Validation;
using System.ComponentModel.DataAnnotations;
using LilaRent.MobileApp.Core.Builders;
using LilaRent.Requests.Services;
using LilaRent.Domain.Entities;
using LilaRent.MobileApp.Core;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Builder), nameof(Builder))]
public partial class RegistrationAboutMyselfViewModel : ObservableValidator
{
    private readonly INavigationService _navigationService;
    private readonly IUserService _userService;
    private readonly IProfileManager _profileManager;


    public UserWithProfileBuilder? Builder { get; set; }


    [ObservableProperty]
    [Required(ErrorMessage = "About myself is required")]
    [Use<DescriptionValidator, string>("about myself")]
    private string _aboutMyself;

    [ObservableProperty]
    private string? _imagePath;


    [ObservableProperty]
    private string? _aboutMyselfError;

    [ObservableProperty]
    private string? _imagePathError;


    [ObservableProperty]
    private bool _isServerRequested;


    public RegistrationAboutMyselfViewModel(INavigationService navigationService, IUserService userService, IProfileManager profileManager)
    {
        _navigationService = navigationService;
        _userService = userService;
        _profileManager = profileManager;

        AboutMyself = string.Empty;
        ImagePath = null;

        AboutMyselfError = null;
        ImagePathError = null;
    }


    [RelayCommand]
    private async Task Continue()
    {
        ValidateAllProperties();

        var aboutMyselfErrors = GetErrors(nameof(AboutMyself));

        if (aboutMyselfErrors.Any())
        {
            AboutMyselfError = aboutMyselfErrors.First().ErrorMessage!;
            return;
        }

        if (string.IsNullOrEmpty(ImagePath))
        {
            ImagePathError = "Photo is required";
            return;
        }
        else if (!ImageConstraints.AllowedFormats.Contains(Path.GetExtension(ImagePath)))
        {
            ImagePathError = ImageConstraints.IncorrectFormatError.Replace("{PropertyName}", "image");
            return;
        }

        if (Builder is null)
            throw new InvalidOperationException("Builder is not set.");

        Builder.Description = AboutMyself;
        Builder.ImagePath = ImagePath;

        var userWithProfile = Builder.Build();

        try
        {
            IsServerRequested = true;

            await _userService.AddWithProfileAsync(userWithProfile);

            var profile = await _userService.GetFirstProfileIdAsycn(Builder.Login);

            _profileManager.CurrentProfile = profile;

            IsServerRequested = false;

            _navigationService.Navigation.Push<RegistrationCompletedViewModel>(new { Builder.Name });
        }
        catch (Exception exception)
        {
            IsServerRequested = false;

            // Some other logic of displaying error
        }   
    }


    partial void OnAboutMyselfChanged(string value)
    {
        AboutMyselfError = string.Empty;
    }

    partial void OnImagePathChanged(string? value)
    {
        if (value is not null && !ImageConstraints.AllowedFormats.Contains(Path.GetExtension(value)))
        {
            ImagePath = null;
            ImagePathError = ImageConstraints.IncorrectFormatError.Replace("{PropertyName}", "image");

            return;
        }

        ImagePathError = null;
    }
}
