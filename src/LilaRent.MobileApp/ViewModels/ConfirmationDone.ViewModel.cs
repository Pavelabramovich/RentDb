using CommunityToolkit.Mvvm.ComponentModel;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Entities;
using CommunityToolkit.Mvvm.Input;
using LilaRent.Application.Dto;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Reservation), nameof(Reservation))]
[QueryProperty(nameof(Announcement), nameof(Announcement))]
public partial class ConfirmationDoneViewModel: ObservableObject
{
	private readonly INavigationService _navigationService;
	private readonly IOrdersService _ordersService;

	//public long OrderIdParameter
	//{
	//	set
	//	{
	//		Order = _ordersService.GetByIdAsync(value).Result;
	//	}
	//}

	[ObservableProperty]
	private ReservationCreatingDto _reservation;

	[ObservableProperty]
	private AnnouncementDetailsDto _announcement;



	public ConfirmationDoneViewModel(INavigationService navigationService, IOrdersService ordersService)
	{
		_navigationService = navigationService;
		_ordersService = ordersService;
	}

	[RelayCommand]
	void CancelOrder()
	{
		_navigationService.CurrentTabs.Navigation.PopToRoot();
		//_navigationService.CurrentTabs.Navigation.Push<ConfirmationCancelViewModel>(new {OrderId = Order.Id});
	}
}
