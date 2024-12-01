using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using System.ComponentModel.DataAnnotations;
using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Core.Validation;
using LilaRent.Application.Validation;
using LilaRent.Application.Dto;
using LilaRent.Requests.Services;
using System.Text.Json;


namespace LilaRent.MobileApp.ViewModels;


public partial class LoginViewModel : ObservableValidator
{
    private readonly INavigationService _navigationService;
    private readonly IProfileManager _profileManager;
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    [ObservableProperty]
    [Required(ErrorMessage = "login is required")]
    [Use<EmailValidator, string>("login")]
    private string _login;

    [ObservableProperty]
    [Required(ErrorMessage = "password is required")]
    [Use<PasswordValidator, string>("password")]
    private string _password;


    [ObservableProperty]
    private string? _loginError;

    [ObservableProperty]
    private string? _passwordError;


    [ObservableProperty]
    private bool _isServerRequested;


    public LoginViewModel(INavigationService navigationService, IProfileManager profileService, IAuthService authService, IUserService userService)
    {
        _navigationService = navigationService;
        _profileManager = profileService;
        _userService = userService;
        _authService = authService;

        Login = string.Empty;
        Password = string.Empty;

        LoginError = null;
        PasswordError = null;
    }


    [RelayCommand]
    private async Task Continue()
    {
        if (!ValidateFields()) 
            return;

        try
        {
            IsServerRequested = true;

            var tokens = await _authService.LoginAsync(new CredentialsDto() { Login = Login, Password = Password });

            await SecureStorage.Default.SetAsync("AccessToken", tokens.AccessToken);
            await SecureStorage.Default.SetAsync("RefreshToken", tokens.RefreshToken);

            var profile = await _userService.GetFirstProfileIdAsycn(Login);

            _profileManager.CurrentProfile = profile;   
            // Preferences.Set("Profile", JsonSerializer.Serialize(profile));

            IsServerRequested = false;

            _navigationService.Navigation.PopToNewRoot<MainTabbedViewModel>();
        }
        catch (Exception ex)
        {
            IsServerRequested = false;

            // Visualize error for user...
        }
    }

    [RelayCommand]
    private void ForgotPassword()
    {
        _navigationService.Navigation.Push<NewPasswordLoginViewModel>(new { Login });
    }


    partial void OnLoginChanged(string value)
    {
        LoginError = null;
    }

    partial void OnPasswordChanged(string value)
    {
        PasswordError = null;
    }


    private bool ValidateFields()
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

        //if (_userService.ValidateLoginAndPassword(Login, Password) is ValidationResult result && result != ValidationResult.Success)
        //{
        //    if (result.MemberNames.First() == nameof(Login))
        //    {
        //        LoginError = result.ErrorMessage!;
        //    }
        //    else if (result.MemberNames?.First() == nameof(Password))
        //    {
        //        PasswordError = result.ErrorMessage!;
        //    }

        //    return false;
        //}

        return true;
    }
}
