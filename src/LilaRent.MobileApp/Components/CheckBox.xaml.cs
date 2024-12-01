namespace LilaRent.MobileApp.Components;


public partial class CheckBox : ContentView
{
	public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
		nameof(IsChecked),
		typeof(bool), 
		typeof(CheckBox),
		defaultValue: false,
		propertyChanged: IsCheckedChanged
	);

    public event EventHandler<CheckedChangedEventArgs>? CheckedChanged;


    public CheckBox()
	{
		InitializeComponent();
	}


	public bool IsChecked
	{
		get => (bool)GetValue(IsCheckedProperty);
		set => SetValue(IsCheckedProperty, value);
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		IsChecked = !IsChecked;
    }

    private static void IsCheckedChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var checkBox = (CheckBox)bindable;
		var newIsChecked = (bool)newValue;

		checkBox.CheckedChanged?.Invoke(checkBox, new(newIsChecked));
    }
}