using LilaRent.MobileApp.Entities;
using LilaRent.MobileApp.ViewModels;
using LilaRent.MobileApp.Core;
using Microsoft.Maui.Controls.Shapes;
using System.Runtime.CompilerServices;



namespace LilaRent.MobileApp.Views;


public partial class ConfirmationView : ContentPage
{
	public ConfirmationView()
	{
		InitializeComponent();
	}

	protected override void OnBindingContextChanged()
	{
		base.OnBindingContextChanged();

		//ConfirmationViewModel _vm = BindingContext as ConfirmationViewModel;

		//if(_vm is null)
		//	throw new ArgumentException($"Confirmation view Binding context should has type " +
		//	$"{typeof(ConfirmationViewModel)} instead of {BindingContext.GetType()}");


		//positionsPicker.ItemsSource = _vm.Ranges.Keys.ToList();
		//positionsPicker.SelectedIndex = 0;
	}

}