using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.Domain.Fields;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;


namespace LilaRent.MobileApp.ViewModels;


public partial class NewAnnouncementPromotionViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;


    [ObservableProperty]
    private PromotionType _promotionType;


    public NewAnnouncementPromotionViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        PromotionType = PromotionType.OftenLifting;
    }


    [RelayCommand]
    private void Next()
    {
        _navigationService.CurrentTabs.Navigation.Push<NewAnnouncementCompletedViewModel>();
    }
}