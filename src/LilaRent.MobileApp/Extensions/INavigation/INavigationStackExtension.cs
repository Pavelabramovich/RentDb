using LilaRent.MobileApp.Core;


namespace LilaRent.MobileApp.Extensions;


public static class NavigationStackExtension
{
    public static TViewModel Push<TViewModel>(this INavigationStack @this, IDictionary<string, object?>? routeParameters = null) where TViewModel : IViewModel
    {
        return (TViewModel)@this.Push(typeof(TViewModel), routeParameters);
    }

    public static IViewModel Push(this INavigationStack @this, Type viewModelType, object routeParameters)
    {
        return @this.Push(viewModelType, routeParameters.PropertiesToDictionary());
    }

    public static TViewModel Push<TViewModel>(this INavigationStack @this, object routeParameters) where TViewModel : IViewModel
    {
        return @this.Push<TViewModel>(routeParameters.PropertiesToDictionary());
    }


    public static IViewModel Pop(this INavigationStack @this, object routeParameters)
    {
        return @this.Pop(routeParameters.PropertiesToDictionary());
    }
}
