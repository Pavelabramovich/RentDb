using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.ViewModels;
using LilaRent.MobileApp.Core;
using Microsoft.Maui.Controls.Shapes;
using System.Runtime.CompilerServices;



namespace LilaRent.MobileApp.Views;


public partial class AppointmentView : ContentPage
{
	enum RangeType
	{
		Occupied,
		Ok,
		Error
	}

	public bool scrollOccupied = false;


	//public static readonly Color ErrorColor = new Color(240, 128, 128);
	//public static readonly Color OkColor = new Color(212, 158, 239);
	//public static readonly Color OccupiedColor = Colors.Grey;

	public string day_dayOfWeek = "{0} {1}";
	private int columnWidth
	{
		get
		{
			if (_vm is null)
				return 80;

			int positionCount = _vm.Appointment.Positions.Count();
			if (positionCount == 0)
				return 0;
			else if (positionCount == 1)
				return 200;
			else if (positionCount == 2)
				return 110;
			else
				return 80;

		}
	}

	const int _horizontalMargin = 15;

	private Border? _choice;

	private double _lineX = 0;
	private double _lineY = 0;

	private List<Border> _rangesBorders = new();

	int heightOfHour = 120;

	AppointmentViewModel? _vm = null;

	public AppointmentView()
	{
		InitializeComponent();
		_vm = null;
		ParentGrid.SizeChanged += ParentGrid_SizeChanged;
		headerScroll.Scrolled += HeaderScroll_Scrolled;
		timeScroll.Scrolled += TimeScroll_Scrolled;
		lineScroll.Scrolled += LineScroll_Scrolled;
	}

	//private void LineScroll_Scrolled(object? sender, ScrolledEventArgs e)
	//{
	//	Console.WriteLine($"Event: {e.ScrollX}, {e.ScrollY}      Obj: {lineScroll.ScrollX}, {lineScroll.ScrollY}");
	//}

	private async void LineScroll_Scrolled(object? sender, ScrolledEventArgs e)
	{
		if (e.ScrollX != 0)
			_lineX = e.ScrollX;
		if(e.ScrollY != 0)
			_lineY = e.ScrollY;
		
		List<Task> tasks = new List<Task>();
		if (lineScroll.ScrollY != 0)
			tasks.Add(timeScroll.ScrollToAsync(0, lineScroll.ScrollY, false));

		if (lineScroll.ScrollX != 0)
			tasks.Add(headerScroll.ScrollToAsync(lineScroll.ScrollX, 0, false));

		Task.WaitAll(tasks.ToArray());
	}

	private async void TimeScroll_Scrolled(object? sender, ScrolledEventArgs e)
	{
		//if (scrollOccupied)
		//	return;

		//scrollOccupied = true;

		if (timeScroll.ScrollY != 0)
			await lineScroll.ScrollToAsync(_lineX, timeScroll.ScrollY, false);

		//scrollOccupied = false;
	}

	private async void HeaderScroll_Scrolled(object? sender, ScrolledEventArgs e)
	{
		//if (scrollOccupied)
		//	return;

		//scrollOccupied = true;

		if (headerScroll.ScrollX != 0)
		{
			if(lineScroll.ScrollY == 0)
			{
				//Debug.Text = $"evargs: X: {e.ScrollX}, Y: {e.ScrollY}. " +
				//$"LineState: X: {lineScroll.ScrollX} Y: {lineScroll.ScrollY}. Header state: X: {headerScroll.ScrollX} Y: {headerScroll.ScrollY}.";
			}
			await lineScroll.ScrollToAsync(headerScroll.ScrollX, _lineY, false);
		}

		//scrollOccupied = false;
	}

	private void ParentGrid_SizeChanged(object? sender, EventArgs e)
	{
		
	}

	private void correctSizes()
	{
		int count = _vm.Appointment.Positions.Count();
		if (count > 3)
		{	
			int width = (2 * _horizontalMargin + columnWidth) * count;
			lineGrid.WidthRequest = width;
			headerGrid.WidthRequest = width;
		}
		else
		{
			//lineGrid.WidthRequest = ParentGrid.Width - 50;
			//headerGrid.WidthRequest = ParentGrid.Width - 50;
			var binding1 = new Binding() { Source = ParentGrid, Path = nameof(Grid.Width)};
			var binding2 = new Binding() { Source = ParentGrid, Path = nameof(Grid.Width)};
			lineGrid.SetBinding(WidthRequestProperty, binding1);
			headerGrid.SetBinding(WidthRequestProperty, binding2);
		}

		// to invoke resize
		//lineGrid.Layout(lineGrid.Bounds);
		//headerGrid.Layout(headerGrid.Bounds);
	}

