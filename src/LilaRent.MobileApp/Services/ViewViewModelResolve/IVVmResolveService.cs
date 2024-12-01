
namespace LilaRent.MobileApp.Services;


public interface IVVmResolveService
{
    void Add(Type viewType, Type viewModelType);

    Type GetViewType(Type viewModelType);
    Type GetViewModelType(Type viewType);

    IView GetView(Type viewModelType);
    IViewModel GetViewModel(Type viewType);
}


public static class VVmResolveServiceExtension
{
    public static Type GetViewType<TViewModel>(this IVVmResolveService @this) where TViewModel : IViewModel
    {
        return @this.GetViewType(typeof(TViewModel));
    }

    public static Type GetViewModelType<TView>(this IVVmResolveService @this) where TView : IView
    {
        return @this.GetViewModelType(typeof(TView));
    }


    public static IView GetView<TViewModel>(this IVVmResolveService @this) where TViewModel : IViewModel
    {
        return @this.GetView(typeof(TViewModel));
    }

    public static TView GetView<TviewModel, TView>(this IVVmResolveService @this) where TviewModel : IViewModel where TView : IView
    {
        return (TView)@this.GetView(typeof(TviewModel));
    }
    

    public static IViewModel GetViewModel<TView>(this IVVmResolveService @this) where TView : IView
    {
        return @this.GetViewModel(typeof(TView));
    }
}
