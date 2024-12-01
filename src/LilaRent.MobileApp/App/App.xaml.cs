using LilaRent.MobileApp.Views;
using LilaRent.MobileApp.ViewModels;

//using Application = Microsoft.Maui.Controls.Application;
using LilaRent.MobileApp.Extensions;


namespace LilaRent.MobileApp;


public partial class App : MauiControlsApplication
{
    private readonly IServiceProvider _serviceProvider;


    private object? _currentViewModel;


    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _currentViewModel = null;
        _serviceProvider = serviceProvider;

        var navigationView = _serviceProvider.GetRequiredService<MainNavigationView>();

        var navigationViewModel = _serviceProvider.GetRequiredService<NavigationViewModel>();
        navigationViewModel.NavigationStack.Push<LandingViewModel>();
        
        MainViewModel = navigationViewModel;
        MainView = navigationView;
    }


    public Page? MainView 
    {
        get => MainPage;
        set
        {
            MainPage = value;
            
            if (MainPage is not null)
                MainPage.BindingContext = MainViewModel;
        }
    }

    public object? MainViewModel 
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;

            if (MainView is not null)
                MainView.BindingContext = _currentViewModel;
        }
    }
}
