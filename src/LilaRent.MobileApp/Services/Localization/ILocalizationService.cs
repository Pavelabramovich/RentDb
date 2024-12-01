using System.Globalization;


namespace LilaRent.MobileApp.Services;


public interface ILocalizationService 
{
    public object this[string key] { get; }

    public CultureInfo CurrentCulture { get; set; }
}
