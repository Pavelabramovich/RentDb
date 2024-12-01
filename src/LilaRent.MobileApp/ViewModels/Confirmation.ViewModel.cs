using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;


namespace LilaRent.MobileApp.ViewModels
{
	[QueryProperty(nameof(DateParameter), "Date")]
	[QueryProperty(nameof(AnnouncementIdParameter), "AnnouncementId")]
	public partial class ConfirmationViewModel : ObservableObject
	{
		private readonly INavigationService _navigationService;
		private readonly IAppointmentService _appointmentService;
		private readonly IFakeAnnouncementsService _announcementService;
		private readonly IRangesService _rangesService;
		private readonly IOrdersService _ordersService;


		public DateOnly DateParameter
		{
			set
			{
				Date = value;

				SetOtherProperties();
			}
		}

		public long AnnouncementIdParameter
		{
			set
			{
				Announcement = _announcementService.GetAnnouncementById(value);
				Appointment = _appointmentService.GetAppointmentByAnnouncmentId(value);
				Profile = Announcement.Profile;
				SetOtherProperties();
			}
		}



		[ObservableProperty]
		private DateOnly? _date = null;

		[ObservableProperty]
		private IEnumerable<DateOnly> _dates;

		[ObservableProperty]
		private AnnouncementInfo _announcement;

		[ObservableProperty]
		private Appointment _appointment;

		[ObservableProperty]
		private Profile _profile;

		[ObservableProperty]
		private IDictionary<Position, IEnumerable<TimeRange>> _ranges;

		[ObservableProperty]
		private Position _currentPosition;

		[ObservableProperty]
		private TimeSpan _begin;

		[ObservableProperty]
		private TimeSpan _end;

		[ObservableProperty]
		private bool _isValid;

		[ObservableProperty]
		private string _errorMessage;

		public ConfirmationViewModel(INavigationService navigationService, IFakeAnnouncementsService announcmentService, 
									IAppointmentService appointmentService, IRangesService rangesService, 
									IOrdersService ordersService)
		{
			_announcementService = announcmentService;
			_appointmentService = appointmentService;
			_navigationService = navigationService;
			_rangesService = rangesService;
			_ordersService = ordersService;
		}

		private void SetOtherProperties()
		{
			if(Appointment is not null && Date is not null)
			{
				Ranges = _rangesService.GetRangesForAppointmentAndDate(Appointment, (DateOnly)Date);

				List<DateOnly> dates = new();
				DateOnly today = DateOnly.FromDateTime(DateTime.Now);
				for (DateOnly it = today; it <= today.AddDays(Appointment.AccessibleDaysAfter); it = it.AddDays(1))
				{
					dates.Add(it);
				}

				Dates = dates;

				CurrentPosition = Ranges.First().Key;

				Begin = Appointment.OpenTime.ToTimeSpan();
				End = Appointment.OpenTime.Add(Appointment.MinimalTime).ToTimeSpan();
			}
		}

		public void Validate()
		{
			if (Begin >= End)
			{
				ErrorMessage = "Begin should be earlier than end";
				IsValid = false;
				return;
			}
			// On open - close
			if (Begin < Appointment.OpenTime.ToTimeSpan() || End > Appointment.CloseTime.ToTimeSpan())
			{
				ErrorMessage = "Object is closed on this time";
				IsValid = false;
				return;
			}

			// dooesn't intersects with other ranges

			foreach (var range in Ranges[CurrentPosition])
			{
				if (End > range.Begin.ToTimeSpan() && Begin < range.End.ToTimeSpan())     // true if intersects

				{
					ErrorMessage = "There is an appointment on this time";
					IsValid = false;
					return;
				}
			}

			IsValid = true;
		}

		[RelayCommand]
		void CreateOrder()
		{
			Validate();
			if(IsValid)
			{
				var range = new TimeRange() { Begin = TimeOnly.FromTimeSpan(Begin), End = TimeOnly.FromTimeSpan(End) };
				var order = _ordersService.CreateOrderAsync(Announcement, (DateOnly)Date, range).Result;

				var args = new
				{
					OrderId = order.Id
				};

				_navigationService.CurrentTabs.Navigation.Push<ConfirmationDoneViewModel>(args);
			}
		}

		partial void OnBeginChanged(TimeSpan value)
		{
			Validate();
		}

		partial void OnEndChanged(TimeSpan value)
		{
			Validate();
		}

		partial void OnCurrentPositionChanged(Position value)
		{
			if(value != null)
				Validate();
		}

		partial void OnDateChanged(DateOnly? value)
		{
			Validate();
		}
	}
}
