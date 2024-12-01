using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Core.Builders;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;


namespace LilaRent.MobileApp.ViewModels;


public partial class LandingViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;


    public LandingViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }


    [RelayCommand]
    private void Enter()
    {
        _navigationService.Navigation.Push<LoginViewModel>();
    }

    [RelayCommand]
    private void Registration()
    {
        var isFirstLaunch = Preferences.Get("IsFirstLaunch", defaultValue: true);

        if (isFirstLaunch)
        {
            Preferences.Set("IsFirstLaunch", false);
            _navigationService.Navigation.Push<WelcomeViewModel>();
        }
        else
        {
            var builder = new UserWithProfileBuilder();

            _navigationService.Navigation.Push<RegistrationCredentialsViewModel>(new { Builder = builder });
        }
    }

    [RelayCommand]
    private void ContinueWithoutEnter()
    {
        _navigationService.Navigation.PopToNewRoot<MainTabbedViewModel>();
    }
}
