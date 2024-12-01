using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilaRent.MobileApp.Entities
{
	public struct TimeRange
	{
		public TimeOnly Begin { get; set; } 
		public TimeOnly End { get; set; }
		public TimeSpan Span()
		{
			return End - Begin;	
		}
	}

    public class Appointment
    {
		public long AnnouncmentId;

		public TimeOnly OpenTime;

		public TimeOnly CloseTime;

		public TimeSpan MinimalTime;

		public TimeSpan MaximalTime;

		/// <summary>
		///		Measure for appointment
		/// </summary>
		/// 10
		public TimeSpan Granularity;

		/// <summary>
		///		Measure for distance between appointments
		/// </summary>
		public TimeSpan Gap;

		// public IEnumerable<TimeRange> TimeRanges;

		public IEnumerable<Position> Positions;

		public int AccessibleDaysBefore = 10;
		public int AccessibleDaysAfter = 10;
    }

	public class Position
	{
		public string Title  { get; set; } = "";

		public int AccessibleDays { get; set; } = 10;

		public string ImagePath  { get; set; } = "sigmastare2.jpg";

		public override string ToString()
		{
			return Title;
		}
	}
}
