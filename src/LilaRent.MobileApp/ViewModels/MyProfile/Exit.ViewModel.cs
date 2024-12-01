using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;


namespace LilaRent.MobileApp.ViewModels;


public partial class ExitViewModel : IViewModel
{
    private readonly INavigationService _navigationService;


    public ExitViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }


    [RelayCommand]
    public void ToBack()
    {
        _navigationService.CurrentTabs.Navigation.Pop();
    }


    [RelayCommand]
    public void ToExit()
    {
        _navigationService.Navigation.PopToNewRoot<LandingViewModel>();
    }
}
