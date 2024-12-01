using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Entities;
using CommunityToolkit.Maui.Core.Platform;
using LilaRent.MobileApp.Core.Validation;
using LilaRent.Application.Validation;
using System.ComponentModel.DataAnnotations;
using LilaRent.Domain.Fields;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Login), "Login")]
[QueryProperty(nameof(Code), "Code")]
public partial class NewPasswordEnteringViewModel : ObservableValidator
{
    private readonly INavigationService _navigationService;
    private readonly PasswordStrengthChecker _passwordStrengthChecker;


    [ObservableProperty]
    private string _login;

    [ObservableProperty]
    private string _code;


    [ObservableProperty]
    [Required(ErrorMessage = "password is required")]
    [Use<PasswordValidator, string>]
    private string _newPassword;

    [ObservableProperty]
    [Compare(nameof(NewPassword), ErrorMessage = "passwords do not match")]
    private string _newRepeatedPassword;


    [ObservableProperty]
    private string _newPasswordError;

    [ObservableProperty]
    private string _newRepeatedPasswordError;

    [ObservableProperty]
    private PasswordStrength? _passwordStrength;



    public NewPasswordEnteringViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        _passwordStrengthChecker = new PasswordStrengthChecker();

        Login = string.Empty;
        Code = string.Empty;

        NewPassword = string.Empty;
        NewRepeatedPassword = string.Empty;

        NewPasswordError = string.Empty;
        NewRepeatedPasswordError = string.Empty;
    }


    [RelayCommand]
    private void Continue()
    {
        if (!ValidateFields()) 
            return;

        _navigationService.Navigation.Push<NewPasswordCompletedViewModel>(new { Login });
    }

    private bool ValidateFields()
    {
        ValidateAllProperties();

        var newPasswordErrors = GetErrors(nameof(NewPassword));
        var newRepeatedPasswordErrors = GetErrors(nameof(NewRepeatedPassword));

        if (newPasswordErrors.Any())
        {
            NewPasswordError = newPasswordErrors.First()!.ErrorMessage!;
            return false;
        }

        if (newRepeatedPasswordErrors.Any())
        {
            NewRepeatedPasswordError = newRepeatedPasswordErrors.First()!.ErrorMessage!;
            return false;
        }

        return true;
    }

    partial void OnNewPasswordChanged(string value)
    {
        PasswordStrength = _passwordStrengthChecker.Check(value);

        NewPasswordError = string.Empty;
        NewRepeatedPasswordError = string.Empty;
    }

    partial void OnNewRepeatedPasswordChanged(string value)
    {
        NewRepeatedPasswordError = string.Empty;
    }
}
