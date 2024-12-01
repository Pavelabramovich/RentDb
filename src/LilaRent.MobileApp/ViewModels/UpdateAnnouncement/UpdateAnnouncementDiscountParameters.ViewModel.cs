using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.Application.Validation;
using LilaRent.Domain.Fields;
using LilaRent.MobileApp.Core.Builders;
using LilaRent.MobileApp.Core.Validation;
using LilaRent.MobileApp.Services;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Builder), nameof(Builder))]
public partial class UpdateAnnouncementDiscountParametersViewModel : ObservableValidator
{
    private readonly INavigationService _navigationService;

    private AnnouncementUpdatingDtoBuilder? _builder;

    public AnnouncementUpdatingDtoBuilder? Builder
    {
        get => _builder;
        set
        {
            _builder = value;

            MinTimeForDiscount = _builder.MinTimeForDiscount;
            MaxTimeForDiscount = _builder.MinTimeForDiscount;
            SelectedTimeUnit = _builder.DiscountTimeUnit;
            DiscountPercentage = _builder.DiscountPercentage;
        }
    }


    [ObservableProperty]
    [Required(ErrorMessage = "enter valid minimum time for discount")]
    [RentMultiplierOf(nameof(SelectedTimeUnit), "minimum time for discount")]
    private int? _minTimeForDiscount;

    [ObservableProperty]
    private bool _isMinTimeForDiscountEmpty;


    [ObservableProperty]
    [Required(ErrorMessage = "enter valid maximum time for discount")]
    [GreaterThan(nameof(MinTimeForDiscount), OtherPropertyName = "minimum time for discount", IsInclusive = true)]
    [RentMultiplierOf(nameof(SelectedTimeUnit), "maximum time for discount")]
    private int? _maxTimeForDiscount;

    [ObservableProperty]
    private bool _isMaxTimeForDiscountEmpty;


    public IReadOnlyList<TimeUnit> TimeUnitsOptions { get; }

    [ObservableProperty]
    private TimeUnit _selectedTimeUnit;


    [ObservableProperty]
    [Required(ErrorMessage = "enter valid discount percentage")]
    [Use<PercentageValidator, int>("discount percentage")]
    private int? _discountPercentage;


    [ObservableProperty]
    private string? _minTimeForDiscountError;

    [ObservableProperty]
    private string? _maxTimeForDiscountError;

    [ObservableProperty]
    private string? _discountPercentageError;


    public UpdateAnnouncementDiscountParametersViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        MinTimeForDiscount = null;
        IsMinTimeForDiscountEmpty = true;

        MaxTimeForDiscount = null;
        IsMaxTimeForDiscountEmpty = true;

        TimeUnitsOptions = [TimeUnit.Minutes, TimeUnit.Hours, TimeUnit.Days];
        SelectedTimeUnit = TimeUnit.Minutes;
        
        DiscountPercentage = null;

        MinTimeForDiscountError = null;
        MaxTimeForDiscountError = null;
        DiscountPercentageError = null;
    }


    [RelayCommand]
    private void Save()
    {
        if (!ValidateFields())
            return;

        if (Builder is null)
            throw new InvalidOperationException("Builder is not set.");

        Builder.MinTimeForDiscount = MinTimeForDiscount;
        Builder.MaxTimeForDiscount = MaxTimeForDiscount;
        Builder.DiscountTimeUnit = SelectedTimeUnit;
        Builder.DiscountPercentage = DiscountPercentage;

        _navigationService.CurrentTabs.Navigation.Pop();
    }


    partial void OnMinTimeForDiscountChanged(int? value)
    {
        MinTimeForDiscountError = null;
    }

    partial void OnMaxTimeForDiscountChanged(int? value)
    {
        MaxTimeForDiscountError = null;
    }

    partial void OnDiscountPercentageChanged(int? value)
    {
        DiscountPercentageError = null;
    }

    partial void OnSelectedTimeUnitChanged(TimeUnit value)
    {
        MinTimeForDiscountError = null;
        MaxTimeForDiscountError = null;
    }


    private bool ValidateFields()
    {
        ValidateAllProperties();

        var minTimeForDiscountErrors = IsMinTimeForDiscountEmpty ? [] : GetErrors(nameof(MinTimeForDiscount));
        var maxTimeForDiscountErrors = IsMaxTimeForDiscountEmpty ? [] : GetErrors(nameof(MaxTimeForDiscount));
        var discountPercentageErrors = GetErrors(nameof(DiscountPercentage));

        var isValid = true;

        if (minTimeForDiscountErrors.Any())
        {
            MinTimeForDiscountError = minTimeForDiscountErrors.First()!.ErrorMessage!;
            isValid = false;
        }
        else if (maxTimeForDiscountErrors.Any())
        {
            MaxTimeForDiscountError = maxTimeForDiscountErrors.First()!.ErrorMessage!;
            isValid = false;
        }
        
        if (discountPercentageErrors.Any())
        {
            DiscountPercentageError = discountPercentageErrors.First()!.ErrorMessage!;
            isValid = false;
        }

        return isValid;
    }
}