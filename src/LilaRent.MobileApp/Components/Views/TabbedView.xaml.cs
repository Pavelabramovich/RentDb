using LilaRent.MobileApp.Components.Core;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.ViewModels;
using Microsoft.Maui.Controls.Shapes;
using System.Collections;
using System.Runtime.CompilerServices;


namespace LilaRent.MobileApp.Components;


public partial class TabbedView : ContentView
{
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
        nameof(ItemsSource),
        typeof(IList),
        typeof(TabbedView),
        defaultValue: Array.Empty<object>()
    );

    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
        nameof(SelectedItem),
        typeof(object),
        typeof(TabbedView),
        defaultValue: null,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: SelectedItemChanged
    );

    public static readonly BindableProperty TabTemplateProperty = BindableProperty.Create(
        nameof(TabTemplate),
        typeof(DataTemplate),
        typeof(TabbedView),
        defaultValue: App.Current.Resources.MergedDictionaries.ToArray()[2]["DefaultTabTemplate"]
    );

    public static readonly BindableProperty TabViewTemplateProperty = BindableProperty.Create(
        nameof(TabViewTemplate),
        typeof(DataTemplate),
        typeof(TabbedView),
        defaultValue: null
    );

    public static readonly new BindableProperty ControlTemplateProperty = BindableProperty.Create(
        nameof(ControlTemplate),
        typeof(ControlTemplate),
        typeof(TabbedView), 
        defaultValue: App.Current.Resources.MergedDictionaries.ToArray()[2]["DefaultTabsControlTemplate"]
    );

    public static readonly BindableProperty IsSwipeEnabledProperty = BindableProperty.Create(
        nameof(IsSwipeEnabled),
        typeof(bool),
        typeof(TabbedView),
        defaultValue: true
    );


    private static void SelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var tabsView = (TabbedView)bindable;

        if (tabsView._tabs is null)
            return;

        if (tabsView._tabs is not null)
            tabsView._tabs.SelectedItem = newValue;
        
        tabsView.ViewsCarouselView.CurrentItem = newValue;
    }

    private CollectionPresenter? _tabs;
    private bool _isDragging;


    public TabbedView()
    {
        InitializeComponent();

        SetBinding(TemplatedView.ControlTemplateProperty, new Binding() { Path = ControlTemplateProperty.PropertyName, Source = this });

        _isDragging = false;
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

    public new ControlTemplate ControlTemplate
    {
        get => (ControlTemplate)GetValue(ControlTemplateProperty);
        set => SetValue(ControlTemplateProperty, value);
    }


    public bool IsSwipeEnabled
    {
        get => (bool)GetValue(IsSwipeEnabledProperty);
        set => SetValue(IsSwipeEnabledProperty, value);
    }


    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        if (args.CurrentSelection[0] is not object currentViewModel)
            return;

        ViewsCarouselView.CurrentItem = currentViewModel;
        SelectedItem = currentViewModel;
    }

   
    void CarouselView_CurrentItemChanged(object sender, CurrentItemChangedEventArgs args)
    {
        if (args.CurrentItem is not object currentViewModel || !_isDragging)
            return;

        int index = ItemsSource.IndexOf(currentViewModel);

        if (_tabs is not null)
        {
            _tabs.SelectedItem = currentViewModel;
            _tabs.ScrollTo(index);
        }

        SelectedItem = currentViewModel;

        _isDragging = false;
    }

    // There is a complicated situation with swiping. Due to the fact that during
    // the animation of CarouselView the pages are scrolled in turn, the CurrentItem
    // also changes in turn. To avoid this, need to process the swipe separately.
    // There is a special variable _isDragging, which determines the state of the swipe.
    // The beginning of the swipe is determined in the Scrolled event, since it is called
    // first during the swipe. The end of the swipe is determined only after setting the
    // SelectedItem, since in the Scrolled event IsDragging can become false before
    // the CurrentItemChanged event is called.
    private void CarouselView_Scrolled(object sender, ItemsViewScrolledEventArgs args)
    {
        if (ViewsCarouselView.IsDragging)
            _isDragging = true;
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (GetTemplateChild("TabsCollection") is CollectionPresenter tabsPresenter)
        {
            tabsPresenter.SelectionMode = SelectionMode.Single;
            tabsPresenter.SelectionChanged += CollectionView_SelectionChanged!;

            var tabsBinding = new Binding(ItemsSourceProperty.PropertyName, source: this);
            tabsPresenter.SetBinding(CollectionView.ItemsSourceProperty, tabsBinding);

            var tabsTemplateBinding = new Binding(TabTemplateProperty.PropertyName, source: this);
            tabsPresenter.SetBinding(CollectionView.ItemTemplateProperty, tabsTemplateBinding);

            _tabs = tabsPresenter;
        }
    }
}

