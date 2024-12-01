using LilaRent.MobileApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilaRent.MobileApp.Services;

public class AppointmentListService : IAppointmentService
{
	IEnumerable<Appointment> _list = new Appointment[]
	{
		new Appointment()
		{
			AnnouncmentId = 0,
			OpenTime = new TimeOnly(9, 0),
			CloseTime = new TimeOnly(20, 0),
			MinimalTime = new TimeSpan(1, 0, 0),
			MaximalTime = new TimeSpan(4, 0, 0),
			Granularity = new TimeSpan(0, 15, 0),
			Gap = new TimeSpan(0, 15, 0),
			Positions = new[] 
			{
				new Position() { Title = "Stupid Position"},
				new Position() { Title = "Another stupid position"},
				new Position() { Title = "Another one"},
				new Position() { Title = "And Another one"},
				new Position() {Title = "One more position"}
			},
		},

		new Appointment()
		{
			AnnouncmentId = 0,
			OpenTime = new TimeOnly(9, 0),
			CloseTime = new TimeOnly(20, 0),
			MinimalTime = new TimeSpan(1, 0, 0),
			MaximalTime = new TimeSpan(4, 0, 0),
			Granularity = new TimeSpan(0, 15, 0),
			Gap = new TimeSpan(0, 15, 0),
			Positions = new[]
			{
				new Position() {Title = "Столик" },
				new Position() {Title = "Кресло"}
			},
		},
		new Appointment()
		{
			AnnouncmentId = 0,
			OpenTime = new TimeOnly(9, 0),
			CloseTime = new TimeOnly(20, 0),
			MinimalTime = new TimeSpan(1, 0, 0),
			MaximalTime = new TimeSpan(4, 0, 0),
			Granularity = new TimeSpan(0, 15, 0),
			Gap = new TimeSpan(0, 15, 0),
			Positions = new[]
			{
				new Position() {Title = "Столик" },
				new Position() {Title = "Кресло"},
				new Position() {Title = "Стул"}
			},
		},
		new Appointment()
		{
			AnnouncmentId = 0,
			OpenTime = new TimeOnly(9, 0),
			CloseTime = new TimeOnly(20, 0),
			MinimalTime = new TimeSpan(1, 0, 0),
			MaximalTime = new TimeSpan(4, 0, 0),
			Granularity = new TimeSpan(0, 30, 0),
			Gap = new TimeSpan(0, 15, 0),
			Positions = new[]
			{
				new Position() {Title = "Столик" },
				new Position() {Title = "Кресло"},
				new Position() {Title = "Стул"},
				new Position() {Title = "Кресло"},
				new Position() {Title = "Стул"},
			},
		}
	};

	public Appointment GetAppointmentByAnnouncmentId(long id)
	{
		return _list.Skip((int)id % 4).First();
	}
}
