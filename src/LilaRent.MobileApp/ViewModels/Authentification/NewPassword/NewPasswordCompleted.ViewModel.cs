using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Login), "Login")]
public partial class NewPasswordCompletedViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private string _login;


    public NewPasswordCompletedViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _navigationService = _serviceProvider.GetRequiredService<INavigationService>();

        Login = string.Empty;
    }


    [RelayCommand]
    private void Continue()
    {
        _navigationService.Navigation.PopToNewRoot<MainTabbedViewModel>();
    }
}