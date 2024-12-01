
using LilaRent.MobileApp.Resources.DataTemplates;
using LilaRent.MobileApp.ViewModels;

namespace LilaRent.MobileApp.Views;


public partial class ManagementCalendarView : ContentPage
{
	private ManagementCalendarViewModel _vm = null;
	private double _positionImageWidth = 78;
	private double _datesHeight = 54;
	private double _cellWidth = 94;
	private double _cellHeight = 60;
	// private int _dateWidth = 47;
	private double _datesPadding;
	private double _positionsPadding;

	private double _scrollX = 0;
	private double _scrollY = 0;

	public ManagementCalendarView()
	{
		InitializeComponent();

		_positionsPadding = (_cellWidth - _positionImageWidth) / 2;
		_datesPadding = (_cellHeight - _datesHeight) / 2;
	
		DatesScroll.HeightRequest = _cellHeight * 9;
		TimeRangesScroll.HeightRequest = _cellHeight * 9;

		DatesScroll.Scrolled += DatesScroll_Scrolled;
		TimeRangesScroll.Scrolled += TimeRangesScroll_Scrolled;
		PositionsScroll.Scrolled += PositionsScroll_Scrolled;
	}

	private async void PositionsScroll_Scrolled(object? sender, ScrolledEventArgs e)
	{
		if (PositionsScroll.ScrollX != 0)
			await TimeRangesScroll.ScrollToAsync(PositionsScroll.ScrollX, _scrollY, false);
	}

	private void TimeRangesScroll_Scrolled(object? sender, ScrolledEventArgs e)
	{
		if (e.ScrollX != 0)
			_scrollX = e.ScrollX;
		if (e.ScrollY != 0)
			_scrollY = e.ScrollY;

		List<Task> tasks = new List<Task>();
		if (TimeRangesScroll.ScrollY != 0)
			tasks.Add(DatesScroll.ScrollToAsync(0, TimeRangesScroll.ScrollY, false));

		if (TimeRangesScroll.ScrollX != 0)
			tasks.Add(PositionsScroll.ScrollToAsync(TimeRangesScroll.ScrollX, 0, false));

		Task.WaitAll(tasks.ToArray());
	}

	private async void DatesScroll_Scrolled(object? sender, ScrolledEventArgs e)
	{
		if (DatesScroll.ScrollY != 0)
			await TimeRangesScroll.ScrollToAsync(_scrollX, DatesScroll.ScrollY, false);
	}

	protected override void OnBindingContextChanged()
	{
		base.OnBindingContextChanged();

		if (_vm is not null)
		{
			ClearTimeRanges();
			ClearDates();

			MonthsCv.SelectionChanged -= MonthsCv_SelectionChanged;
		}

		_vm = BindingContext as ManagementCalendarViewModel;

		if (_vm is null)
			throw new ArgumentException($"Appointment view Binding context should has type " +
			$"{typeof(AppointmentViewModel)} instead of {BindingContext.GetType()}");

		MonthsCv.SelectedItem = MonthsCv.ItemsSource.Cast<DateOnly>().FirstOrDefault();
		MonthsCv.SelectionChanged += MonthsCv_SelectionChanged;

		PrintPositions();
		PrintDates();
		PrintTimeRanges();
	}

	private void MonthsCv_SelectionChanged(object? sender, SelectionChangedEventArgs e)
	{
		ClearDates();
		ClearTimeRanges();

		PrintDates();
		PrintTimeRanges();
	}

	private void PrintPositions()
	{
		var positions = _vm.Table.Keys;
		int column = 0;

		foreach(var position in positions)
		{
			PositionsGrid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(_cellWidth + TimeRangesGrid.ColumnSpacing)));

			Image image = new Image();
			image.Source = position.ImagePath;
			image.Style = (Style)Microsoft.Maui.Controls.Application.Current.Resources["PositionImage"];
			image.VerticalOptions = LayoutOptions.Center;
			image.HorizontalOptions = LayoutOptions.Center;

			Border imageWrap = new Border()
			{
				Style = (Style)Microsoft.Maui.Controls.Application.Current.Resources["SmallOutlinelessBorderStyle"],
				Content = image,
				Margin = new Thickness(_positionsPadding, 0),
				WidthRequest = _positionImageWidth,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center
			};

			PositionsGrid.Add(imageWrap, column: column);
			++column;
		}
	}

	private void PrintDates()
	{
		DateOnly it = (DateOnly)MonthsCv.SelectedItem;
		int month = it.Month;

		int row = 0;
		while(it.Month == month)
		{
			DatesGrid.RowDefinitions.Add(new RowDefinition(new GridLength(_cellHeight + TimeRangesGrid.RowSpacing)));	

			DateDayTemplate card = new DateDayTemplate(it);
			card.HeightRequest = _datesHeight;

			card.VerticalOptions = LayoutOptions.Center;

			DatesGrid.Add(card, row: row);

			++row;
			it = it.AddDays(1);
		}
	}

	private void PrintTimeRanges()
	{
		int column = 0;
		DateOnly currentMonth = (DateOnly)MonthsCv.SelectedItem;

		foreach (var position in _vm.Table.Keys)
		{
			int row = 0;
			TimeRangesGrid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(_cellWidth)));
			DateOnly it = currentMonth;
			while(it.Month == currentMonth.Month)
			{
				var value = _vm.Table[position][it];
				if (column == 0)
					TimeRangesGrid.RowDefinitions.Add(new RowDefinition(new GridLength(_cellHeight)));

				var border = new Border()
				{
					Style = (Style)Microsoft.Maui.Controls.Application.Current.Resources["ManagementCellBorder"],
					WidthRequest = _cellWidth,
					HeightRequest = _cellHeight,
				};

				TimeRangesGrid.Add(border, column: column, row: row);

				it = it.AddDays(1);
				++row;

				if (value.Begin == _vm.Appointment.OpenTime && value.End == _vm.Appointment.CloseTime && Random.Shared.Next() % 3 == 0)
					continue;

				VerticalStackLayout vs = new VerticalStackLayout();

				vs.VerticalOptions = LayoutOptions.Center;

				Label open = new Label();
				open.Text = $"{value.Begin: HH:mm}";
				open.HorizontalOptions = LayoutOptions.Center;
				open.VerticalOptions = LayoutOptions.Center;

				Label close = new Label();
				close.Text = $"{value.End: HH:mm}";
				close.HorizontalOptions = LayoutOptions.Center;
				close.VerticalOptions = LayoutOptions.Center;

				vs.Add(open);
				vs.Add(close);

				border.Content = vs;
			}

			++column;
		}
	}

	private void ClearDates()
	{
		DatesGrid.Clear();
		DatesGrid.RowDefinitions.Clear();
	}

	private void ClearTimeRanges()
	{
		TimeRangesGrid.Clear();
		TimeRangesGrid.RowDefinitions.Clear();
		TimeRangesGrid.ColumnDefinitions.Clear();
	}
}