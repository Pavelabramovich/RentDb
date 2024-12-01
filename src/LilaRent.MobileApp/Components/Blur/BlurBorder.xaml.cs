
namespace LilaRent.MobileApp.Components;


[ContentProperty(nameof(InnerContent))]
public partial class BlurBorder : ContentView
{
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius),
        typeof(CornerRadius),
        typeof(BlurBorder),
        defaultValue: new CornerRadius(0)
    );

    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(
        nameof(BorderWidth),
        typeof(double),
        typeof(BlurBorder),
        defaultValue: 0.0
    );

    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
        nameof(BorderColor),
        typeof(Brush),
        typeof(BlurBorder),
        defaultValue: null
    );


    public static readonly BindableProperty BlurRadiusProperty = BindableProperty.Create(
        nameof(BlurRadius),
        typeof(double),
        typeof(BlurBorder),
        defaultValue: 0.0
    );

    public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(
        nameof(BlurBorder.BackgroundColor),
        typeof(Color),
        typeof(BlurBorder),
        defaultValue: null
    );


    public static readonly new BindableProperty PaddingProperty = BindableProperty.Create(
        nameof(BlurBorder.Padding),
        typeof(Thickness),
        typeof(BlurBorder),
        defaultValue: Thickness.Zero
    );


    public static readonly BindableProperty InnerContentProperty = BindableProperty.Create(
        nameof(InnerContent),
        typeof(View),
        typeof(BlurBorder),
        defaultValue: null
    );


    public BlurBorder()
    {
        InitializeComponent();
    }


    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    public Brush BorderColor
    {
        get => (Brush)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }


    public double BlurRadius
    {
        get => (double)GetValue(BlurRadiusProperty);
        set => SetValue(BlurRadiusProperty, value);
    }

    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }


    public new Thickness Padding
    {
        get => (Thickness)GetValue(PaddingProperty);
        set => SetValue(PaddingProperty, value);
    }


    public View? InnerContent
    {
        get => (View?)GetValue(InnerContentProperty);
        set => SetValue(InnerContentProperty, value);
    }
}