	protected override void OnPropertyChanging([CallerMemberName] string? propertyName = null)
	{
		if (propertyName == nameof(BindingContext))
			OnBindingContextChanging();

		base.OnPropertyChanging(propertyName);
	}

	private void OnBindingContextChanging()
	{
		if (_vm is null)
			return;

		_vm.PropertyChanged -= vm_SelectedRangeChanged;
		_vm.PropertyChanged -= vm_RangesChanged;
	}

	/// <summary>
	///		Paints Border for giver range
	/// </summary>
	/// <param name="range"></param>
	/// <param name="color"></param>
	/// <returns>Created border</returns>
	private Border PaintRange(TimeRange range, RangeType rangeType, int column = 0)
	{
		int stepsInHour = 60 / _vm.Appointment.Gap.Minutes;
		int heightOfStep = heightOfHour / stepsInHour;
		int minutesInStep = _vm.Appointment.Gap.Hours * 60 + _vm.Appointment.Gap.Minutes;
		TimeSpan tao = range.Begin - _vm.Appointment.OpenTime;   // time after opening
		int minutesAfterOpen = tao.Hours * 60 + tao.Minutes;
		int index = minutesAfterOpen / minutesInStep;

		int rowCount = (range.Span().Hours * 60 + range.Span().Minutes) / minutesInStep + 1;

		var border = new Border()
		{
			Margin = new Thickness(_horizontalMargin, heightOfStep / 2),
			WidthRequest = columnWidth,
			HorizontalOptions = LayoutOptions.Center,
			Style = (Style)Microsoft.Maui.Controls.Application.Current.Resources["MediumAccentBorderStyle"]
		};

		lineGrid.Add(border, column: column, row: index);
		Grid.SetRowSpan(border, rowCount);

		return border;
	}

	private void PaintRanges()
	{
		int i = 0;
		foreach (var pos in _vm.Appointment.Positions)
		{
			foreach (var range in _vm.Ranges[pos])
			{
				_rangesBorders.Add(PaintRange(range, RangeType.Occupied, i));
			}

			++i;
		}
	}

	private void ClearRanges()
	{
		foreach (Border border in _rangesBorders)
		{
			lineGrid.Remove(border);
		}

		_rangesBorders.Clear();
	}

	private void PaintChoice()
	{
		if (_vm.Begin is null || _vm.End is null)
			return;

		if (_vm.Begin >= _vm.End)
			return;

		if (_vm.Begin < _vm.Appointment.OpenTime || _vm.End > _vm.Appointment.CloseTime)
			return;

		TimeRange range = new TimeRange() { Begin = _vm.Begin.Value, End = _vm.End.Value };
		// vm is paintable? (not if out of bounds or begin > end)
		// if not return

		// vm.IsOkay?
		// if not
		if (_vm.IsValid)
		{
			_choice = PaintRange(range, RangeType.Ok);
		}
		else
		{
			_choice = PaintRange(range, RangeType.Error);
		}
	}

	private void ClearChoice()
	{
		if (_choice is not null)
		{
			lineGrid.Remove(_choice);
			_choice = null;
		}
	}

