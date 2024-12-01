using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Entities;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Extensions;

namespace LilaRent.MobileApp.ViewModels
{
	[QueryProperty(nameof(OrderIdParameter), "OrderId")]
	public partial class ConfirmationDoneViewModel: ObservableObject
    {
		private readonly INavigationService _navigationService;
		private readonly IOrdersService _ordersService;

		public long OrderIdParameter
		{
			set
			{
				Order = _ordersService.GetByIdAsync(value).Result;
			}
		}

		[ObservableProperty]
		OrderBasic _order;

		public ConfirmationDoneViewModel(INavigationService navigationService, IOrdersService ordersService)
		{
			_navigationService = navigationService;
			_ordersService = ordersService;
		}

		[RelayCommand]
		void CancelOrder()
		{
			_navigationService.CurrentTabs.Navigation.Push<ConfirmationCancelViewModel>(new {OrderId = Order.Id});
		}	
	}
}
