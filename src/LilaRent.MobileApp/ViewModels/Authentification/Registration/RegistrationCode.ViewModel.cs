using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Core.Validation;
using LilaRent.Application.Validation;
using LilaRent.MobileApp.Core.Builders;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Builder), nameof(Builder))]
public partial class RegistrationCodeViewModel : ObservableValidator
{
    private readonly INavigationService _navigationService;
    private readonly IFakeUserService _userService;

    public UserWithProfileBuilder? Builder { get; set; }


    [ObservableProperty]
    [Use<VerificationCodeValidator, string>]
    private string _code;

    [ObservableProperty]
    private string _codeError;


    public RegistrationCodeViewModel(INavigationService navigationService, IFakeUserService userService)
    {
        _navigationService = navigationService;
        _userService = userService;

        Code = string.Empty;
        CodeError = string.Empty;
    }


    [RelayCommand]
    private void SendCodeAgain()
    {
        // some logic ...
    }

    [RelayCommand]
    private async Task Continue()
    {
        if (!await ValidateFields())
            return;

        if (Builder is null)
            throw new InvalidOperationException("Builder is not set.");

        _navigationService.Navigation.Push<RegistrationGoalViewModel>(new { Builder });
    }


    private async Task<bool> ValidateFields()
    {
        ValidateAllProperties();

        var codeErrors = GetErrors(nameof(Code));

        if (codeErrors.Any())
        {
            CodeError = codeErrors.First()!.ErrorMessage!;
            return false;
        }

        if (!await _userService.ValidateVerificationCodeAsync(Builder.Login, Code))
        {
            CodeError = "Invalid verification code";
            return false;
        }

        return true;
    }


    partial void OnCodeChanged(string value)
    {
        CodeError = string.Empty;
    }
}