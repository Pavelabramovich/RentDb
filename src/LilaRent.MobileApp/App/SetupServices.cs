using CommunityToolkit.Maui;
using LilaRent.Application.Validation;
using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Services;
using LilaRent.Requests.Services;


namespace LilaRent.MobileApp;


public static class SetupServicesExtension
{
    public static MauiAppBuilder SetupServices(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<IFakeAnnouncementsService, AnnouncementListService>();
        builder.Services.AddSingleton<IOrdersService, OrdersListService>();
        builder.Services.AddTransient<ICategoryService, CategoryService>();

        builder.Services.AddSingleton<ILocalizationService, LocalizationService>(serviceProvider => LocalizationService.Instance);
        builder.Services.AddSingleton<ILocalizationManager, LocalizationManager>(serviceProvider => LocalizationManager.Instance);

        builder.Services.AddSingleton<IApplicationService, ApplicationService>();

        builder.Services.AddSingleton<INavigationService, NavigationService>();

		builder.Services.AddTransient<IProfileService, ProfileListService>();
        builder.Services.AddSingleton<IProfileManager, ProfileManager>(serviceProvider => ProfileManager.Instance);

        builder.Services.AddTransient<IFakeUserService, UserListService>();
        builder.Services.AddSingleton<IEmailValidationService, EmailValidationService>();

		builder.Services.AddTransient<IAppointmentService, AppointmentListService>();

		builder.Services.AddTransient<IRangesService, RangesSimpleService>();



        builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthService, AuthService>();

        builder.Services.AddHttpClient();

        return builder;
    }
}
