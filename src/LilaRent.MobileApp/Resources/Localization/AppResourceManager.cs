using System.Resources;


namespace LilaRent.MobileApp.Resources.Localization;


public static class AppResourceManager
{
    private const string LocalizationResourcesBasePath = "LilaRent.MobileApp.Resources.Localization";

    private static readonly Lazy<IEnumerable<ResourceManager>> _resourceManagersLazy = new(() =>
    [
        new ResourceManager($"{LocalizationResourcesBasePath}.Views.Views", typeof(ThisAssemblyMarker).Assembly),
        new ResourceManager($"{LocalizationResourcesBasePath}.ErrorMessages.ErrorMessages", typeof(ThisAssemblyMarker).Assembly)
    ]);
    
    private static IEnumerable<ResourceManager> ResourceManagers => _resourceManagersLazy.Value;


    public static object? GetObject(string key)
    {
        foreach (var resourceManager in ResourceManagers)
        {
            var value = resourceManager.GetObject(key);

            if (value is not null)
                return value;
        }

        return null;
    }
}
