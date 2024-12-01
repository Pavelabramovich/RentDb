using Microsoft.Maui.Controls.Shapes;
using System.Collections;


namespace LilaRent.MobileApp.Components;


public partial class TabbedViewPage : ContentPage
{
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(IList),
        typeof(TabbedView),
        defaultValue: TabbedView.ItemsSourceProperty.DefaultValue
    );

    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
        nameof(SelectedItem),
        typeof(object),
        typeof(TabbedView),
        defaultValue: TabbedView.SelectedItemProperty.DefaultValue
    );

    public static readonly BindableProperty TabTemplateProperty = BindableProperty.Create(
        nameof(TabTemplate),
        typeof(DataTemplate),
        typeof(TabbedView),
        defaultValue: TabbedView.TabTemplateProperty.DefaultValue
    );

    public static readonly BindableProperty TabViewTemplateProperty = BindableProperty.Create(
        nameof(TabViewTemplate),
        typeof(DataTemplate),
        typeof(TabbedView),
        defaultValue: TabbedView.TabViewTemplateProperty.DefaultValue
    );

    public static readonly BindableProperty IsSwipeEnabledProperty = BindableProperty.Create(
        nameof(IsSwipeEnabled),
        typeof(bool),
        typeof(TabbedView),
        defaultValue: TabbedView.IsSwipeEnabledProperty.DefaultValue
    );

    public static readonly BindableProperty TabsControlTemplateProperty = BindableProperty.Create(
        nameof(TabsControlTemplate),
        typeof(ControlTemplate),
        typeof(TabbedView),
        defaultValue: TabbedView.ControlTemplateProperty.DefaultValue
    );


    public TabbedViewPage()
	{
		InitializeComponent();
	}


    public IList ItemsSource
    {
        get => (IList)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public object SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    public DataTemplate TabTemplate
    {
        get => (DataTemplate)GetValue(TabTemplateProperty);
        set => SetValue(TabTemplateProperty, value);
    }

    public DataTemplate TabViewTemplate
    {
        get => (DataTemplate)GetValue(TabViewTemplateProperty);
        set => SetValue(TabViewTemplateProperty, value);
    }

    public bool IsSwipeEnabled
    {
        get => (bool)GetValue(IsSwipeEnabledProperty);
        set => SetValue(IsSwipeEnabledProperty, value);
    }

    public ControlTemplate TabsControlTemplate
    {
        get => (ControlTemplate)GetValue(TabsControlTemplateProperty);
        set => SetValue(TabsControlTemplateProperty, value);
    }
}