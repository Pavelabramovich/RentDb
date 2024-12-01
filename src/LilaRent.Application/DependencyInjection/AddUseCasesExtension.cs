using LilaRent.Application.UseCases.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace LilaRent.Application.DependencyInjection;


public static class AddUseCasesExtension
{
    public static IServiceCollection AddUseCases(this IServiceCollection @this)
    {
        @this.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ThisAssemblyMarker).Assembly));


        @this.AddTransient<IPipelineBehavior<CreateAnnouncementCommand, Unit>, CreateAnnouncementValidation>();
        @this.AddTransient<IPipelineBehavior<CreateUserWithProfileCommand, Unit>, CreateUserWithProfileValidation>();

        return @this;
    }
}
