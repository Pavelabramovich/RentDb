using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.Application.Validation;
using LilaRent.Domain.Fields;
using LilaRent.MobileApp.Core.Builders;
using LilaRent.MobileApp.Core.Validation;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;
using LilaRent.Requests.Services;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Builder), nameof(Builder))]
public partial class UpdateAnnouncementDurationViewModel : ObservableValidator
{
    private readonly INavigationService _navigationService;
    private readonly IAnnouncementService _announcementService;

    private AnnouncementUpdatingDtoBuilder? _builder;

    public AnnouncementUpdatingDtoBuilder? Builder
    {
        get => _builder;
        set
        {
            _builder = value;

            MinRentTime = _builder.MinRentTime;
            MaxRentTime = _builder.MaxRentTime;
            SelectedTimeUnit = _builder.RentTimeUnit;
            TimeUnitRentCost = _builder.TimeUnitRentCost;
            IsDiscountUsed = _builder.IsDiscountUsed ?? false;
        }
    }


    [ObservableProperty]
    [Required(ErrorMessage = "enter correct minimum rent time")]
    [RentMultiplierOf(nameof(SelectedTimeUnit), "minimum rent time")]
    private int? _minRentTime;

    [ObservableProperty]
    [Required(ErrorMessage = "enter correct maximum rent time")]
    [GreaterThan(nameof(MinRentTime), OtherPropertyName = "minimum rent time",  IsInclusive = true)]
    [RentMultiplierOf(nameof(SelectedTimeUnit), "maximum rent time")]
    private int? _maxRentTime;


    public IReadOnlyList<TimeUnit> TimeUnitsOptions { get; }

    [ObservableProperty]
    private TimeUnit _selectedTimeUnit;


    [ObservableProperty]
    [Required(ErrorMessage = "price is required")]
    [Use<PriceValidator, decimal>("price")]
    private decimal? _timeUnitRentCost;

    [ObservableProperty]
    private bool _isDiscountUsed;


    [ObservableProperty]
    private bool _isServerRequested;


    [ObservableProperty]
    private string? _minRentTimeError;

    [ObservableProperty]
    private string? _maxRentTimeError;

    [ObservableProperty]
    private string? _timeUnitRentCostError;



    public UpdateAnnouncementDurationViewModel(INavigationService navigationService, IAnnouncementService announcementService)
    {
        _navigationService = navigationService;
        _announcementService = announcementService;

        MinRentTime = null;
        MaxRentTime = null;

        TimeUnitsOptions = [TimeUnit.Minutes, TimeUnit.Hours, TimeUnit.Days];
        SelectedTimeUnit = TimeUnit.Minutes;

        TimeUnitRentCost = null;
        IsDiscountUsed = false;

        IsServerRequested = false;

        MinRentTimeError = null;
        MaxRentTimeError = null;
        TimeUnitRentCostError = null;
    }


    [RelayCommand]
    private async Task Publish()
    {
        if (!ValidateFields())
            return;

        if (Builder is null)
            throw new InvalidOperationException("Builder is not set.");

        Builder.MinRentTime = MinRentTime;
        Builder.MaxRentTime = MaxRentTime;
        Builder.RentTimeUnit = SelectedTimeUnit;
        Builder.TimeUnitRentCost = TimeUnitRentCost;
        Builder.IsDiscountUsed = IsDiscountUsed;

        var announcement = Builder.Build();
        
        try
        {
            IsServerRequested = true;

            await _announcementService.UpdateAsync(announcement);

            IsServerRequested = false;

            _navigationService.CurrentTabs.Navigation.PopOverRoot<UpdateAnnouncementCompletedViewModel>();
        }
        catch (Exception exception)
        {
            IsServerRequested = false;

            // Some other logic of displaying error
        }
    }

    [RelayCommand]
    private void ToDiscountParameters()
    {
        _navigationService.CurrentTabs.Navigation.Push<UpdateAnnouncementDiscountParametersViewModel>(new { Builder });
    }

    partial void OnMinRentTimeChanged(int? value)
    {
        MinRentTimeError = null;
    }

    partial void OnMaxRentTimeChanged(int? value)
    {
        MaxRentTimeError = null;
    }

    partial void OnTimeUnitRentCostChanged(decimal? value)
    {
        TimeUnitRentCostError = null;
    }

    partial void OnSelectedTimeUnitChanged(TimeUnit value)
    {
        MinRentTimeError = null;
        MaxRentTimeError = null;
    }

    private bool ValidateFields()
    {
        ValidateAllProperties();

        var minRentTimeErrors = GetErrors(nameof(MinRentTime));
        var maxRentTimeErrors = GetErrors(nameof(MaxRentTime));
        var timeUnitRentCostErrors = GetErrors(nameof(TimeUnitRentCost));

        var isValid = true;

        if (minRentTimeErrors.Any())
        {
            MinRentTimeError = minRentTimeErrors.First()!.ErrorMessage!;
            isValid = false;
        }
        else if (maxRentTimeErrors.Any())
        {
            MaxRentTimeError = maxRentTimeErrors.First()!.ErrorMessage!;
            isValid = false;
        }

        if (timeUnitRentCostErrors.Any())
        {
            TimeUnitRentCostError = timeUnitRentCostErrors.First()!.ErrorMessage!;
            isValid = false;
        }

        return isValid;
    }
}
