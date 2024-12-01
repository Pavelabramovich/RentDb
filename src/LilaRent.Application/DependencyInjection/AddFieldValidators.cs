using LilaRent.Application.Validation;
using Microsoft.Extensions.DependencyInjection;


namespace LilaRent.Application;


public static class AddFieldValidatorsExtension
{
    public static IServiceCollection AddFieldValidators(this IServiceCollection @this)
    {
        @this.AddSingleton<AddressValidator>();
        @this.AddSingleton<AreaValidator>();
        @this.AddSingleton<DaysCountOpenAheadValidator>();
        @this.AddSingleton<EmailValidator>();
        @this.AddSingleton<FloorValidator>();
        @this.AddSingleton<NameValidator>();
        @this.AddSingleton<PasswordValidator>();
        @this.AddSingleton<PercentageValidator>();
        @this.AddSingleton<PhoneValidator>();
        @this.AddSingleton<PriceValidator>();
        @this.AddSingleton<StationValidator>();
        @this.AddSingleton<VerificationCodeValidator>();

        return @this;
    }
}
