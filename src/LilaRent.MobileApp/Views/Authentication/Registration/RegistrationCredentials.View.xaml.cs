using CommunityToolkit.Maui.Converters;
using LilaRent.MobileApp.Core;
using System.Globalization;


namespace LilaRent.MobileApp.Views;


public partial class RegistrationCredentialsView : ContentPage
{
	public RegistrationCredentialsView()
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

            return values[1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
