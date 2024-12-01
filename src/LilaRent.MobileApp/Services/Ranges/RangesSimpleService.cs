using LilaRent.MobileApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilaRent.MobileApp.Services;

public class RangesSimpleService : IRangesService
{
	IEnumerable<TimeRange> _list1 = new TimeRange[]
	{
		new TimeRange() { Begin = new TimeOnly(10, 0), End = new TimeOnly(12, 0)},
		new TimeRange() { Begin = new TimeOnly(14, 0), End = new TimeOnly(15, 30)},
		new TimeRange() { Begin = new TimeOnly(15, 00), End = new TimeOnly(18, 0)},
	};

	IEnumerable<TimeRange> _list2 = new TimeRange[]
	{
		new TimeRange() { Begin = new TimeOnly(11, 0), End = new TimeOnly(12, 0)},
		new TimeRange() { Begin = new TimeOnly(9, 0), End = new TimeOnly(10, 30)},
		new TimeRange() { Begin = new TimeOnly(15, 30), End = new TimeOnly(17, 30)},
	};
	public IDictionary<Position, IEnumerable<TimeRange>> GetRangesForAppointmentAndDate(Appointment appointment, DateOnly date)
	{
		Dictionary<Position, IEnumerable<TimeRange>> res = new();

		int i = date.Day % 2;

		foreach(var pos in appointment.Positions)
		{
			if( i % 2 == 0)
			{
				res.Add(pos, _list1);
			}
			else
			{
				res.Add(pos, _list2);
			}

			++i;
		}

		return res;
	}
}
