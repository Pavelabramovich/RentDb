using CommunityToolkit.Mvvm.ComponentModel;
using LilaRent.MobileApp.Services;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Extensions;


namespace LilaRent.MobileApp.ViewModels;


public partial class ConfirmationCanceledViewModel: ObservableObject
{
	private readonly INavigationService _navigationService;


	public ConfirmationCanceledViewModel(INavigationService navigationService)
	{
		_navigationService = navigationService;
	}


	[RelayCommand]
	void Ok()
	{
		_navigationService.CurrentTabs.Navigation.PopTo<AnnouncementViewModel>();
	}
}
