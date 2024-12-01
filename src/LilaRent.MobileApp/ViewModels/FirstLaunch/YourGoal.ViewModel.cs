using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Core.Builders;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;


namespace LilaRent.MobileApp.ViewModels;


public partial class YourGoalViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;


    public YourGoalViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }


    [RelayCommand]
    private void Continue()
    {
        var builder = new UserWithProfileBuilder();

        _navigationService.Navigation.Push<RegistrationCredentialsViewModel>(new { Builder = builder });
    }
}
