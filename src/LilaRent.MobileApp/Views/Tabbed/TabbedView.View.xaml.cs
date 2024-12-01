using LilaRent.MobileApp.Components;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.ViewModels;


namespace LilaRent.MobileApp.Views;


public partial class TabbedViewView : TabbedViewPage
{
    public TabbedViewView(IServiceProvider serviceProvider)
    {
        Resources.MergedDictionaries.Add(new() 
        { 
            ["TabTemplate"] = new TabTemplateSelector(serviceProvider) 
        });

        InitializeComponent();
    }


    private class TabTemplateSelector : DataTemplateSelector
    { 
        private readonly IServiceProvider _serviceProvider;
        private readonly IVVmResolveService _vVmResolveService;


        public TabTemplateSelector(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _vVmResolveService = _serviceProvider.GetRequiredService<IVVmResolveService>();
        }

        protected override DataTemplate OnSelectTemplate(object viewModel, BindableObject container)
        {
            var view = GetView(viewModel.GetType());
            return new DataTemplate(() => view);
        }

        private IView GetView(Type viewModelType)
        {
            Type viewType = _vVmResolveService.GetViewType(viewModelType);
            var view = (IView)_serviceProvider.GetRequiredService(viewType);

            return view;
        }
    }
}