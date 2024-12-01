using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilaRent.MobileApp.Converters
{
	public class TimeOnlyToTimeSpanConverter : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value is null)
				return null;

			var time = value as TimeOnly?;

			if (time is null)
				throw new ArgumentException($"type of value is {value.GetType()} instead of {typeof(TimeOnly)}");

			return new TimeSpan(time.Value.Hour, time.Value.Minute, time.Value.Second);
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value is null)
				return null;

			var span = value as TimeSpan?;

			if (span is null)
				throw new ArgumentException($"type of value is {value.GetType()} instead of {typeof(TimeSpan)}");

			return new TimeOnly(span.Value.Hours, span.Value.Minutes, span.Value.Seconds);
		}
	}
}
