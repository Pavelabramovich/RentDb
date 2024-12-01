using LilaRent.MobileApp.Views;
using LilaRent.MobileApp.ViewModels;
using LilaRent.MobileApp.Extensions;

using Vs = LilaRent.MobileApp.Extensions.ViewServiceFactory;
using Vms = LilaRent.MobileApp.Extensions.ViewModelServiceFactory;


namespace LilaRent.MobileApp;


public static class SetupViewViewModelsExtension
{
    public static MauiAppBuilder SetupViewViewModels(this MauiAppBuilder builder)
    {
        builder.Services
            .AddWithVVmResolving(Vs.Singleton<LandingView>(), Vms.Singleton<LandingViewModel>())

            .AddWithVVmResolving(Vs.Singleton<LoginView>(), Vms.Singleton<LoginViewModel>())

            .AddWithVVmResolving(Vs.Singleton<NewPasswordLoginView>(), Vms.Singleton<NewPasswordLoginViewModel>())
            .AddWithVVmResolving(Vs.Singleton<NewPasswordCodeView>(), Vms.Singleton<NewPasswordCodeViewModel>())
            .AddWithVVmResolving(Vs.Singleton<NewPasswordEnteringView>(), Vms.Singleton<NewPasswordEnteringViewModel>())
            .AddWithVVmResolving(Vs.Singleton<NewPasswordCompletedView>(), Vms.Singleton<NewPasswordCompletedViewModel>())

            .AddWithVVmResolving(Vs.Singleton<WelcomeView>(), Vms.Singleton<WelcomeViewModel>())
            .AddWithVVmResolving(Vs.Singleton<YourGoalView>(), Vms.Singleton<YourGoalViewModel>())
            .AddWithVVmResolving(Vs.Singleton<RegistrationCredentialsView>(), Vms.Singleton<RegistrationCredentialsViewModel>())
            .AddWithVVmResolving(Vs.Singleton<RegistrationCodeView>(), Vms.Singleton<RegistrationCodeViewModel>())
            .AddWithVVmResolving(Vs.Singleton<RegistrationGoalView>(), Vms.Singleton<RegistrationGoalViewModel>())
            .AddWithVVmResolving(Vs.Singleton<RegistrationContactsView>(), Vms.Singleton<RegistrationContactsViewModel>())
            .AddWithVVmResolving(Vs.Singleton<RegistrationAboutMyselfView>(), Vms.Singleton<RegistrationAboutMyselfViewModel>())
            .AddWithVVmResolving(Vs.Singleton<RegistrationCompletedView>(), Vms.Singleton<RegistrationCompletedViewModel>())

            .AddWithVVmResolving(Vs.Singleton<NewAnnouncementTargetView>(), Vms.Singleton<NewAnnouncementTargetViewModel>())
            .AddWithVVmResolving(Vs.Singleton<NewAnnouncementFilesView>(), Vms.Singleton<NewAnnouncementFilesViewModel>())
            .AddWithVVmResolving(Vs.Singleton<NewAnnouncementScheduleView>(), Vms.Singleton<NewAnnouncementScheduleViewModel>())
            .AddWithVVmResolving(Vs.Singleton<NewAnnouncementAccessSettingsView>(), Vms.Singleton<NewAnnouncementAccessSettingsViewModel>())
            .AddWithVVmResolving(Vs.Singleton<NewAnnouncementDurationView>(), Vms.Singleton<NewAnnouncementDurationViewModel>())
            .AddWithVVmResolving(Vs.Singleton<NewAnnouncementDiscountParametersView>(), Vms.Singleton<NewAnnouncementDiscountParametersViewModel>())
            .AddWithVVmResolving(Vs.Singleton<NewAnnouncementCompletedView>(), Vms.Singleton<NewAnnouncementCompletedViewModel>())

            .AddWithVVmResolving(Vs.Singleton<UpdateAnnouncementTargetView>(), Vms.Singleton<UpdateAnnouncementTargetViewModel>())
            .AddWithVVmResolving(Vs.Singleton<UpdateAnnouncementFilesView>(), Vms.Singleton<UpdateAnnouncementFilesViewModel>())
            .AddWithVVmResolving(Vs.Singleton<UpdateAnnouncementScheduleView>(), Vms.Singleton<UpdateAnnouncementScheduleViewModel>())
            .AddWithVVmResolving(Vs.Singleton<UpdateAnnouncementAccessSettingsView>(), Vms.Singleton<UpdateAnnouncementAccessSettingsViewModel>())
            .AddWithVVmResolving(Vs.Singleton<UpdateAnnouncementDurationView>(), Vms.Singleton<UpdateAnnouncementDurationViewModel>())
            .AddWithVVmResolving(Vs.Singleton<UpdateAnnouncementDiscountParametersView>(), Vms.Singleton<UpdateAnnouncementDiscountParametersViewModel>())
            .AddWithVVmResolving(Vs.Singleton<UpdateAnnouncementCompletedView>(), Vms.Singleton<UpdateAnnouncementCompletedViewModel>())

            .AddWithVVmResolving(Vs.Singleton<RemovingAnnouncementReasonView>(), Vms.Singleton<RemovingAnnouncementReasonViewModel>())
            .AddWithVVmResolving(Vs.Singleton<RemovingAnnouncementLongTermView>(), Vms.Singleton<RemovingAnnouncementLongTermViewModel>())
            .AddWithVVmResolving(Vs.Singleton<RemovingAnnouncementClosedView>(), Vms.Singleton<RemovingAnnouncementClosedViewModel>())
            .AddWithVVmResolving(Vs.Singleton<RemovingAnnouncementDetailsView>(), Vms.Singleton<RemovingAnnouncementDetailsViewModel>())
            .AddWithVVmResolving(Vs.Singleton<RemovingAnnouncementCompletedView>(), Vms.Singleton<RemovingAnnouncementCompletedViewModel>())

            .AddWithVVmResolving(Vs.Singleton<MainBottomTabbedView>(), Vms.Singleton<MainTabbedViewModel>(serviceProvider => new(
                serviceProvider,
                currentTabIndex: 3,
                [
                    typeof(CatalogNavigationViewModel),
                    typeof(HistoryNavigationViewModel),
                    typeof(ManagementNavigationViewModel),
                    typeof(MyProfileNavigationViewModel),
                ]))
            )

            .AddWithVVmResolving(Vs.Transient<MainNavigationView>(), Vms.Transient<NavigationViewModel>())

            .AddWithVVmResolving(Vs.Singleton<CatalogNavigationView>(), Vms.Singleton<CatalogNavigationViewModel>())
            .AddWithVVmResolving(Vs.Singleton<CatalogView>(), Vms.Singleton<CatalogViewModel>())

            .AddWithVVmResolving(Vs.Singleton<HistoryNavigationView>(), Vms.Singleton<HistoryNavigationViewModel>())
            .AddWithVVmResolving(Vs.Singleton<HistoryView>(), Vms.Singleton<HistoryViewModel>())

            .AddWithVVmResolving(Vs.Singleton<ManagementNavigationView>(), Vms.Singleton<ManagementNavigationViewModel>())
            .AddWithVVmResolving(Vs.Singleton<ManagementView>(), Vms.Singleton<ManagementViewModel>())

            .AddWithVVmResolving(Vs.Singleton<MyProfileNavigationView>(), Vms.Singleton<MyProfileNavigationViewModel>())
            .AddWithVVmResolving(Vs.Singleton<MyIndividualProfileView>(), Vms.Singleton<MyIndividualProfileViewModel>())
            .AddWithVVmResolving(Vs.Singleton<MyLegalPersonProfileView>(), Vms.Singleton<MyLegalPersonProfileViewModel>())
            .AddWithVVmResolving(Vs.Singleton<MyAnnouncementsView>(), Vms.Singleton<MyAnnouncementsViewModel>())
            .AddWithVVmResolving(Vs.Singleton<MyAnnouncementView>(), Vms.Singleton<MyAnnouncementViewModel>())
            .AddWithVVmResolving(Vs.Singleton<ExitView>(), Vms.Singleton<ExitViewModel>())
            .AddWithVVmResolving(Vs.Singleton<AboutUsView>(), Vms.Singleton<AboutUsViewModel>())

            .AddWithVVmResolving(Vs.Transient<AnnouncementView>(), Vms.Transient<AnnouncementViewModel>())
            .AddWithVVmResolving(Vs.Singleton<FilterChoiceView>(), Vms.Singleton<FilterChoiceViewModel>())
            .AddWithVVmResolving(Vs.Transient<SettingsView>(), Vms.Transient<SettingsViewModel>())
            .AddWithVVmResolving(Vs.Transient<ProfileView>(), Vms.Transient<ProfileViewModel>())
            .AddWithVVmResolving(Vs.Transient<AppointmentView>(), Vms.Transient<AppointmentViewModel>())

            .AddWithVVmResolving(Vs.Singleton<FavoritesTabbedView>(), Vms.Singleton<FavoritesTabbedViewModel>())
            .AddWithVVmResolving(Vs.Singleton<AnnouncementsView>(), Vms.Singleton<AnnouncementsViewModel>())
            .AddWithVVmResolving(Vs.Singleton<OwnersView>(), Vms.Singleton<OwnersViewModel>())

            .AddWithVVmResolving(Vs.Transient<ConfirmationView>(), Vms.Transient<ConfirmationViewModel>())
            .AddWithVVmResolving(Vs.Transient<ConfirmationDoneView>(), Vms.Transient<ConfirmationDoneViewModel>())
            .AddWithVVmResolving(Vs.Transient<ConfirmationCancelView>(), Vms.Transient<ConfirmationCancelViewModel>())
            .AddWithVVmResolving(Vs.Transient<ConfirmationCanceledView>(), Vms.Transient<ConfirmationCanceledViewModel>())

            .AddWithVVmResolving(Vs.Transient<HistoryAnnouncementsView>(), Vms.Transient<HistoryAnnouncementsViewModel>())
            .AddWithVVmResolving(Vs.Transient<HistoryOwnersView>(), Vms.Transient<HistoryOwnersViewModel>());

  //      builder.Services.AddWithVVmResolving(Vs.Singleton<WelcomeView>(), Vms.Singleton<WelcomeViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<YourGoalView>(), Vms.Singleton<YourGoalViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<LandingView>(), Vms.Singleton<LandingViewModel>());

  //      builder.Services.AddWithVVmResolving(Vs.Singleton<LoginView>(), Vms.Singleton<LoginViewModel>());

  //      builder.Services.AddWithVVmResolving(Vs.Singleton<NewPasswordLoginView>(), Vms.Singleton<NewPasswordLoginViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<NewPasswordCodeView>(), Vms.Singleton<NewPasswordCodeViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<NewPasswordEnteringView>(), Vms.Singleton<NewPasswordEnteringViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<NewPasswordCompletedView>(), Vms.Singleton<NewPasswordCompletedViewModel>());

  //      builder.Services.AddWithVVmResolving(Vs.Singleton<RegistrationCredentialsView>(), Vms.Singleton<RegistrationCredentialsViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<RegistrationCodeView>(), Vms.Singleton<RegistrationCodeViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<RegistrationGoalView>(), Vms.Singleton<RegistrationGoalViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<RegistrationContactsView>(), Vms.Singleton<RegistrationContactsViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<RegistrationAboutMyselfView>(), Vms.Singleton<RegistrationAboutMyselfViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<RegistrationCompletedView>(), Vms.Singleton<RegistrationCompletedViewModel>());

  //      builder.Services.AddWithVVmResolving(Vs.Singleton<NewAnnouncementTargetView>(), Vms.Singleton<NewAnnouncementTargetViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<NewAnnouncementPhotosView>(), Vms.Singleton<NewAnnouncementPhotosViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<NewAnnouncementOfferAgreementView>(), Vms.Singleton<NewAnnouncementOfferAgreementViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<NewAnnouncementScheduleView>(), Vms.Singleton<NewAnnouncementScheduleViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<NewAnnouncementAccessSettingsView>(), Vms.Singleton<NewAnnouncementAccessSettingsViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<NewAnnouncementDurationView>(), Vms.Singleton<NewAnnouncementDurationViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<NewAnnouncementDiscountParametersView>(), Vms.Singleton<NewAnnouncementDiscountParametersViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<NewAnnouncementCompletedView>(), Vms.Singleton<NewAnnouncementCompletedViewModel>());

  //      builder.Services.AddWithVVmResolving(Vs.Singleton<RemovingAnnouncementReasonView>(), Vms.Singleton<RemovingAnnouncementReasonViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<RemovingAnnouncementLongTermView>(), Vms.Singleton<RemovingAnnouncementLongTermViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<RemovingAnnouncementClosedView>(), Vms.Singleton<RemovingAnnouncementClosedViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<RemovingAnnouncementDetailsView>(), Vms.Singleton<RemovingAnnouncementDetailsViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<RemovingAnnouncementCompletedView>(), Vms.Singleton<RemovingAnnouncementCompletedViewModel>());



  //   //   builder.Services.AddWithVVmResolving(VS.Singleton<MyOrdersView>(), VMS.Singleton<MyOrdersViewModel>());
  //   //   builder.Services.AddWithVVmResolving(VS.Singleton<MyPastOrdersView>(), VMS.Singleton<MyPastOrdersViewModel>()); 


  //      builder.Services.AddWithVVmResolving(Vs.Singleton<MainBottomTabbedView>(), Vms.Singleton<MainTabbedViewModel>(serviceProvider => new(
  //          serviceProvider, 
  //          currentTabIndex: 3,
  //          [
  //              typeof(CatalogNavigationViewModel),
  //              typeof(HistoryNavigationViewModel),
  //              typeof(ManagementNavigationViewModel),
  //              typeof(MyProfileNavigationViewModel),
  //          ]))
  //      );

  //      builder.Services.AddWithVVmResolving(Vs.Transient<MainNavigationView>(), Vms.Transient<NavigationViewModel>());

  //      builder.Services.AddWithVVmResolving(Vs.Singleton<CatalogNavigationView>(), Vms.Singleton<CatalogNavigationViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<CatalogView>(), Vms.Singleton<CatalogViewModel>());

		////builder.Services.AddWithVVmResolving(VS.Singleton<ScheduleNavigationView>(), VMS.Singleton<ScheduleNavigationViewModel>());
		////builder.Services.AddWithVVmResolving(VS.Singleton<ScheduleView>(), VMS.Singleton<ScheduleViewModel>());


  //      builder.Services.AddWithVVmResolving(Vs.Singleton<HistoryNavigationView>(), Vms.Singleton<HistoryNavigationViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<HistoryView>(), Vms.Singleton<HistoryViewModel>());

  //      builder.Services.AddWithVVmResolving(Vs.Singleton<ManagementNavigationView>(), Vms.Singleton<ManagementNavigationViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<ManagementView>(), Vms.Singleton<ManagementViewModel>());

  //      builder.Services.AddWithVVmResolving(Vs.Singleton<MyProfileNavigationView>(), Vms.Singleton<MyProfileNavigationViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<MyIndividualProfileView>(), Vms.Singleton<MyIndividualProfileViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<MyLegalPersonProfileView>(), Vms.Singleton<MyLegalPersonProfileViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<MyAnnouncementsView>(), Vms.Singleton<MyAnnouncementsViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<MyAnnouncementView>(), Vms.Singleton<MyAnnouncementViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<ExitView>(), Vms.Singleton<ExitViewModel>());    
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<AboutUsView>(), Vms.Singleton<AboutUsViewModel>());

