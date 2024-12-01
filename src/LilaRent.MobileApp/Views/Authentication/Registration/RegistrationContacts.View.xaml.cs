using CommunityToolkit.Maui.Converters;
using System.Globalization;


namespace LilaRent.MobileApp.Views;


public partial class RegistrationContactsView : ContentPage
{
	public RegistrationContactsView()
	{
        Resources.Add("ErrorsConverter", new ErrorConverter());

        InitializeComponent();
    }


    private class ErrorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(values[0]?.ToString()))
                return values[0];

            if (!string.IsNullOrEmpty(values[1]?.ToString()))
                return values[1];

            return values[2];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
