using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Core.Builders;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Builder), nameof(Builder))]
public partial class NewAnnouncementAccessSettingsViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public AnnouncementCreatingDtoBuilder? Builder { get; set; }


    [ObservableProperty]
    private bool _canClientsChangeRecords;

    [ObservableProperty]
    private bool _canClientsDisableRecords;


    public NewAnnouncementAccessSettingsViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        CanClientsChangeRecords = false;
        CanClientsDisableRecords = false;
    }

    partial void OnCanClientsChangeRecordsChanged(bool value)
    {
        if (Builder is null)
            throw new InvalidOperationException("Announcement builder is not set");

        Builder.CanClientsChangeRecords = value;
    }

    partial void OnCanClientsDisableRecordsChanged(bool value)
    {
        if (Builder is null)
            throw new InvalidOperationException("Announcement builder is not set");

        Builder.CanClientsDisableRecords = value;
    }

    [RelayCommand]
    private void Back()
    {
        _navigationService.CurrentTabs.Navigation.Pop(new { Builder });
    }
}