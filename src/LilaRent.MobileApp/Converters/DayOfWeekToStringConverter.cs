using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilaRent.MobileApp.Converters
{
	public class DayOfWeekToStringConverter : IValueConverter
	{
		public static string Convert(DayOfWeek day)
		{
			return day switch
			{
				DayOfWeek.Monday => "Mon",
				DayOfWeek.Tuesday => "Tue",
				DayOfWeek.Wednesday => "Wed",
				DayOfWeek.Thursday => "Thu",
				DayOfWeek.Friday => "Fri",
				DayOfWeek.Saturday => "Sat",
				DayOfWeek.Sunday => "Sun",
				_ => "?"
			};
		}

		public static DayOfWeek ConvertBack(string day)
		{
			return day switch
			{
				"Mon" => DayOfWeek.Monday,
				"Tue" => DayOfWeek.Tuesday,
				"Wed" => DayOfWeek.Wednesday ,
				"Thu" => DayOfWeek.Thursday,
				"Fri" => DayOfWeek.Friday,
				"Sat" => DayOfWeek.Saturday,
				"Sun" => DayOfWeek.Sunday,


				_ => DayOfWeek.Monday
			};
		}

		public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			ArgumentNullException
			.ThrowIfNull(value, nameof(value));

			try
			{
				DayOfWeek? val = (DayOfWeek)value;

				return Convert(val.Value);
			}
			catch (Exception ex) when (ex
				is FormatException
				or InvalidCastException
				or OverflowException
				or InvalidOperationException)
			{
				throw new ArgumentException("Value must be a DayOfWeek type", nameof(value), ex);
			}
		}


		public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			ArgumentNullException
			.ThrowIfNull(value, nameof(value));

			try
			{
				string val = (string)value;

				return ConvertBack(val);
			}
			catch (Exception ex) when (ex
				is FormatException
				or InvalidCastException
				or OverflowException
				or InvalidOperationException)
			{
				throw new ArgumentException("Value must be a string type", nameof(value), ex);
			}
		}
	}
}
