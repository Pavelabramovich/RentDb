using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilaRent.MobileApp.ViewModels
{
	public class HistoryNavigationViewModel : NavigationViewModel
	{
		public HistoryNavigationViewModel(IServiceProvider serviceProvider)
			: base(serviceProvider, typeof(HistoryAnnouncementsViewModel)) 
		{ }
	}
}
