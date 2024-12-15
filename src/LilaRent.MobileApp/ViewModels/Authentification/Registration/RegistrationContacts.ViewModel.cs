using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Core.Validation;
using LilaRent.Application.Validation;
using LilaRent.MobileApp.Core.Builders;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Builder), nameof(Builder))]
public partial class RegistrationContactsViewModel : ObservableValidator
{
    private readonly INavigationService _navigationService;
    private readonly IEmailValidationService _emailValidationService;
    private readonly PhoneValidator _phoneValidator;


    public UserWithProfileBuilder? Builder { get; set; }


    [ObservableProperty]
    [Use<NameValidator, string>]
    private string _name;

    [ObservableProperty]
  //  [Use<PhoneValidator, string>]
    private string _phone;

    [ObservableProperty]
    [Optional<EmailValidator, string>]
    private string _email;


    [ObservableProperty]
    private string _nameError;

    [ObservableProperty]
    private string _phoneError;

    [ObservableProperty]
    private string _emailError;


    public RegistrationContactsViewModel(INavigationService navigationService, IEmailValidationService emailValidationService, PhoneValidator phoneValidator)
    {
        _navigationService = navigationService;
        _emailValidationService = emailValidationService;
        _phoneValidator = phoneValidator;

        Name = string.Empty;
        Phone = string.Empty;
        Email = string.Empty;

        NameError = string.Empty;
        PhoneError = string.Empty;
        EmailError = string.Empty;
    }


    [RelayCommand]
    private async Task Continue()
    {
        if (!await ValidateFields())
            return;

        if (Builder is null)
            throw new InvalidOperationException("Builder is not set.");

        Builder.Name = Name;
        Builder.Phone = NormalizePhone(Phone);

        Builder.Email = string.IsNullOrEmpty(Email)
            ? Builder.Login
            : Email;

        _navigationService.Navigation.Push<RegistrationAboutMyselfViewModel>(new { Builder });
    }


    private async Task<bool> ValidateFields()
    {
        ValidateAllProperties();

        var nameErrors = GetErrors(nameof(Name));
      //  var phoneErrors = GetErrors(nameof(Phone));
        var emailErrors = GetErrors(nameof(Email));

        if (nameErrors.Any())
        {
            NameError = nameErrors.First().ErrorMessage!;
            return false;
        }

        if (_phoneValidator.Validate(NormalizePhone(Phone)) is { IsValid: false } result)
        {
            PhoneError = result.Errors.First().ErrorMessage;
            return false;
        }

        //if (phoneErrors.Any())
        //{
        //    PhoneError = phoneErrors.First().ErrorMessage!;
        //    return false;
        //}


        if (emailErrors.Any())
        {
            EmailError = emailErrors.First()!.ErrorMessage!;
            return false;   
        }

        if (!await _emailValidationService.IsExistAsync(Email))
        {
            EmailError = "This email is not exist";
            return false;
        }

        return true;
    }

    partial void OnNameChanged(string value)
    {
        NameError = string.Empty;
    }

    partial void OnPhoneChanged(string value)
    {
        PhoneError = string.Empty;
    }

    partial void OnEmailChanged(string value)
    {
        EmailError = string.Empty;
    }

    private static string NormalizePhone(string phone)
    {
        phone = phone.Trim();
        phone = phone.Replace(" ", string.Empty);

        phone = phone.Replace("(", string.Empty);
        phone = phone.Replace(")", string.Empty);
        phone = phone.Replace("-", string.Empty);

        if (phone.Length <= 1)
            return phone;

        var numbers = phone[1..];

        if (phone.StartsWith("+375"))
        {
            if (numbers.Length != 12)
                return phone;

            phone = $"+{numbers[..3]} ({numbers[3..5]}) {numbers[5..8]} {numbers[8..12]}";
        }
        else if (phone.StartsWith("+7"))
        {
            if (phone.Length != 11)
                return phone;

            phone = $"+{numbers[0]} ({numbers[1..4]}) {numbers[4..7]} {numbers[7..11]}";
        }

        return phone;
    }

}
