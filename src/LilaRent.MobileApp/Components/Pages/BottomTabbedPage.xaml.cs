using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.ViewModels;
using Microsoft.Maui.Platform;
using System.Reflection;


namespace LilaRent.MobileApp.Components;


public partial class BottomTabbedPage : TabbedPage
{
    public static readonly BindableProperty SelectedIconImageSourceProperty = BindableProperty.CreateAttached(
        nameof(GetSelectedIconImageSource).Replace("Get", string.Empty),
        typeof(ImageSource),
        typeof(Page),
        defaultValue: null
    );

    public static readonly BindableProperty UnselectedIconImageSourceProperty = BindableProperty.CreateAttached(
        nameof(GetUnselectedIconImageSource).Replace("Get", string.Empty),
        typeof(ImageSource),
        typeof(Page),
        defaultValue: null
    );


    private Page? _oldCurrentPage;


    public BottomTabbedPage()
    {
        InitializeComponent();

        ChildAdded += TabbedView_OnChildAdded;
        CurrentPageChanged += MainTabbedView_CurrentPageChanged;
    }

    private void TabbedView_OnChildAdded(object? sender, ElementEventArgs args)
    {
        var newPage = (Page)args.Element;

        if (GetSelectedIconImageSource(newPage) is not null
            && GetUnselectedIconImageSource(newPage) is ImageSource newImageSource)
        {
            newPage.IconImageSource = newImageSource;
        }
    }

    private void MainTabbedView_CurrentPageChanged(object? sender, EventArgs e)
    {
        if (GetSelectedIconImageSource(CurrentPage) is ImageSource newImageSource
            && GetUnselectedIconImageSource(CurrentPage) is not null)
        {
            CurrentPage.IconImageSource = newImageSource;
        }

        if (_oldCurrentPage is not null
            && GetSelectedIconImageSource(_oldCurrentPage) is not null
            && GetUnselectedIconImageSource(_oldCurrentPage) is ImageSource oldImageSource)
        {
            _oldCurrentPage.IconImageSource = oldImageSource;
        }

        _oldCurrentPage = CurrentPage;
    }


    public static ImageSource GetSelectedIconImageSource(BindableObject bindable)
    {
        return (ImageSource)bindable.GetValue(SelectedIconImageSourceProperty);
    }

    public static void SetSelectedIconImageSource(BindableObject bindable, ImageSource value)
    {
        bindable.SetValue(SelectedIconImageSourceProperty, value);
    }


    public static ImageSource GetUnselectedIconImageSource(BindableObject bindable)
    {
        return (ImageSource)bindable.GetValue(UnselectedIconImageSourceProperty);
    }

    public static void SetUnselectedIconImageSource(BindableObject bindable, ImageSource value)
    {
        bindable.SetValue(UnselectedIconImageSourceProperty, value);
    }
}