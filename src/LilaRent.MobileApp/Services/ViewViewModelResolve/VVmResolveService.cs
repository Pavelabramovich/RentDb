using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.ViewModels;
using LilaRent.MobileApp.Views;


namespace LilaRent.MobileApp.Services;


public class VVmResolveService : IVVmResolveService
{
    private readonly IServiceProvider _serviceProvider;

    private readonly BijectiveDictionary<Type, Type> _vVmTypesPairs;


    public VVmResolveService(IServiceProvider serviceProvider, params VVmTypePair[] vVmTypesPairs)
    {
        ArgumentNullException
            .ThrowIfNull(serviceProvider);

        ArgumentNullException
            .ThrowIfNull(vVmTypesPairs);


        _serviceProvider = serviceProvider;

        foreach (var (viewType, viewModelType) in vVmTypesPairs)
        {
            if (viewType is null)
                throw new ArgumentException("View types must be not null.", nameof(vVmTypesPairs));

            if (!viewType.IsSameOrSubclassOf<IView>())
                throw new ArgumentException($"View types must {nameof(IView)} based.", nameof(vVmTypesPairs));

            if (viewModelType is null)
                throw new ArgumentException("View model must be not null", nameof(vVmTypesPairs));

            if (!viewModelType.IsSameOrSubclassOf<IViewModel>())
                throw new ArgumentException($"View model types must {nameof(IViewModel)} based.", nameof(vVmTypesPairs));
        }

        _vVmTypesPairs = new BijectiveDictionary<Type, Type>(vVmTypesPairs.Select(vVm => Key1Key2Pair.Create(vVm.ViewType, vVm.ViewModelType)));
    }

    public void Add(Type viewType, Type viewModelType)
    {
        _vVmTypesPairs.Add(viewType, viewModelType);
    }


    public Type GetViewType(Type viewModelType)
    {
        return _vVmTypesPairs.Backward[viewModelType];
    }

    public Type GetViewModelType(Type viewType)
    {
        return _vVmTypesPairs.Forward[viewType];
    }


    public IView GetView(Type viewModelType)
    {
        var viewType = GetViewType(viewModelType);
        return (IView)_serviceProvider.GetRequiredService(viewType);   
    }

    public IViewModel GetViewModel(Type viewType)
    {
        var viewModelType = GetViewModelType(viewType);
        return (IViewModel)_serviceProvider.GetRequiredService(viewModelType);
    }
}
