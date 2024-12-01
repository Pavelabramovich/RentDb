using FluentValidation;
using LilaRent.Application.Dto;
using LilaRent.Application.Validation;
using Microsoft.Extensions.DependencyInjection;


namespace LilaRent.Application;


public static class AddDtoValidatorsExtension
{
    public static IServiceCollection AddDtoValidators(this IServiceCollection @this)
    {
        @this.AddTransient<IValidator<AnnouncementCreatingDto>, AnnouncementCreatingDtoValidator>();


        @this.AddTransient<IValidator<CredentialsDto>, AuthenticationDtoValidator>(); 
        

        @this.AddTransient<IValidator<UserWithProfileCreatingDto>, UserWithProfileCreatingDtoValidator>();
        @this.AddTransient<IValidator<ProfileCreatingDto>, ProfileCreatingDtoValidator>();

        return @this;
    }
}
