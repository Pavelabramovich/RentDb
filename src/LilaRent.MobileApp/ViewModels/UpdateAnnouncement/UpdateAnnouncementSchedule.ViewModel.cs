using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.Application.Validation;
using LilaRent.MobileApp.Core.Builders;
using LilaRent.MobileApp.Core.Validation;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Builder), nameof(Builder))]
public partial class UpdateAnnouncementScheduleViewModel : ObservableValidator
{
    private readonly INavigationService _navigationService;
    private readonly BreakValidator _breakValidator;


    private AnnouncementUpdatingDtoBuilder? _builder;

    public AnnouncementUpdatingDtoBuilder? Builder
    {
        get => _builder;
        set
        {
            _builder = value;

            RentStart = Builder.RentStart;
            RentEnd = Builder.RentEnd;
            Break = Builder.Break;
        }
    }


    [ObservableProperty]
    [Required(ErrorMessage = "rent start is required")]
    private TimeOnly? _rentStart;

    [ObservableProperty]
    [Required(ErrorMessage = "rent end is required")]
  //  [GreaterThan(nameof(RentStart), ErrorMessage = "rent ent time should be greather then rent start time")]
    private TimeOnly? _rentEnd;

    [ObservableProperty]
    [BreakFormat("break", IsEmptyProperty = nameof(IsBreakEmpty))]
    private int? _break;

    [ObservableProperty]
    private bool _isBreakEmpty;


    [ObservableProperty]
    private string? _rentStartError;

    [ObservableProperty]
    private string? _rentEndError;

    [ObservableProperty]
    private string? _breakError;


    public UpdateAnnouncementScheduleViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        _breakValidator = new BreakValidator("break");

        RentStart = null;
        RentEnd = null;

        Break = null;
        IsBreakEmpty = true;
    }


    [RelayCommand]
    private void Next()
    {
        if (!ValidateFields())
            return;

        if (Builder is null)
            throw new InvalidOperationException("Announcement builder is not set");

        Builder.RentStart = RentStart;
        Builder.RentEnd = RentEnd;
        Builder.Break = Break ?? 0;

        _navigationService.CurrentTabs.Navigation.Push<UpdateAnnouncementDurationViewModel>(new { Builder });
    }

    [RelayCommand]
    private void ToAccessSettings()
    {
        _navigationService.CurrentTabs.Navigation.Push<UpdateAnnouncementAccessSettingsViewModel>(new { Builder });
    }


    partial void OnRentStartChanged(TimeOnly? value)
    {
        RentStartError = null;
    }

    partial void OnRentEndChanging(TimeOnly? value)
    {
        RentEndError = null;
    }

    partial void OnBreakChanged(int? value)
    {
        BreakError = null;
    }


    private bool ValidateFields()
    {
        ValidateAllProperties();

        var rentStartErrors = GetErrors(nameof(RentStart));
        var rentEndErrors = GetErrors(nameof(RentEnd));
        var breakErrors = GetErrors(nameof(Break));

        var isValid = true;

        if (rentStartErrors.Any())
        {
            RentStartError = rentStartErrors.First()!.ErrorMessage!;
            isValid = false;
        }
        else if (rentEndErrors.Any())
        {
            RentEndError = rentEndErrors.First()!.ErrorMessage!;
            isValid = false;
        }

        if (breakErrors.Any())
        {
            BreakError = breakErrors.First()!.ErrorMessage!;
            isValid = false;
        }

        return isValid;
    }
}