	private void PaintGrid()
	{
		foreach (var pos in _vm.Appointment.Positions)
		{
			lineGrid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(1, GridUnitType.Star)));
		}

		var time = _vm.Appointment.OpenTime;
		int stepsInHour = 60 / _vm.Appointment.Gap.Minutes;
		int heightOfStep = heightOfHour / stepsInHour;

		int columnCount = _vm.Appointment.Positions.Count();

		int i = 0;
		while (time <= _vm.Appointment.CloseTime)
		{
			lineGrid.RowDefinitions.Add(new RowDefinition(heightOfStep));
			timeGrid.RowDefinitions.Add(new RowDefinition(heightOfStep));

			// add elements with index. if its start or end of hour -> add line

			var line = new  Components.Line();

			if (time.Minute == 0)
			{
				line.Style = (Style)Microsoft.Maui.Controls.Application.Current.Resources["OutlineLineStyle"];
			}
			else
			{
				line.Style = (Style)Microsoft.Maui.Controls.Application.Current.Resources["AccentLineStyle"];
			}

			lineGrid.Add(line, row: i, column: 0);
			Grid.SetColumnSpan(line, columnCount);
			timeGrid.Add(new Label() { Text = time.ToShortTimeString(), FontSize = 14, VerticalOptions = LayoutOptions.Center }, row: i);

			time = time.Add(_vm.Appointment.Granularity);
			++i;
		}
	}

	private void PaintHeader()
	{
		foreach (var pos in _vm.Appointment.Positions)
		{
			headerGrid.ColumnDefinitions.Add(new ColumnDefinition(new GridLength(1, GridUnitType.Star)));
		}

		var columnsCount = _vm.Appointment.Positions.Count();

		for (int i = 0; i < columnsCount; ++i)
		{
			Position currentPosition = _vm.Appointment.Positions.Skip(i).First();
			Grid microGrid = new Grid() 
			{
				HorizontalOptions = LayoutOptions.Center, 
				WidthRequest = columnWidth, 
				Margin = new Thickness(_horizontalMargin, 0) 
			};

			microGrid.RowDefinitions.Add(new RowDefinition(new GridLength(1, GridUnitType.Star)));
			microGrid.RowDefinitions.Add(new RowDefinition(new GridLength(30)));

			Border imageWrap = new Border()
			{
				Style = (Style)Microsoft.Maui.Controls.Application.Current.Resources["SmallOutlinelessBorderStyle"],
				//HeightRequest = columnWidth
			};

			Image image = new Image() 
			{ 
				Source = currentPosition.ImagePath, 
				Style = (Style)Microsoft.Maui.Controls.Application.Current.Resources["PositionImage"],
				HeightRequest = columnWidth
			};

			imageWrap.Content = image;

			Label label = new Label() 
			{
				Text = currentPosition.Title,
				Style = (Style)Microsoft.Maui.Controls.Application.Current.Resources["SignDarkStyle"]
			};

			microGrid.Add(imageWrap, row: 0);
			microGrid.Add(label, row: 1);
			microGrid.Margin = new Thickness(2, 0);

			headerGrid.Add(microGrid, column: i);
		}
	}

	protected override void OnBindingContextChanged()
	{
		base.OnBindingContextChanged();
		lineGrid.Clear();
		lineGrid.RowDefinitions.Clear();
		lineGrid.ColumnDefinitions.Clear();

		headerGrid.Clear();
		headerGrid.ColumnDefinitions.Clear();

		timeGrid.Clear();
		timeGrid.RowDefinitions.Clear();

		_rangesBorders.Clear();

		_vm = BindingContext as AppointmentViewModel;

		if (_vm is null)
			throw new ArgumentException($"Appointment view Binding context should has type " +
			$"{typeof(AppointmentViewModel)} instead of {BindingContext.GetType()}");

		correctSizes();

		_vm.PropertyChanged += vm_SelectedRangeChanged;
		_vm.PropertyChanged += vm_RangesChanged;

		var nowItem = datesCv.ItemsSource.Cast<MyDateOnly>().Where(mdo => mdo.DateOnly == DateOnly.FromDateTime(DateTime.Now)).First();
		datesCv.SelectedItem = nowItem;

		datesCv.SelectionChangedCommand = _vm.SetDateCommand;
		var binding = new Binding() { Source = datesCv, Path = nameof(CollectionView.SelectedItem) };
		datesCv.SetBinding(CollectionView.SelectionChangedCommandParameterProperty, binding);

		PaintGrid();
		PaintHeader();
		PaintRanges();
	}

	private void vm_RangesChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		if (e.PropertyName != nameof(AppointmentViewModel.Ranges))
			return;

		ClearRanges();
		ClearChoice();

		PaintRanges();
		PaintChoice();
	}

	private void vm_SelectedRangeChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		if (e.PropertyName != nameof(AppointmentViewModel.Begin) && e.PropertyName != nameof(AppointmentViewModel.End))
			return;

		var vm = sender as AppointmentViewModel;
		if (vm is null)
			throw new ArgumentException($"Appointment view Binding context should has type " +
			$"{typeof(AppointmentViewModel)} instead of {BindingContext.GetType()}");

		ClearChoice();
		PaintChoice();
	}
}