using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using LilaRent.MobileApp.Extensions;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Core.Builders;
using LilaRent.Domain.Fields;
using LilaRent.Requests.Services;
using LilaRent.Application.Dto;


namespace LilaRent.MobileApp.ViewModels;


[QueryProperty(nameof(AnnouncementDetails), nameof(AnnouncementDetails))]
public partial class MyAnnouncementViewModel : ObservableObject
{
	private readonly IAnnouncementService _announcementService;
	private readonly INavigationService _navigationService;

	public MyAnnouncementViewModel(IAnnouncementService annnouncementService, INavigationService navigationService)
	{
		_announcementService = annnouncementService;
		_navigationService = navigationService;

		AnnouncementDetails = null;
	}


	[ObservableProperty]
	private AnnouncementUpdatingDetailsDto? _announcementDetails;


	[RelayCommand]
	private void Share()
	{

	}

	[RelayCommand]
	private void Delete()
	{
		_navigationService.CurrentTabs.Navigation.Push<RemovingAnnouncementReasonViewModel>(new { AnnouncementDetails!.Id });
	}

	[RelayCommand]
	private void Promote()
	{

	}

	[RelayCommand]
	private void Update()
	{
		if (AnnouncementDetails is null)
			throw null;

		var images = AnnouncementDetails.Images
			.Select(image => new UriImageSource() { Uri = new Uri(image.Uri) })
			.ToList();

        var builder = new AnnouncementUpdatingDtoBuilder(images)
		{
			Id = AnnouncementDetails.Id,
			RentObjectName = AnnouncementDetails.RentObjectName,
			Address = AnnouncementDetails.Address,
			Description = AnnouncementDetails.Description,

			RentStart = AnnouncementDetails.OpenTime,
			RentEnd = AnnouncementDetails.CloseTime,
			Break = AnnouncementDetails.BreakBetweenReservations.Minutes,

			CanClientsChangeRecords = AnnouncementDetails.CanClientsChangeRecords,
			CanClientsDisableRecords = AnnouncementDetails.CanClientsDisableRecords,

			MinRentTime = AnnouncementDetails.MinReservationTime is null ? null : (int)AnnouncementDetails.MinReservationTime.Value.TotalMinutes / AnnouncementDetails.Price.TimeUnit.GetMinutesCount(),
			MaxRentTime = AnnouncementDetails.MaxReservationTime is null ? null : (int)AnnouncementDetails.MaxReservationTime.Value.TotalMinutes / AnnouncementDetails.Price.TimeUnit.GetMinutesCount(),
			
			RentTimeUnit = AnnouncementDetails.Price.TimeUnit,
            TimeUnitRentCost = AnnouncementDetails.Price.Value,
			IsDiscountUsed = AnnouncementDetails.UseDiscount,

            MinTimeForDiscount = AnnouncementDetails.MinTimeForDiscount is null ? null : (int)AnnouncementDetails.MinTimeForDiscount.Value.TotalMinutes,
			MaxTimeForDiscount = AnnouncementDetails.MaxTimeForDiscount is null ? null : (int)AnnouncementDetails.MaxTimeForDiscount.Value.TotalMinutes,
			DiscountTimeUnit = TimeUnit.Minutes,
			DiscountPercentage = AnnouncementDetails.DiscountPercentage
		};

		_navigationService.CurrentTabs.Navigation.Push<UpdateAnnouncementTargetViewModel>(new { Builder = builder });
	}
}
