using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Services;
using System.Collections.ObjectModel;
using LilaRent.MobileApp.Extensions;


namespace LilaRent.MobileApp.ViewModels;


public partial class MyOrdersViewModel : ObservableObject
{
	private readonly IOrdersService _ordersService;
	private readonly INavigationService _navigationService;

    public MyOrdersViewModel(IOrdersService ordersService, INavigationService navigationService) 
	{ 
		_ordersService = ordersService;
		_navigationService = navigationService;
		OrganizedOrders = _ordersService.GetOrganizedAsync().Result;
		Future = OrganizedOrders.Where(kv => kv.Key == "Future").First()	;

    }

	[ObservableProperty]
	KeyValuePair<string, IEnumerable<OrderBasic>> _future;

	[ObservableProperty]
	IDictionary<string, IEnumerable<OrderBasic>> _organizedOrders;
}
