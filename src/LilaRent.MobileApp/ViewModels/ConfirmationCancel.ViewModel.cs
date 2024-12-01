using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Entities;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Extensions;

namespace LilaRent.MobileApp.ViewModels;

[QueryProperty(nameof(OrderIdParameter), "OrderId")]
public partial class ConfirmationCancelViewModel : ObservableObject
{
	INavigationService _navigationService;
	IOrdersService _ordersService;

	[ObservableProperty]
	OrderBasic _order;

	public long OrderIdParameter
	{
		set
		{
			Order = _ordersService.GetByIdAsync(value).Result;
		}
	}

	public ConfirmationCancelViewModel(INavigationService navigationService, IOrdersService ordersService)
	{
		_navigationService = navigationService;
		_ordersService = ordersService;
	}

	[RelayCommand]
	void Cancel()
	{
		_navigationService.CurrentTabs.Navigation.Push<ConfirmationCanceledViewModel>();
	}

	[RelayCommand]
	void Leave()
	{
		_navigationService.CurrentTabs.Navigation.Pop();
	}
}
