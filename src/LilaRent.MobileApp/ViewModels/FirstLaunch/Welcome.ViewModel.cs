using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;


namespace LilaRent.MobileApp.ViewModels;


public partial class WelcomeViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;


    public WelcomeViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }


    [RelayCommand]
    private void Continue()
    {
        _navigationService.Navigation.Push<YourGoalViewModel>();
    }
}
