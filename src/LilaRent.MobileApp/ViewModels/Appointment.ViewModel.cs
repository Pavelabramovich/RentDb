using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Views;

namespace LilaRent.MobileApp.ViewModels;

public class MyDateOnly
{
	public DateOnly DateOnly { get; set; }

	public MyDateOnly(DateOnly date)
	{
		DateOnly = date;
	}
}

[QueryProperty(nameof(AnnouncementId), "AnnouncementId")]
public partial class AppointmentViewModel : ObservableObject
{
	private readonly IFakeAnnouncementsService _announcementsService;
	private readonly IAppointmentService _appointmentService;
	private readonly IRangesService _rangesService;
	private readonly INavigationService _navigationService;

	public AppointmentViewModel(IFakeAnnouncementsService announcementsService, INavigationService navigationService,
								IAppointmentService appointmentService, IRangesService rangesService)
	{
		_announcementsService = announcementsService;
		_appointmentService = appointmentService;
		_rangesService = rangesService;
		_navigationService = navigationService;
	}

	[ObservableProperty]
	MyDateOnly[] _dates = Array.Empty<MyDateOnly>();

	public long AnnouncementId
	{
		set
		{
			Announcement = _announcementsService.GetAnnouncementById(value);
			Appointment = _appointmentService.GetAppointmentByAnnouncmentId(value);
			CurrentPosition = Appointment.Positions.First();
			Ranges = _rangesService.GetRangesForAppointmentAndDate(Appointment, Date);

			DateTime it = DateTime.Now.Date.AddDays(-Appointment.AccessibleDaysBefore);

			List<MyDateOnly> dates = new List<MyDateOnly>();

			while(it < DateTime.Now.Date.AddDays(Appointment.AccessibleDaysAfter))
			{
				dates.Add(new MyDateOnly(DateOnly.FromDateTime(it)));
				//DateOnly.FromDateTime(it).Mon
				it = it.AddDays(1);
			}

			Dates = dates.ToArray();
		}
	}

	[ObservableProperty]
	private AnnouncementInfo _announcement;

	[ObservableProperty]
	private Appointment _appointment;

	[ObservableProperty]
	private TimeOnly? _begin = null;

	[ObservableProperty]
	private TimeOnly? _end = null;

	private DateOnly _date = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
	public DateOnly Date
	{
		get => _date;
		set
		{
			SetProperty(ref _date, value);
			Ranges = _rangesService.GetRangesForAppointmentAndDate(Appointment, _date);
		}
	}

	[ObservableProperty]
	private IDictionary<Position, IEnumerable<TimeRange>> _ranges;

	public bool IsValid
	{
		get
		{
			if (Begin >= End)
				return false;
			// On open - close
			if (Begin < Appointment.OpenTime || End > Appointment.CloseTime)
				return false;

			// dooesn't intersects with other ranges
			foreach (var range in Ranges[_currentPosition])
			{
				if (End > range.Begin && Begin < range.End)     // true if intersects
					return false;
			}

			return true;
		}
	}

	[RelayCommand]
	private void AddDay()
	{
		Date = Date.AddDays(1);
	}

	[RelayCommand]
	private void SubDay()
	{
		Date = Date.AddDays(-1);
	}

	[RelayCommand]
	private void SetDate(object d)
	{
		var date = d as MyDateOnly;

		if (date == null)
			throw new ArgumentException($"Argument must have type {typeof(MyDateOnly)} in SetDate command");

		Date = date.DateOnly;
	}

	[ObservableProperty]
	Position _currentPosition;

	[RelayCommand]
	private void ToConfirmation()
	{
		_navigationService.CurrentTabs.Navigation.Push<ConfirmationViewModel>(
			new 
			{
				Date = Date,
				AnnouncementId = Announcement.Id
			}
		);
	}
}
