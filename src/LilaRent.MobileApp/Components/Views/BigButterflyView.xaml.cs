

namespace LilaRent.MobileApp.Components.Views;


[ContentProperty(nameof(InnerContent))]
public partial class BigButterflyView : ContentView
{
    public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create(
        nameof(HeaderText),
        typeof(string),
        typeof(BigButterflyPage),
        defaultValue: string.Empty
    );

    public static readonly BindableProperty InnerContentProperty = BindableProperty.Create(
        nameof(InnerContent),
        typeof(object),
        typeof(BigButterflyPage),
        defaultValue: null,
        propertyChanged: InnerContentChanged
    );


    public BigButterflyView()
	{
		InitializeComponent();
	}


    public object? InnerContent
    {
        get => GetValue(InnerContentProperty);
        set => SetValue(InnerContentProperty, value);
    }

    public string HeaderText
    {
        get => (string)GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }


    private static void InnerContentChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var butterflyView = (BigButterflyView)bindable;

        if (newValue is not null and not View)
        {
            butterflyView.TextContent.Text = newValue.ToString();
            butterflyView.TextContent.IsVisible = true;

            butterflyView.ViewContent.Content = null;
            butterflyView.ViewContent.IsVisible = false;
        }
        else if (newValue is null)
        {
            butterflyView.TextContent.Text = string.Empty;
            butterflyView.TextContent.IsVisible = false;

            butterflyView.ViewContent.Content = null;
            butterflyView.ViewContent.IsVisible = false;
        }
        else
        {
            butterflyView.TextContent.Text = string.Empty;
            butterflyView.TextContent.IsVisible = false;

            butterflyView.ViewContent.Content = (View)newValue;
            butterflyView.ViewContent.IsVisible = true;
        }
    }
}
