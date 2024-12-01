using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.MobileApp.ViewModels;


public partial class RemovingAnnouncementCompletedViewModel : IViewModel
{
    private INavigationService _navigationService;


    public RemovingAnnouncementCompletedViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }


    [RelayCommand]
    private void Ok()
    {
        _navigationService.CurrentTabs.Navigation.PopTo<MyLegalPersonProfileViewModel>();
    }
}
