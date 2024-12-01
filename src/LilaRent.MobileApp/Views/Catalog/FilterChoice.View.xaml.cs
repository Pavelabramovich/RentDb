using LilaRent.MobileApp.ViewModels;
using LilaRent.Domain.Fields;


namespace LilaRent.MobileApp.Views;


public partial class FilterChoiceView : ContentPage
{ 
	public FilterChoiceView()
	{
		InitializeComponent();

        CurrencyExpander.ItemsSource = new Currency[] { Currency.Byn, Currency.Usd };
        CurrencyExpander.SelectedItem = Currency.Byn;

        DurationExpander.ItemsSource = new TimeUnit[] { TimeUnit.Minutes, TimeUnit.Hours, TimeUnit.Days };
        DurationExpander.SelectedItem = TimeUnit.Minutes;
    }
}