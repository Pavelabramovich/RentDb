using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Name), "Name")]
public partial class RegistrationCompletedViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;


    [ObservableProperty]
    private string _name;


    public RegistrationCompletedViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        Name = string.Empty;
    }


    [RelayCommand]
    private void Continue()
    {
        _navigationService.Navigation.PopToNewRoot<MainTabbedViewModel>(); 
    }
}