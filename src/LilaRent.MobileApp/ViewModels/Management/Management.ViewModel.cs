using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.Application.Validation;
using LilaRent.MobileApp.Core.Validation;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.MobileApp.ViewModels;

public partial class ManagementViewModel : ObservableValidator
{
	private readonly IProfileService _profileService;
	private readonly IAppointmentService _appointmentService;
	private readonly IFakeAnnouncementsService _announcementsService;

	[ObservableProperty]
	IEnumerable<Position> _positions;

	[ObservableProperty]
	[Use<DaysCountOpenAheadValidator, int>(nameof(Days))]
	int _days = 7;

	[ObservableProperty]
	string _daysError;

	[ObservableProperty]
	bool _all = true;


	public ManagementViewModel(IProfileService profileService, IAppointmentService appointmentService, IFakeAnnouncementsService announcementsService)
	{
		_profileService = profileService;
		_appointmentService = appointmentService;
		_announcementsService = announcementsService;

		//long profileId = profileService.CurrentProfile.Id;


		////populate months
		//var months = new List<DateOnly>();
		//DateOnly it = DateOnly.FromDateTime(DateTime.Now);
		
		//// populate position
		//var announcement = _announcementsService.GetAnnouncements(ancmnt => ancmnt.Profile == _profileService.CurrentProfile).Result.First();
		//var appointment = _appointmentService.GetAppointmentByAnnouncmentId(announcement.Id);

		//Positions = appointment.Positions;
	}

	[RelayCommand]
	void SelectionChanged(IEnumerable selectedObjects)
	{
		var positions = selectedObjects.Cast<Position>();

		if(positions.Count() != 0
			&& positions.All(pos => pos.AccessibleDays == positions.First().AccessibleDays))
		{
			Days = positions.First().AccessibleDays;
		}
		else
		{
			Days = 1;
		}
	}

	partial void OnDaysChanged(int value)
	{
		DaysError = "";
		ValidateAllProperties();

		var errors = GetErrors(nameof(Days));
		if(errors.Any())
		{
			DaysError = errors.First().ErrorMessage!;
		}
	}

	[RelayCommand]
	void Save()
	{
		
	}
}
