using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Core.Validation;
using LilaRent.Application.Validation;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Login), "Login")]
public partial class NewPasswordCodeViewModel : ObservableValidator
{
    private readonly IFakeUserService _userService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private string _login;


    [ObservableProperty]
    [Use<VerificationCodeValidator, string>]
    private string _code;

    [ObservableProperty]
    private string _codeError;


    public NewPasswordCodeViewModel(IFakeUserService userService, INavigationService navigationService)
    {
        _userService = userService;
        _navigationService = navigationService;

        Login = string.Empty;
        
        Code = string.Empty;
        CodeError = string.Empty;
    }

    [RelayCommand]
    private void SendCodeAgain()
    {
        // some logic ...
    }


    [RelayCommand]
    private void Continue()
    {
        if (!ValidateFields())
            return;

        _navigationService.Navigation.Push<NewPasswordEnteringViewModel>(new { Login, Code });
    }

    partial void OnCodeChanged(string value)
    {
        CodeError = string.Empty;
    }

    private bool ValidateFields()
    {
        ValidateAllProperties();

        var codeErrors = GetErrors(nameof(Code));

        if (codeErrors.Any())
        {
            CodeError = codeErrors.First()!.ErrorMessage!;
            return false;
        }

        if (!_userService.ValidateVerificationCodeAsync(Login, Code).Result)
        {
            CodeError = "Invalid verification code";
            return false;
        }

        return true;
    }
}