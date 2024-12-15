using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using LilaRent.Application.Dto;
using LilaRent.Requests.Services;
using LilaRent.MobileApp.Core;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(Announcement), nameof(Announcement))]
[QueryProperty(nameof(Reservations), nameof(Reservations))]
public partial class ConfirmationViewModel : ObservableObject
{
	private readonly INavigationService _navigationService;
	private readonly IAppointmentService _appointmentService;
	private readonly IAnnouncementService _announcementService;
	private readonly IRangesService _rangesService;
	private readonly IOrdersService _ordersService;
	private readonly IProfileManager _profileService;


	//public DateOnly DateParameter
	//{
	//	set
	//	{
	//		Date = value;

	//		SetOtherProperties();
	//	}
	//}

	//public long AnnouncementIdParameter
	//{
	//	set
	//	{
	//		Announcement = _announcementService.GetAnnouncementById(value);
	//		Appointment = _appointmentService.GetAppointmentByAnnouncmentId(value);
	//		Profile = Announcement.Profile;
	//		SetOtherProperties();
	//	}
	//}



	//[ObservableProperty]
	//private DateOnly? _date = null;

	//[ObservableProperty]
	//private IEnumerable<DateOnly> _dates;

	//[ObservableProperty]
	//private AnnouncementDetailsDto _announcement;

	//[ObservableProperty]
	//private Appointment _appointment;

	//[ObservableProperty]
	//private Profile _profile;

	//[ObservableProperty]
	//private IDictionary<Position, IEnumerable<TimeRange>> _ranges;

	//[ObservableProperty]
	//private Position _currentPosition;

	[ObservableProperty]
	public AnnouncementDetailsDto _announcement;// { get; set; }

	public IEnumerable<ReservationSummaryDto> Reservations { get; set; }

	[ObservableProperty]
	private TimeSpan? _begin;

	[ObservableProperty]
	private TimeSpan? _end;


	[ObservableProperty]
	private DateTime _date;

	//[ObservableProperty]
	//private bool _isValid;

	[ObservableProperty]
	private string _errorMessage;


	[ObservableProperty]
	private bool _isServerRequested;


	public ConfirmationViewModel(INavigationService navigationService, IAnnouncementService announcmentService, 
								IAppointmentService appointmentService, IRangesService rangesService, 
								IProfileManager profileService,
								IOrdersService ordersService)
	{
		_announcementService = announcmentService;
		_appointmentService = appointmentService;
		_navigationService = navigationService;
		_rangesService = rangesService;
		_ordersService = ordersService;
		_profileService = profileService;

		Date = DateTime.Now.Date;
	}

	//private void SetOtherProperties()
	//{
	//	if(Appointment is not null && Date is not null)
	//	{
	//		Ranges = _rangesService.GetRangesForAppointmentAndDate(Appointment, (DateOnly)Date);

	//		List<DateOnly> dates = new();
	//		DateOnly today = DateOnly.FromDateTime(DateTime.Now);
	//		for (DateOnly it = today; it <= today.AddDays(Appointment.AccessibleDaysAfter); it = it.AddDays(1))
	//		{
	//			dates.Add(it);
	//		}

	//		Dates = dates;

	//		CurrentPosition = Ranges.First().Key;

	//		Begin = Appointment.OpenTime.ToTimeSpan();
	//		End = Appointment.OpenTime.Add(Appointment.MinimalTime).ToTimeSpan();
	//	}
	//}

	public void Validate()
	{
		if (Begin is null)
		{
			ErrorMessage = "Begin is required";
			return;
		}

		if (End is null)
		{
			ErrorMessage = "End is required";
			return;
		}

		if (Begin is not null && End is not null && Begin >= End)
		{
			ErrorMessage = "Begin should be earlier than end";
		//	IsValid = false;
			return;
		}

        // On open - close
        if (Begin < Announcement.OpenTime.ToTimeSpan() || End > Announcement.CloseTime.ToTimeSpan())
		{
			ErrorMessage = "Object is closed on this time";
			return;
		}

		// dooesn't intersects with other ranges

		var endDateTime = Date + End;
		var beginDateTime = Date + Begin;

        if (beginDateTime < DateTime.Now)
        {
            ErrorMessage = "This time in past";
            return;
        }

        foreach (var range in Reservations)
		{
			if (endDateTime > range.Begin + TimeSpan.FromHours(3) && beginDateTime < range.End + TimeSpan.FromHours(3))     // true if intersects
			{
				ErrorMessage = "There is an appointment on this time";
				return;
			}
		}

		ErrorMessage = null;

		//IsValid = true;
	}

	[RelayCommand]
	private async Task CreateOrder()
	{
		Validate();

		if (ErrorMessage is not null)
			return;

		try
		{
			IsServerRequested = true;

			var creatingDto = new ReservationCreatingDto()
			{
				ClientId = _profileService.CurrentProfile.Id,
				AnnouncementId = Announcement.Id,
				Begin = (Date + Begin).Value,
				End = (Date + End).Value,
				CreatedAt = DateTime.Now
			};

			await _announcementService.AddReservationAsync(creatingDto);

			IsServerRequested = false;

            _navigationService.CurrentTabs.Navigation.Push<ConfirmationDoneViewModel>(new
			{
				Announcement, Reservation = creatingDto
			});
        }
		catch (Exception ex)
		{
			IsServerRequested = false;

			// Handling exceptions
		}


		//if (IsValid)
		//{
		//	var range = new TimeRange() { Begin = TimeOnly.FromTimeSpan(Begin), End = TimeOnly.FromTimeSpan(End) };
		//	var order = _ordersService.CreateOrderAsync(Announcement, (DateOnly)Date, range).Result;

		//	var args = new
		//	{
		//		OrderId = order.Id
		//	};

		//	_navigationService.CurrentTabs.Navigation.Push<ConfirmationDoneViewModel>(args);
		//}
	}

	partial void OnBeginChanged(TimeSpan? value)
	{
		Validate();
	}

	partial void OnEndChanged(TimeSpan? value)
	{
		Validate();
	}

	//partial void OnCurrentPositionChanged(Position value)
	//{
	//	if(value != null)
	//		Validate();
	//}

	partial void OnDateChanged(DateTime value)
	{
		Validate();
	}
}
