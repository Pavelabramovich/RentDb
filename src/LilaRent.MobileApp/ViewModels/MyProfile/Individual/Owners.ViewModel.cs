using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.Input;


namespace LilaRent.MobileApp.ViewModels;


public partial class OwnersViewModel : ObservableObject
{
    private readonly IFakeUserService _userService;
    private readonly INavigationService _navigationService;


    [ObservableProperty]
    private string _title = "Owners";


    public ObservableCollection<Profile> Profiles { get; }


    public OwnersViewModel(IFakeUserService userService, INavigationService navigationService)
    {
        _userService = userService;
        _navigationService = navigationService;

        Profiles = _userService.GetUsers().Select(u => u.Profile).ToObservableCollection();
    }

    [RelayCommand]
    public void ToProfile(Profile? profile)
    {
        ArgumentNullException
            .ThrowIfNull(profile, nameof(profile));

        _navigationService.CurrentTabs.Navigation.Push<ProfileViewModel>(new { profile.Id });
    }
}
