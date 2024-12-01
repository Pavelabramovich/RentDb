using System.Globalization;

namespace LilaRent.MobileApp.Resources.DataTemplates;


/// <summary>
/// DataTemplate created to display Announcement Basic Information
/// </summary>
public partial class DateDayTemplate : ContentView
{
	public DateDayTemplate()
	{
		InitializeComponent();
	}

	public DateDayTemplate(DateOnly date): this()
	{
		BindingContext = date;
	}
}