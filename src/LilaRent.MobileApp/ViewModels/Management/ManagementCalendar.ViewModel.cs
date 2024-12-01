using CommunityToolkit.Mvvm.ComponentModel;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.MobileApp.ViewModels;

public partial class ManagementCalendarViewModel : IViewModel
{
	private const int MonthCount = 5;

	private readonly IProfileService _profileService;
	private readonly IAppointmentService _appointmentService;
	private readonly IFakeAnnouncementsService _announcementService;

	[ObservableProperty]
	IEnumerable<DateOnly> _months;

	[ObservableProperty]
	Appointment _appointment;

	[ObservableProperty]
	IDictionary<Position, IDictionary<DateOnly, TimeRange>> _table;

	public ManagementCalendarViewModel(IProfileService profileService, IAppointmentService appointmentService, IFakeAnnouncementsService announcementsService)
	{
		//_profileService = profileService;
		//_appointmentService = appointmentService;
		//_announcementService = announcementsService;
		//Table = new Dictionary<Position, IDictionary<DateOnly, TimeRange>>();

		//long profileId = profileService.CurrentProfile.Id;


		////populate months
		//var months = new List<DateOnly>();
		//DateOnly it = DateOnly.FromDateTime(DateTime.Now);
		//for(int i = 0; i < MonthCount; ++i)
		//{
		//	months.Add(it);
		//	it = new DateOnly(it.Year, it.Month, 1);
		//	it = it.AddMonths(1);
		//}

		//// populate position
		//var announcement = announcementsService.GetAnnouncements(ancmnt => ancmnt.Profile == _profileService.CurrentProfile).Result.First();
		//Appointment = appointmentService.GetAppointmentByAnnouncmentId(announcement.Id);

		//foreach(var position in Appointment.Positions)
		//{
		//	var dict = new Dictionary<DateOnly, TimeRange>();
		//	foreach(var month in months)
		//	{
		//		int monthNumber = month.Month;
		//		var itMonth = month;

		//		while(itMonth.Month == monthNumber)
		//		{
		//			dict[itMonth] = new TimeRange() { Begin = new TimeOnly(9, 0), End = new TimeOnly(20, 0) };
		//			itMonth = itMonth.AddDays(1);
		//		}

		//	}
		//	Table[position] = dict;
		//}

		//Months = months;
	}
}
