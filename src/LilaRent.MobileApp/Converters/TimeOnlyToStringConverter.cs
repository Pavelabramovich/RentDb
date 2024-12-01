using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilaRent.MobileApp.Converters
{
	public class TimeOnlyToStringConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value is null)
				return null;

			var time = value as TimeOnly?;

			if (time is null)
				throw new ArgumentException($"type of value is {value.GetType()} instead of {typeof(TimeOnly)}");

			return time.ToString();
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
