using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Core.Builders;


namespace LilaRent.MobileApp.ViewModels;


public enum RegistrationGoal
{
    RentPremises = 0,
    RentOutPremises = 1
}


[QueryProperty(nameof(Builder), nameof(Builder))]
public partial class RegistrationGoalViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public UserWithProfileBuilder? Builder { get; set; }


    [ObservableProperty]
    private RegistrationGoal _registrationGoal;


    public RegistrationGoalViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        RegistrationGoal = RegistrationGoal.RentOutPremises;
    }


    [RelayCommand]
    private void Continue()
    {
        if (Builder is null)
            throw new InvalidOperationException("Builder is not set.");

        Builder.Goal = RegistrationGoal;

        _navigationService.Navigation.Push<RegistrationContactsViewModel>(new { Builder });
    }
}
