using LilaRent.MobileApp.Resources.Localization;
using System.Globalization;


namespace LilaRent.MobileApp.Services;


public class LocalizationService : ILocalizationService
{
    private static readonly Lazy<LocalizationService> _lazy = new(() => new LocalizationService());
    public static LocalizationService Instance => _lazy.Value;

    private LocalizationService()
    {
        CurrentCulture = CultureInfo.CurrentCulture;
    }

    public object this[string key]
    {
        get => AppResourceManager.GetObject(key) ?? key;
    }

    public CultureInfo CurrentCulture
    {
        get => CultureInfo.CurrentUICulture;
        set
        {
            CultureInfo.CurrentUICulture = value;
            CultureInfo.CurrentCulture = value;
        }
    }
}
