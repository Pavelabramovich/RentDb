using LilaRent.MobileApp.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.MobileApp.ViewModels;


public class MainTabbedViewModel : TabbedViewModel
{
    public MainTabbedViewModel(IServiceProvider serviceProvider, int currentTabIndex = 0, params Type[] tabTypes)
        : base()
    {
        foreach (var tabType in tabTypes)
        {
            var viewModel = (IViewModel)serviceProvider.GetRequiredService(tabType);
            Tabs.Add(viewModel);
        }

        CurrentTab = Tabs[currentTabIndex];
    }
}
