using CommunityToolkit.Mvvm.ComponentModel;


namespace LilaRent.MobileApp.ViewModels;


public partial class FavoritesTabbedViewModel : TabbedViewModel
{
    public FavoritesTabbedViewModel(IServiceProvider serviceProvider)
    {
        Type[] tabTypes = [typeof(AnnouncementsViewModel), typeof(OwnersViewModel)];

        foreach (var tabType in tabTypes)
        {
            var viewModel = (IViewModel)serviceProvider.GetRequiredService(tabType);
            Tabs.Add(viewModel);
        }

        CurrentTab = Tabs[0];
    }
}
