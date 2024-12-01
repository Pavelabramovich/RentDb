using Microsoft.Extensions.Configuration;


namespace LilaRent.Application.DependencyInjection;


public static class AddJwtConfigurationExtension
{
    public static IConfigurationBuilder AddJwtConfiguration(this IConfigurationBuilder @this)
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        @this.SetBasePath(baseDirectory).AddJsonFile("jwtSettings.json", optional: false, reloadOnChange: true);

        return @this;
    }
}
