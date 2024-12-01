using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Core.Validation;
using LilaRent.Application.Validation;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Login), "Login")]
public partial class NewPasswordLoginViewModel : ObservableValidator
{
    private readonly IFakeUserService _userService;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    [Use<EmailValidator, string>]
    private string _login;

    [ObservableProperty]
    private string _loginError;


    public NewPasswordLoginViewModel(IFakeUserService userService, INavigationService navigationService)
    {
        _userService = userService;
        _navigationService = navigationService;

        Login = string.Empty;
        LoginError = string.Empty;
    }


    [RelayCommand]
    private void Continue()
    {
        if (!ValidateFields())
            return;

        _navigationService.Navigation.Push<NewPasswordCodeViewModel>(new { Login });
    }

    partial void OnLoginChanged(string value)
    {
        LoginError = string.Empty;
    }

    private bool ValidateFields()
    {
        ValidateAllProperties();

        var loginErrors = GetErrors(nameof(Login));

        if (loginErrors.Any())
        {
            LoginError = loginErrors.First()!.ErrorMessage!;
            return false;
        }

        if (!_userService.IsLoginExists(Login))
        {
            LoginError = "This login is not registered in the system";
            return false;
        }

        return true;
    }
}
