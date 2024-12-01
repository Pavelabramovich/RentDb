using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.MobileApp.ViewModels;


public class CatalogNavigationViewModel : NavigationViewModel
{
    public CatalogNavigationViewModel(IServiceProvider serviceProvider)
        : base(serviceProvider, typeof(CatalogViewModel))
    { }
}
