using LilaRent.Domain.Fields;


namespace LilaRent.MobileApp.Components;


public partial class PasswordStrengthBar : ContentView
{
    public static readonly BindableProperty PasswordStrengthProperty = BindableProperty.Create(
        nameof(PasswordStrength),
        typeof(PasswordStrength?),
        typeof(PasswordStrengthBar),
        defaultValue: null
    );

    public static readonly BindableProperty WeakColorProperty = BindableProperty.Create(
        nameof(WeakColor),
        typeof(Color),
        typeof(PasswordStrengthBar),
        defaultValue: Color.Parse("Red")
    );

    public static readonly BindableProperty MediumColorProperty = BindableProperty.Create(
        nameof(MediumColor),
        typeof(Color),
        typeof(PasswordStrengthBar),
        defaultValue: Color.Parse("Yellow")
    );

    public static readonly BindableProperty StrongColorProperty = BindableProperty.Create(
        nameof(StrongColor),
        typeof(Color),
        typeof(PasswordStrengthBar),
        defaultValue: Color.Parse("Green")
    );


    public PasswordStrengthBar()
	{
		InitializeComponent();
	}


    public PasswordStrength? PasswordStrength
    {
        get => (PasswordStrength?)GetValue(PasswordStrengthProperty);
        set => SetValue(PasswordStrengthProperty, value);
    }

    public Color WeakColor
    {
        get => (Color)GetValue(WeakColorProperty);
        set => SetValue(WeakColorProperty, value);
    }

    public Color MediumColor
    {
        get => (Color)GetValue(MediumColorProperty);
        set => SetValue(MediumColorProperty, value);
    }

    public Color StrongColor
    {
        get => (Color)GetValue(StrongColorProperty);
        set => SetValue(StrongColorProperty, value);
    }
}