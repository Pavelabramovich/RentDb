using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.ViewModels;


namespace LilaRent.MobileApp.Services;


public abstract class BaseNavigationService : INavigationService 
{
    public INavigationList Navigation
    {
        get => CurrentNavigationViewModel.NavigationStack;
    }

    public INavigationStack ModalNavigation
    {
        get => CurrentNavigationViewModel.ModalStack;
    }

    public ITabbedNavigationService CurrentTabs
    {
        get
        {
            if (CurrentNavigationViewModel.NavigationStack.Current is TabbedViewModel tabbedViewModel)
            {
                return new TabbedNavigationService(tabbedViewModel);
            }
            else
            {
                throw new InvalidOperationException("Current view model is not tabbed view model.");
            }
        }
    }
    public IFlyoutNavigationService CurrentFlyout => throw new NotImplementedException();


    protected abstract NavigationViewModel CurrentNavigationViewModel { get; }
}


public class NavigationService : BaseNavigationService
{
    private readonly IApplicationService _applicationService;


    public NavigationService(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    protected override NavigationViewModel CurrentNavigationViewModel 
    {
        get
        {
            var mainViewModel = _applicationService.CurrentApplication.MainViewModel
                ?? throw new InvalidOperationException("View model not set.");

            if (mainViewModel is not NavigationViewModel navigationViewModel)
            {
                throw new InvalidOperationException("Current page is not NavigationPage.");
            }
            else
            {
                return navigationViewModel;
            }
        }
    }
}


public class TabbedNavigationService : BaseNavigationService, ITabbedNavigationService
{
    private readonly TabbedViewModel _tabbedViewModel;


    public TabbedNavigationService(TabbedViewModel tabbedViewModel)
    {
        _tabbedViewModel = tabbedViewModel;
    }


    public IReadOnlyList<IViewModel> Tabs
    {
        get => _tabbedViewModel.Tabs;
    }

    public IViewModel SwitchToTabByIndex(Index index)
    {
        var viewModelToSwitch = _tabbedViewModel.Tabs[index];

        _tabbedViewModel.CurrentTab = viewModelToSwitch;

        return viewModelToSwitch;
    }

    protected override NavigationViewModel CurrentNavigationViewModel
    {
        get => _tabbedViewModel.CurrentTab as NavigationViewModel
            ?? throw new InvalidOperationException("Tabs must be navigation view model.");
    }
}
