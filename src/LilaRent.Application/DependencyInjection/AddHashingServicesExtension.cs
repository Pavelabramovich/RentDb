using LilaRent.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LilaRent.Application.DependencyInjection;


public static class AddHashingServicesExtension
{
    public static IServiceCollection AddHashingServices(this IServiceCollection @this)
    {
        @this.AddTransient<CryptographyService>();
        @this.AddTransient<JwtTokenService>();

        return @this;
    }
}