  //      builder.Services.AddWithVVmResolving(Vs.Transient<AnnouncementView>(), Vms.Transient<AnnouncementViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<FilterChoiceView>(), Vms.Singleton<FilterChoiceViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Transient<SettingsView>(), Vms.Transient<SettingsViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Transient<ProfileView>(), Vms.Transient<ProfileViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Transient<AppointmentView>(), Vms.Transient<AppointmentViewModel>());

  //      builder.Services.AddWithVVmResolving(Vs.Singleton<FavoritesTabbedView>(), Vms.Singleton<FavoritesTabbedViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<AnnouncementsView>(), Vms.Singleton<AnnouncementsViewModel>());
  //      builder.Services.AddWithVVmResolving(Vs.Singleton<OwnersView>(), Vms.Singleton<OwnersViewModel>());

		//builder.Services.AddWithVVmResolving(Vs.Transient<ConfirmationView>(), Vms.Transient<ConfirmationViewModel>());
		//builder.Services.AddWithVVmResolving(Vs.Transient<ConfirmationDoneView>(), Vms.Transient<ConfirmationDoneViewModel>());
		//builder.Services.AddWithVVmResolving(Vs.Transient<ConfirmationCancelView>(), Vms.Transient<ConfirmationCancelViewModel>());
		//builder.Services.AddWithVVmResolving(Vs.Transient<ConfirmationCanceledView>(), Vms.Transient<ConfirmationCanceledViewModel>());

		//builder.Services.AddWithVVmResolving(Vs.Transient<HistoryAnnouncementsView>(), Vms.Transient<HistoryAnnouncementsViewModel>());
		//builder.Services.AddWithVVmResolving(Vs.Transient<HistoryOwnersView>(), Vms.Transient<HistoryOwnersViewModel>());


        return builder;
    }
}
