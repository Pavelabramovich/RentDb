using LilaRent.MobileApp.Core;


namespace LilaRent.MobileApp.Extensions;


public static class NavigationListExtension
{
    public static void PopToRoot(this INavigationList @this, object routeParameters)
    {
        @this.PopToRoot(routeParameters.PropertiesToDictionary());
    }


    public static void PopToNewRoot<TViewModel>(this INavigationList @this, IDictionary<string, object?>? routeParameters = null) where TViewModel : IViewModel
    {
        @this.PopToNewRoot(typeof(TViewModel), routeParameters);
    }

    public static void PopToNewRoot(this INavigationList @this, Type viewModelType, object routeParameters)
    {
        @this.PopToNewRoot(viewModelType, routeParameters.PropertiesToDictionary());
    }

    public static void PopToNewRoot<TViewModel>(this INavigationList @this, object routeParameters) where TViewModel : IViewModel
    {
        @this.PopToNewRoot<TViewModel>(routeParameters.PropertiesToDictionary());
    }


    public static void PopOverRoot(this INavigationList @this, Type viewModelType, object routeParameters)
    {
        ArgumentNullException
            .ThrowIfNull(routeParameters, nameof(routeParameters));

        @this.PopOverRoot(viewModelType, routeParameters.PropertiesToDictionary());
    }

    public static void PopOverRoot<TViewModel>(this INavigationList @this, IDictionary<string, object?>? routeParameters = null)
        where TViewModel : IViewModel
    {
        @this.PopOverRoot(typeof(TViewModel), routeParameters);
    }

    public static void PopOverRoot<TViewModel>(this INavigationList @this, object routeParameters)
        where TViewModel : IViewModel
    {
        @this.PopOverRoot(typeof(TViewModel), routeParameters.PropertiesToDictionary());
    }


    public static void PopTo(this INavigationList @this, Type viewModelType, object routeParameters)
    {
        ArgumentNullException
            .ThrowIfNull(routeParameters, nameof(routeParameters));

        @this.PopTo(viewModelType, routeParameters.PropertiesToDictionary());
    }

    public static void PopTo<TViewModel>(this INavigationList @this, IDictionary<string, object?>? routeParameters = null) 
        where TViewModel : IViewModel
    {
        @this.PopTo(typeof(TViewModel), routeParameters);
    }

    public static void PopTo<TViewModel>(this INavigationList @this, object routeParameters)
        where TViewModel : IViewModel
    {
        @this.PopTo<TViewModel>(routeParameters.PropertiesToDictionary());
    }


    public static TViewModel InsertBefore<TViewModel, TViewModelBefore>(this INavigationList @this, IDictionary<string, object?>? routeParameters = null)
        where TViewModel : IViewModel where TViewModelBefore : IViewModel
    {
        return (TViewModel)@this.InsertBefore(typeof(TViewModel), typeof(TViewModelBefore), routeParameters);
    }

    public static IViewModel InsertBefore(this INavigationList @this, Type viewModelType, Type viewModelTypeBefore, object routeParameters)
    {
        return @this.InsertBefore(viewModelType, viewModelTypeBefore, routeParameters.PropertiesToDictionary());
    }

    public static TViewModel InsertBefore<TViewModel, TViewModelBefore>(this INavigationList @this, object routeParameters)
        where TViewModel : IViewModel where TViewModelBefore : IViewModel
    {
        return (TViewModel)@this.InsertBefore(typeof(TViewModel), typeof(TViewModelBefore), routeParameters);
    }


    public static void Remove<TViewModel>(this INavigationList @this) where TViewModel : IViewModel
    {
        @this.Remove(typeof(TViewModel));
    }
}
