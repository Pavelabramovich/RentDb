using LilaRent.MobileApp.Core;


namespace LilaRent.MobileApp.Services;


public interface INavigationService 
{
   // IStackNavigation<object> Navigation { get; }
   // IStackNavigation<object> ModalNavigation { get; }

    INavigationList Navigation { get; }
    INavigationStack ModalNavigation { get; }

    ITabbedNavigationService CurrentTabs { get; }
    IFlyoutNavigationService CurrentFlyout { get; }
}

public interface ITabbedNavigationService : INavigationService
{
    IReadOnlyList<IViewModel> Tabs { get; }

    IViewModel SwitchToTabByIndex(Index index);
}

public interface IFlyoutNavigationService 
{ 
    // Not implemented
}
