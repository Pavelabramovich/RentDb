using CommunityToolkit.Mvvm.ComponentModel;
using LilaRent.MobileApp.Core;


namespace LilaRent.MobileApp.ViewModels;


public partial class NavigationViewModel : IViewModel
{
    [ObservableProperty]
    private string? _iconImageSource;

    public ObservableNavigationList NavigationStack { get; }
    public ObservableNavigationStack ModalStack { get; }


    public NavigationViewModel(IServiceProvider serviceProvider, Type rootViewModelType, string? iconImageSource = null)
        : this(serviceProvider, iconImageSource)
    {
        NavigationStack.Push(rootViewModelType);
    }

    public NavigationViewModel(IServiceProvider serviceProvider, string? iconImageSource = null)
    {
        NavigationStack = new(serviceProvider);
        ModalStack = new(serviceProvider);

        IconImageSource = iconImageSource;
    }
}