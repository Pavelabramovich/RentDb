using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.Extensions;
using LilaRent.Domain.Fields;


namespace LilaRent.MobileApp.ViewModels;


public enum Currency
{
	Byn,
	Usd
}


public partial class FilterChoiceViewModel: ObservableObject
{
	private readonly INavigationService _navigationService;


	[ObservableProperty]
	private string? _town;

	[ObservableProperty]
	// [Optional<PriceValidator, decimal>]
	private string? _minPrice;

	[ObservableProperty]
    // [Optional<PriceValidator, decimal>]
    private string? _maxPrice;

	[ObservableProperty]
	private Currency _currency;

	[ObservableProperty]
	private string? _minTime;

	[ObservableProperty]
	private string? _maxTime;

	[ObservableProperty]
	private TimeUnit _timeStep;


	public FilterChoiceViewModel(INavigationService navigationService)
	{
		_navigationService = navigationService;

		TimeStep = TimeUnit.Minutes;
	}


	[RelayCommand]
	void ClearFilters()
	{
		Town = null;
		MinPrice = null;
		MaxPrice = null;
		Currency = Currency.Byn;
		MinTime = null;
		MaxTime = null;
		TimeStep = TimeUnit.Minutes;

		_navigationService.CurrentTabs.Navigation.Pop(new { Filters = (Func<AnnouncementInfo, bool>?)null });
	}

	[RelayCommand]
	void UseFilters()
	{
		List<Func<AnnouncementInfo, bool>> filters = [];

		if (!string.IsNullOrEmpty(Town))
			filters.Add(a => a.Address.Contains(Town, StringComparison.OrdinalIgnoreCase));
		
		if (MinPrice is not null)
            filters.Add(a => a.Price >= Convert.ToDecimal(MinPrice));

		if (MaxPrice is not null)
            filters.Add(a => a.Price <= Convert.ToDecimal(MaxPrice));

		// And something with time...


		Func<AnnouncementInfo, bool>? filter;

		if (filters.Count == 0)
		{
			filter = null;
		}
		else
		{
            filter = a => filters.All(f => f(a));
		}

        _navigationService.CurrentTabs.Navigation.Pop(new { Filters = filter });
	}
}
