using LilaRent.Application;
using Microsoft.Extensions.Configuration;
using System.Reflection;


namespace LilaRent.MobileApp;


public static class MauiProgram
{
	public static MauiApp CreateMauiApp() 
	{
        var builder = MauiApp.CreateBuilder();

        var assembly = Assembly.GetExecutingAssembly();

        // appsettings.json is located directly under MyNamespace.csproj
        using var stream = assembly.GetManifestResourceStream("LilaRent.MobileApp.appsettings.json");
        stream.Position = 0;

        var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
        builder.Configuration.AddConfiguration(config);



        //builder.Configuration
        //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        builder
            .SetupConfiguration()
            .SetupHandlers()
            .SetupServices()
            .SetupViewViewModels();

        builder.Services.AddFieldValidators();

        return builder.Build();
	}
}
