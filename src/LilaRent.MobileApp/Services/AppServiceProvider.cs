
namespace LilaRent.MobileApp.Services;


public static class AppServiceProvider
{
    private static readonly Lazy<IServiceProvider> _lazy = new(() =>
    {
        #if ANDROID || IOS || MACCATALYST
        {
            return IPlatformApplication.Current?.Services
                ?? throw new InvalidOperationException("Can't resolve current service provider.");
        }
        #elif WINDOWS10_0_17763_0_OR_GREATER
        {
            return MauiWinUIApplication.Current.Services;
        }
        #else
        {
            throw new InvalidOperationException("Can't resolve current service provider.");
        }
        #endif 
    });

    public static IServiceProvider Instance => _lazy.Value;
}
