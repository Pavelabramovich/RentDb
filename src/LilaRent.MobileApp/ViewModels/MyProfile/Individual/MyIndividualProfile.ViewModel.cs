using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;



namespace LilaRent.MobileApp.ViewModels;


public partial class MyIndividualProfileViewModel : BaseMyProfileViewModel
{
    public MyIndividualProfileViewModel(INavigationService navigationService, IProfileManager profileService)
        : base(navigationService, profileService)
    { }


    [RelayCommand]
    private void ToFavorites()
    {
        _navigationService.CurrentTabs.Navigation.Push<FavoritesTabbedViewModel>();
    }
}