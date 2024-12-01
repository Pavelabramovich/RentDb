using LilaRent.MobileApp.Components.Views;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using static System.Net.Mime.MediaTypeNames;


namespace LilaRent.MobileApp.Components;


[ContentProperty(nameof(InnerContent))]
public partial class BigButterflyPage : ContentPage
{
    public static readonly BindableProperty InnerContentProperty = BindableProperty.Create(
        nameof(InnerContent),
        typeof(object),
        typeof(BigButterflyPage),
        defaultValue: null
    );

    public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create(
        nameof(HeaderText),
        typeof(string),
        typeof(BigButterflyPage),
        defaultValue: string.Empty
    );


    public BigButterflyPage()
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

    public VerticalStackLayout Buttons => ButtonsLayout;
}
