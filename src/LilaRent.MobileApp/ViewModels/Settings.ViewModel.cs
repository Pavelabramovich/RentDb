using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Services;
using System.Globalization;


namespace LilaRent.MobileApp.ViewModels;


public partial class SettingsViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly ILocalizationManager _localizationManager;


    public SettingsViewModel(INavigationService navigationService, ILocalizationManager localizationManager)
    {
        _navigationService = navigationService;
        _localizationManager = localizationManager;
    }

    private int counter = 0;

    [RelayCommand]
    private void ChangeLanguage()
    {
        _localizationManager.CurrentCulture = new CultureInfo(++counter % 2 != 0 ? "en-US" : "RU-ru");
    }
}