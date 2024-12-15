using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using LilaRent.Domain.Fields;
using System.ComponentModel.DataAnnotations;
using LilaRent.MobileApp.Core.Validation;
using LilaRent.Application.Validation;
using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Core.Builders;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Builder), nameof(Builder))]
public partial class RegistrationCredentialsViewModel : ObservableValidator
{
    private readonly INavigationService _navigationService;
    private readonly IEmailValidationService _emailValidationService;
    private readonly PasswordStrengthChecker _passwordStrengthChecker;

    public UserWithProfileBuilder? Builder { get; set; }


    [ObservableProperty]
    [Required(ErrorMessage = "login is required")]
    [Use<EmailValidator, string>("login")]
    private string _login;

    [ObservableProperty]
    [Required(ErrorMessage = "password is required")]
    [Use<PasswordValidator, string>("password")]
    private string _password;


    [ObservableProperty]
    private string _loginError;

    [ObservableProperty]
    private string _passwordError;

    [ObservableProperty]
    private PasswordStrength? _passwordStrength;


    public RegistrationCredentialsViewModel(INavigationService navigationService, IEmailValidationService emailValidationService)
    {
        _navigationService = navigationService;
        _emailValidationService = emailValidationService;
        _passwordStrengthChecker = new PasswordStrengthChecker();

        Login = string.Empty;
        LoginError = string.Empty;

        Password = string.Empty;
        PasswordError = string.Empty;
    }


    [RelayCommand]
    private async Task Continue()
    {
        if (!await ValidateFields())
            return;

        if (Builder is null)
            throw new InvalidOperationException("Builder is not set.");

        Builder.Login = Login;
        Builder.Password = Password;

        _navigationService.Navigation.Push<RegistrationGoalViewModel>(new { Builder });
    }


    private async Task<bool> ValidateFields()
    {
        ValidateAllProperties();

        var loginErrors = GetErrors(nameof(Login));
        var passwordErrors = GetErrors(nameof(Password));

        if (loginErrors.Any())
        {
            LoginError = loginErrors.First()!.ErrorMessage!;
            return false;
        }

        if (passwordErrors.Any())
        {
            PasswordError = passwordErrors.First()!.ErrorMessage!;
            return false;
        }

        if (await _emailValidationService.IsRegisteredAsync(Login))
        {
            LoginError = "This login is already registered in the system";
            return false;
        }

        return true;
    }


    partial void OnLoginChanged(string value)
    {
        LoginError = string.Empty;
    }

    partial void OnPasswordChanged(string value)
    {
        PasswordStrength = _passwordStrengthChecker.Check(value);
        PasswordError = string.Empty;
    }
}
