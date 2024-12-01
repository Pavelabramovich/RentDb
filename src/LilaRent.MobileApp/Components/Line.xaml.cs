namespace LilaRent.MobileApp.Components;

public partial class Line : ContentView
{
	public static readonly BindableProperty ColorProperty = BindableProperty.Create(
		nameof(Color), 
		typeof(Color), 
		typeof(Line), 
		defaultValue: Colors.Black
	);

	public Line()
	{
		InitializeComponent();
	}

	public Color Color
	{
		get => (Color)GetValue(ColorProperty); 
		set => SetValue(ColorProperty, value);
	}
}