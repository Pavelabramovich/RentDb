using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.MobileApp.Entities;


public enum OrderStatus : byte
{
	Past = 0,
	Current = 1,
    Upcoming = 2,
}


public class OrderBasic
{
	public long Id { get; set; }

	public TimeRange Range { get; set; }

	public DateOnly Date { get; set; }

	public AnnouncementInfo Announcement { get; set; }

	public User User { get; set; }

	public OrderStatus Status	// Need to be changed for localisation
	{
		get
		{
			DateTime Start = new DateTime(Date, Range.Begin);
			DateTime End = new DateTime(Date, Range.End);

			if (End <=  DateTime.Now)
			{
				return OrderStatus.Past;
			}
			else if (Start <= DateTime.Now && End > DateTime.Now)
			{
				return OrderStatus.Current;
			}
			else
			{
				return OrderStatus.Upcoming;
			}
		}
	}
}
