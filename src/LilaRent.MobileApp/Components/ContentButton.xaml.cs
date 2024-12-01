using System.Windows.Input;


namespace LilaRent.MobileApp.Components;


[ContentProperty(nameof(InnerContent))]
public partial class ContentButton : ContentView
{
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
       nameof(CornerRadius),
       typeof(int),
       typeof(ContentButton),
       defaultValue: 0
   );

    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(
        nameof(BorderWidth),
        typeof(double),
        typeof(ContentButton),
        defaultValue: 0.0
    );

    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
        nameof(BorderColor),
        typeof(Color),
        typeof(ContentButton),
        defaultValue: null
    );


    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(ContentButton),
        defaultValue: null
    );

    public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(
        nameof(BackgroundColor),
        typeof(Color),
        typeof(ContentButton),
        defaultValue: null
    );

    public static readonly new BindableProperty PaddingProperty = BindableProperty.Create(
        nameof(ContentButton.Padding),
        typeof(Thickness),
        typeof(ContentButton),
        defaultValue: Thickness.Zero
    );

    public static readonly BindableProperty InnerContentProperty = BindableProperty.Create(
        nameof(InnerContent),
        typeof(View),
        typeof(ContentButton),
        defaultValue: null
    );


    public event EventHandler Clicked
    {
        add => Button.Clicked += value;
        remove => Button.Clicked -= value;
    }


    public ContentButton()
	{
		InitializeComponent();
	}


    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public View? InnerContent
    {
        get => (View?)GetValue(InnerContentProperty);
        set => SetValue(InnerContentProperty, value);
    }

    public int CornerRadius
    {
        get => (int)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    public new Thickness Padding
    {
        get => (Thickness)GetValue(PaddingProperty);
        set => SetValue(PaddingProperty, value);
    }
}