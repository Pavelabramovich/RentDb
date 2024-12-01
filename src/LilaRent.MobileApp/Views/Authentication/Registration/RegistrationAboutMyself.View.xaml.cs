using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.Views;
using LilaRent.MobileApp.ViewModels;
using System.ComponentModel;
using System.Globalization;


namespace LilaRent.MobileApp.Views;


public partial class RegistrationAboutMyselfView : ContentPage
{
    private readonly Func<Popup> _popupFactory;
    private Popup? _currentPopup;


    public RegistrationAboutMyselfView()
	{
        Resources.Add("ErrorsConverter", new ErrorConverter());

        InitializeComponent();


        var template = (DataTemplate)App.Current.Resources.MergedDictionaries.Skip(2).First()["LoadingPopupFactory"];

        _popupFactory = () =>
        {
            var popup = (Popup)template.LoadTemplate();
            popup.CanBeDismissedByTappingOutsideOfPopup = false;
            return popup;
        };
    }


    public new RegistrationAboutMyselfViewModel BindingContext => (RegistrationAboutMyselfViewModel)base.BindingContext;

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        BindingContext.PropertyChanged += BindingContext_PropertyChanged;
    }

    private async void BindingContext_PropertyChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName == nameof(RegistrationAboutMyselfViewModel.IsServerRequested))
        {
            var isRequested = BindingContext.IsServerRequested;

            if (isRequested)
            {
                _currentPopup = _popupFactory();

                await this.ShowPopupAsync(_currentPopup);
            }
            else
            {
                await _currentPopup!.CloseAsync();
                _currentPopup = null;
            }
        }
    }


    private async void ContentButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync();

            if (result?.FullPath != BindingContext.ImagePath)
            {
                BindingContext.ImagePath = result?.FullPath;
            }

            return;
        }
        catch 
        { }

        return;
    }

    private class ErrorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(values[0]?.ToString()))
                return values[0];

            return values[1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}