using Microsoft.Extensions.DependencyInjection;


namespace LilaRent.Application.DependencyInjection;


public static class AddDtoMapperExtension
{
    public static IServiceCollection AddDtoMapper(this IServiceCollection @this)
    {
        return @this.AddAutoMapper(typeof(ThisAssemblyMarker).Assembly);
    }
}
