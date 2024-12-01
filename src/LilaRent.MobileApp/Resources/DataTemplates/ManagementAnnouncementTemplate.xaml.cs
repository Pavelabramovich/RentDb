namespace LilaRent.MobileApp.Resources.DataTemplates;


public partial class ManagementPositionTemplate : ContentView
{
	public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(
		nameof(IsSelected),
		typeof(bool),
		typeof(ManagementPositionTemplate),
		defaultValue: false,
		propertyChanged: IsSelectedChanged
	);

	public event EventHandler<CheckedChangedEventArgs>? SelectedChanged;

	private static void IsSelectedChanged(BindableObject sender, object oldValue, object newValue)
	{
		var temp = sender as ManagementPositionTemplate;
		var args = new CheckedChangedEventArgs((bool)newValue);
		temp!.SelectedChanged?.Invoke(temp, args);		
	}

	public bool IsSelected
	{
		get => (bool)GetValue(IsSelectedProperty); 
		set => SetValue(IsSelectedProperty, value); 
	}



	public ManagementPositionTemplate()
	{
		InitializeComponent();
	}

}