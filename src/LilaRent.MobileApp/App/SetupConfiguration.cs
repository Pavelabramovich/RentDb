using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;


namespace LilaRent.MobileApp;


public static class SetupConfigurationExtension
{
    public static MauiAppBuilder SetupConfiguration(this MauiAppBuilder builder)
    {
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit(options =>
            {
                options.SetShouldSuppressExceptionsInConverters(true);
                options.SetShouldSuppressExceptionsInBehaviors(true);
                options.SetShouldSuppressExceptionsInAnimations(true);
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                
                fonts.AddFont("Consolas.ttf", "Consolas");

                fonts.AddFont("Inter-Bold.ttf", "InterBold");
                fonts.AddFont("Inter-Medium.ttf", "InterMedium");
                fonts.AddFont("Inter-Regular.ttf", "InterRegular");
                fonts.AddFont("Inter-Light.ttf", "InterLight");

                fonts.AddFont("Montserrat-Regular.ttf", "Montserrat");
                fonts.AddFont("Montserrat-Bold.ttf", "MontserratBold");
                fonts.AddFont("Montserrat-SemiBold.ttf", "MontserratSemiBold");
            });

        #if DEBUG
		    builder.Logging.AddDebug();
        #endif

        return builder;
    }
}