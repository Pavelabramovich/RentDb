using System.ComponentModel;
using CommunityToolkit.Maui.Converters;
using System.Runtime.CompilerServices;
using LilaRent.MobileApp.Converters;
using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Services;
using LilaRent.MobileApp.ViewModels;


namespace LilaRent.MobileApp.Views;


public partial class NavigationView : NavigationPage
{
    private const string DefaultControlTemplateKey = "DefaultControlTemplate";
    private const string DefaultBackButtonStyleKey = "DefaultBackButtonStyle";


    public static readonly BindableProperty ControlTemplateProperty = BindableProperty.Create(
        nameof(ControlTemplate),
        typeof(ControlTemplate),
        typeof(NavigationView),
        defaultValueCreator: CreateDefaultControlTemplate,
        propertyChanged: OnControlTemplatePropertyChanged,
        coerceValue: CoerceControlTemplate
    );

    public static readonly BindableProperty BackButtonStyleProperty = BindableProperty.Create(
       nameof(BackButtonStyle),
       typeof(Style),
       typeof(NavigationView),
       defaultValueCreator: CreateDefaultBackButtonStyle
   );


    public static readonly BindableProperty PageControlTemplateProperty = BindableProperty.CreateAttached(
        nameof(GetPageControlTemplate).Replace("Get", string.Empty),
        typeof(ControlTemplate),
        typeof(ContentPage),
        defaultValue: null,
        propertyChanged: OnPageContorolTemplatePropertyChanged,
        coerceValue: CoercePageControlTemplate
    );


    public static readonly BindableProperty PageBackButtonStyleProperty = BindableProperty.CreateAttached(
        nameof(GetPageBackButtonStyle).Replace("Get", string.Empty),
        typeof(Style),
        typeof(ContentPage),
        defaultValue: null
    );


    public static readonly BindableProperty PageTitleViewProperty = BindableProperty.CreateAttached(
        nameof(GetPageTitleView).Replace("Get", string.Empty),
        typeof(View),
        typeof(ContentPage),
        defaultValue: null,
        propertyChanged: OnPageTitleViewChanged
    );

    private static void OnPageTitleViewChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var oldTitleView = (View)oldValue;
        var newTitleView = (View)newValue;

        if (oldTitleView is not null)
        {
            oldTitleView.RemoveBinding(View.BindingContextProperty);
            oldTitleView.ClearValue(Page.BindingContextProperty);
            oldTitleView.BindingContext = null;
        }

        if (newTitleView is not null)
        {
            var bindingContextBinding = new Binding(path: "BindingContext", source: bindable);
            newTitleView.SetBinding(View.BindingContextProperty, bindingContextBinding);
        }
    }

    private readonly IServiceProvider _serviceProvider;

    private readonly IVVmResolveService _vVmResolveService;
    private readonly IApplicationService _applicationService;


    public NavigationView(IServiceProvider serviceProvider)
        : base()
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;
        _vVmResolveService = _serviceProvider.GetRequiredService<IVVmResolveService>();
        _applicationService = _serviceProvider.GetRequiredService<IApplicationService>();

        Pushed += NavigationView_Pushed;
        Popped += NavigationView_Popped;
        PoppedToRoot += NavigationView_PoppedToRoot;

        _applicationService.CurrentApplication.ModalPushed += CurrentApplication_ModalPushed;
        _applicationService.CurrentApplication.ModalPopped += CurrentApplication_ModalPopped;

        Binding iconImageSourceBinding = new() { Path = nameof(NavigationViewModel.IconImageSource) };
        SetBinding(IconImageSourceProperty, iconImageSourceBinding);


        #if WINDOWS && DEBUG
            Title = "Hoh";
        #endif
    }


    public ControlTemplate ControlTemplate
    {
        get => (ControlTemplate)GetValue(ControlTemplateProperty);
        set => SetValue(ControlTemplateProperty, value);
    }

    public Style BackButtonStyle
    {
        get => (Style)GetValue(BackButtonStyleProperty);
        set => SetValue(BackButtonStyleProperty, value);
    }




    protected override void OnPropertyChanging([CallerMemberName] string? propertyName = null)
    {
        if (propertyName == nameof(BindingContext))
            OnBindingContextChanging();

        base.OnPropertyChanging(propertyName);
    }



    private static object CreateDefaultControlTemplate(BindableObject bindable)
    {
        var navigationView = (NavigationView)bindable;
        var template = (ControlTemplate)navigationView.Resources[DefaultControlTemplateKey];

        return navigationView.CreateEnhancedTemplate(template, navigationView);
    }


    private static object CoerceControlTemplate(BindableObject bindable, object value)
    {
        var navigationView = (NavigationView)bindable;
        var template = (ControlTemplate)value;

        return navigationView.CreateEnhancedTemplate(template, navigationView);
    }


    private static void OnControlTemplatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var navigationView = (NavigationView)bindable;
        var newTemplate = (ControlTemplate)newValue;

        foreach (var page in navigationView.Navigation.NavigationStack)
        {
            if (page is ContentPage contentPage)
            {
                contentPage.ControlTemplate = newTemplate;
            }
        }
    }


    private static object CreateDefaultBackButtonStyle(BindableObject bindable)
    {
        var navigationView = (NavigationView)bindable;

        var style = (Style)navigationView.Resources[DefaultBackButtonStyleKey];
        return style;
    }


    public static ControlTemplate GetPageControlTemplate(BindableObject bindable)
    {
        return (ControlTemplate)bindable.GetValue(PageControlTemplateProperty);
    }

    public static void SetPageControlTemplate(BindableObject bindable, ControlTemplate value)
    {
        bindable.SetValue(PageControlTemplateProperty, value);
    }


    private static object? CoercePageControlTemplate(BindableObject bindable, object value)
    {
        var page = (ContentPage)bindable;
        var template = (ControlTemplate)value;

        if (page.Parent is not NavigationView navigationView || template is null)
            return template;

        return navigationView.CreateEnhancedTemplate(template, page);
    }

    private static void OnPageContorolTemplatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var page = (ContentPage)bindable;
        var template = (ControlTemplate)newValue;

        if (page.Parent is not NavigationView navigationView)
            return;

        if (template is null)
        {
            page.ControlTemplate = ((NavigationBarTemplate)navigationView.ControlTemplate).AddCurrentPage(page);
        }
        else
        {
            page.ControlTemplate = template;
        }
    }


    public static Style GetPageBackButtonStyle(BindableObject bindable)
    {
        return (Style)bindable.GetValue(PageBackButtonStyleProperty);
    }

    public static void SetPageBackButtonStyle(BindableObject bindable, Style value)
    {
        bindable.SetValue(PageBackButtonStyleProperty, value);
    }


    public static View GetPageTitleView(BindableObject bindable)
    {
        return (View)bindable.GetValue(PageTitleViewProperty);
    }

    public static void SetPageTitleView(BindableObject bindable, View value)
    {
        bindable.SetValue(PageBackButtonStyleProperty, value);
    }


    private void NavigationView_Pushed(object? sender, NavigationEventArgs args)
    {
        if (BindingContext is NavigationViewModel navigationViewModel
            && Navigation.NavigationStack.Count == navigationViewModel.NavigationStack.Count + 1)
        {
            var newView = args.Page;
            SetUpNavigationBar(newView);

            Type viewModelType = _vVmResolveService.GetViewModelType(newView.GetType());

            navigationViewModel.NavigationStack.NavigationListChanged -= ViewModelNavigationStack_Changed;

            var newViewModel = navigationViewModel.NavigationStack.Push(viewModelType);
            newView.BindingContext = newViewModel;

            navigationViewModel.NavigationStack.NavigationListChanged += ViewModelNavigationStack_Changed;
        }
    }


    private void NavigationView_Popped(object? sender, NavigationEventArgs args)
    {
        if (BindingContext is NavigationViewModel navigationViewModel
            && Navigation.NavigationStack.Count == navigationViewModel.NavigationStack.Count - 1)
        {
            var oldView = args.Page;
            RemoveNavigationBar(oldView);

            navigationViewModel.NavigationStack.NavigationListChanged -= ViewModelNavigationStack_Changed;
            navigationViewModel.NavigationStack.Pop();
            navigationViewModel.NavigationStack.NavigationListChanged += ViewModelNavigationStack_Changed;
        }
    }


    private void NavigationView_PoppedToRoot(object? sender, NavigationEventArgs args)
    {
        NavigationView_PoppedToRoot(sender, (PoppedToRootEventArgs)args);   
    }

    private void NavigationView_PoppedToRoot(object? sender, PoppedToRootEventArgs args)
    {
        if (BindingContext is NavigationViewModel navigationViewModel
            && Navigation.NavigationStack.Count < navigationViewModel.NavigationStack.Count)
        {
            var pages = args.PoppedPages;

            foreach (var page in pages)
            {
                RemoveNavigationBar(page);
            }

            navigationViewModel.NavigationStack.NavigationListChanged -= ViewModelNavigationStack_Changed;
            navigationViewModel.NavigationStack.PopToRoot();
            navigationViewModel.NavigationStack.NavigationListChanged += ViewModelNavigationStack_Changed;
        }
    }


    private void CurrentApplication_ModalPopped(object? sender, ModalPoppedEventArgs args)
    {
        if (BindingContext is NavigationViewModel navigationViewModel
            && Navigation.ModalStack.Count == navigationViewModel.ModalStack.Count - 1)
        {
            navigationViewModel.ModalStack.NavigationStackChanged -= ViewModelModalStack_Changed;
            navigationViewModel.ModalStack.Pop();
            navigationViewModel.ModalStack.NavigationStackChanged += ViewModelModalStack_Changed;
        }
    }

    private void CurrentApplication_ModalPushed(object? sender, ModalPushedEventArgs args)
    {
        if (BindingContext is NavigationViewModel navigationViewModel
            && Navigation.ModalStack.Count == navigationViewModel.ModalStack.Count + 1)
        {
            var modalView = args.Modal;
            Type viewModelType = _vVmResolveService.GetViewModelType(modalView.GetType());

            navigationViewModel.ModalStack.NavigationStackChanged -= ViewModelModalStack_Changed;

            var newViewModel = navigationViewModel.ModalStack.Push(viewModelType);
            modalView.BindingContext = newViewModel;

            navigationViewModel.ModalStack.NavigationStackChanged += ViewModelModalStack_Changed;
        }
    }


    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (BindingContext is NavigationViewModel newBindingContext)
        {
            newBindingContext.NavigationStack.NavigationListChanged += ViewModelNavigationStack_Changed;
            InitNavigationStack(newBindingContext.NavigationStack);

            newBindingContext.ModalStack.NavigationStackChanged += ViewModelModalStack_Changed;
            InitModalStack(newBindingContext.ModalStack);
        }
    }


    protected virtual void OnBindingContextChanging()
    {
        if (BindingContext is NavigationViewModel oldBindingContext)
        {
            oldBindingContext.NavigationStack.NavigationListChanged -= ViewModelNavigationStack_Changed;
            oldBindingContext.ModalStack.NavigationStackChanged -= ViewModelModalStack_Changed;
        }
    }


    private async void ViewModelNavigationStack_Changed(object? sender, NotifyNavigationListChangedEventArgs args)
    {
        var action = args.Action;

        if (action == NotifyNavigationListChangedAction.Insert)
        {
            int index = args.NewStartingIndex;
            var newViewModel = (IViewModel)args.NewItems[0]!;

            await AddNavigationViewModel(newViewModel, index);
        }
        else if (action == NotifyNavigationListChangedAction.Remove)
        {
            int index = args.OldStartingIndex;

            await RemoveNavigationViewModel(index);
        }
        else if (action == NotifyNavigationListChangedAction.PopToRoot)
        {
            await PopToNavigationStackRoot();
        }
        else
        {
            throw new ArgumentException($"Unknown type of {nameof(NotifyNavigationListChangedAction)}.", nameof(args));
        }
    }

    private async void ViewModelModalStack_Changed(object? sender, NotifyNavigationStackChangedEventArgs args)
    {
        var action = args.Action;

        if (action == NotifyNavigationStackChangedAction.Push)
        {
            var newViewModel = (IViewModel)args.NewItem!;

            await AddModalViewModel(newViewModel);
        }
        else if (action == NotifyNavigationStackChangedAction.Pop)
        {
            await RemoveModalViewModel();
        }
        else
        {
            throw new ArgumentException($"Unknown type of {nameof(NotifyNavigationStackChangedAction)}.", nameof(args));
        }
    }


    private async Task AddNavigationViewModel(IViewModel newViewModel, int index)
    {
        var newView = (Page)GetView(newViewModel.GetType());
        newView.BindingContext = newViewModel;

        if (index == Navigation.NavigationStack.Count)
        {
            await Navigation.PushAsync(newView);
        }
        else
        {
            var viewBefore = Navigation.NavigationStack[index];
            Navigation.InsertPageBefore(newView, viewBefore);
        }
    }

    private async Task RemoveNavigationViewModel(int index)
    {
        var oldView = Navigation.NavigationStack[index];

        if (index == Navigation.NavigationStack.Count - 1)
        {
            await Navigation.PopAsync();
        }
        else
        {
            var viewToRemove = Navigation.NavigationStack[index];
            Navigation.RemovePage(viewToRemove);
        }

        RemoveNavigationBar(oldView);
    }

    private async Task PopToNavigationStackRoot()
    {
        var oldViews = Navigation.NavigationStack.Skip(1).ToArray();

        await Navigation.PopToRootAsync();

        foreach (var oldView in oldViews) 
        {
            RemoveNavigationBar(oldView);
        }     
    }


    private async Task AddModalViewModel(IViewModel newViewModel)
    {
        var newView = (Page)GetView(newViewModel.GetType());
        newView.BindingContext = newViewModel;

        await Navigation.PushModalAsync(newView);
    }

    private async Task RemoveModalViewModel()
    {
        await Navigation.PopModalAsync();
    }


    private async void InitNavigationStack(IEnumerable<IViewModel> newStack)
    {
        if (Navigation.NavigationStack.Count == 0)
        {
            foreach (var newViewModel in newStack)
            {
                var newView = (Page)GetView(newViewModel.GetType());
                newView.BindingContext = newViewModel;

                await Navigation.PushAsync(newView);
            }

            return;
        }
        else if (Navigation.NavigationStack.Count > 0 && newStack.Any())
        {
            IEnumerable<Page> oldStack = Navigation.NavigationStack;

            var newStackHeadViewModel = newStack.Last();
            var newStackRestViewModels = newStack.ToArray()[..^1];

            var newStackHeadView = (Page)GetView(newStackHeadViewModel.GetType());
            newStackHeadView.BindingContext = newStackHeadViewModel;

            var newStackRestViews = newStackRestViewModels.Select(viewModel =>
            {
                var newView = (Page)GetView(viewModel.GetType());
                newView.BindingContext = viewModel;

                return newView;
            });


            await Navigation.PushAsync(newStackHeadView);

            foreach (var oldView in oldStack)
            {
                Navigation.RemovePage(oldView);
            }

            foreach (var newView in newStackRestViews)
            {
                Navigation.InsertPageBefore(newView, Navigation.NavigationStack[0]);
            }
        }
        else
        {
            throw new InvalidOperationException("Can't clear navigation stack root.");
        }

    }

    private async void InitModalStack(IEnumerable<IViewModel> newStack)
    {
        while (Navigation.ModalStack.Count > 0)
        {
            await Navigation.PopModalAsync();
        }

        foreach (var newViewModel in newStack)
        {
            var newView = (Page)GetView(newViewModel.GetType());
            newView.BindingContext = newViewModel;

            await Navigation.PushModalAsync(newView);
        }
    }

    private IView GetView(Type viewModelType)
    {
        Type viewType = _vVmResolveService.GetViewType(viewModelType);
        var view = (Page)_serviceProvider.GetRequiredService(viewType);

        SetUpNavigationBar(view);

        return view;
    }

    private object GetViewModel(Type viewType)
    {
        Type viewModelType = _vVmResolveService.GetViewModelType(viewType);

        return _serviceProvider.GetRequiredService(viewModelType);
    }


    private void SetUpNavigationBar(Page view)
    {
        if (view is not ContentPage contentPage)
            return;

        NavigationPage.SetHasBackButton(view, false);
        NavigationPage.SetHasNavigationBar(view, false);


        if (NavigationView.GetPageControlTemplate(view) is ControlTemplate template)
        {
            var navbarTemplate = CreateEnhancedTemplate(template, view);
            SetPageControlTemplate(view, navbarTemplate);

            contentPage.ControlTemplate = navbarTemplate;
        }
        else
        {
            contentPage.ControlTemplate = ((NavigationBarTemplate)ControlTemplate).AddCurrentPage(contentPage);
        }
    }


    private void RemoveNavigationBar(IView view)
    {
        if (view is not ContentPage contentPage)
            return;

        if (NavigationView.GetPageControlTemplate(view) is NavigationBarTemplate navbarTemplate)
        {
            SetPageControlTemplate(view, navbarTemplate.Template);
        }

        contentPage.ControlTemplate = ContentOnlyControlTemplate.Instance;
    }


    protected virtual EnhancedControlTemplate CreateEnhancedTemplate(ControlTemplate template, Page? currentPage = null)
    {
        return new NavigationBarTemplate(template, this, currentPage);
    }


    //private void NavigationView_Unloaded(object? sender, EventArgs e)
    //{
    //    // maybe there should be IDisposable with destructor

    //    _applicationService.CurrentApplication.ModalPushed -= CurrentApplication_ModalPushed;
    //    _applicationService.CurrentApplication.ModalPopped -= CurrentApplication_ModalPopped;

    //    if (BindingContext is NavigationViewModel navigationViewModel)
    //    {
    //        navigationViewModel.NavigationStack.CollectionChanged -= _viewModelNavigationStack_Changed;
    //        navigationViewModel.ModalNavigationStack.CollectionChanged -= _viewModelModalNavigationStack_Changed;
    //    }
    //}


    protected class NavigationBarTemplate : EnhancedControlTemplate
    {
        protected readonly NavigationView _navigationView;
        private readonly Page? _currentPage;


        public NavigationBarTemplate(ControlTemplate template, NavigationView navigationView, Page? currentPage = null)
            : base(template)
        {
            _navigationView = navigationView;
            _currentPage = currentPage;
        }

        protected override void Modify(Element root)
        {
            if (root.FindByName("BackButton") is ContentPresenter backButtonPresenter)
                backButtonPresenter.Content = CreateBackButton();

            if (root.FindByName("TitleView") is ContentPresenter titlePresenter)
                titlePresenter.Content = CreateTitleView();
        }

        protected virtual ImageButton CreateBackButton()
        {
            var backButton = new ImageButton();

            backButton.Clicked += async (sender, args) => await _navigationView.PopAsync();

            if (_currentPage is null)
            {
                var backBauttonStyleBinding = new Binding(BackButtonStyleProperty.PropertyName, source: _navigationView);
                backButton.SetBinding(ImageButton.StyleProperty, backBauttonStyleBinding);
            }
            else
            {
                var navBinding = new Binding(BackButtonStyleProperty.PropertyName, source: _navigationView);
                var pageBinding = BindingExtension
                    .CreateBindingToAttachedProperty(PageBackButtonStyleProperty, _currentPage);

                var binding = new MultiBinding() { Bindings = [pageBinding, navBinding], Converter = new FirstNotNullOrNullMultiConverter() };
                backButton.SetBinding(ImageButton.StyleProperty, binding);

                var isButtonVisibleBinding = new Binding()
                {
                    Path = NavigationPage.RootPageProperty.PropertyName,
                    Source = _navigationView,
                    Converter = new IsNotEqualConverter(),
                    ConverterParameter = _currentPage,
                };

                backButton.SetBinding(ImageButton.IsVisibleProperty, isButtonVisibleBinding);
            }

            return backButton;
        }

        protected virtual View CreateTitleView()
        {
            var titleLabel = new Label();

            if (_currentPage is null)
            {
                var titleBinding = new Binding(TitleProperty.PropertyName, source: _navigationView);
                titleLabel.SetBinding(Label.TextProperty, titleBinding);

                return titleLabel;
            }
            else
            {
                var titleViewBinding = BindingExtension
                    .CreateBindingToAttachedProperty(PageTitleViewProperty, _currentPage);

                var titleBinding = new Binding(TitleProperty.PropertyName, source: _navigationView);
                var pageTitleBinding = new Binding(Page.TitleProperty.PropertyName, source: _currentPage);

                var titleMultiBinding = new MultiBinding()
                {
                    Bindings = [pageTitleBinding, titleBinding],
                    Converter = new FirstNotNullOrNullMultiConverter()
                };
                titleLabel.SetBinding(Label.TextProperty, titleMultiBinding);

                var titleLabelBinding = new Binding(".", source: titleLabel);

                var titleViewMultiBinding = new MultiBinding()
                {
                    Bindings = [titleViewBinding, titleLabelBinding],
                    Converter = new FirstNotNullOrNullMultiConverter()
                };

                var contentView = new ContentView();
                contentView.SetBinding(ContentView.ContentProperty, titleViewMultiBinding);

                return contentView;
            }
        }


        public virtual NavigationBarTemplate AddCurrentPage(Page currentPage)
        {
            return new NavigationBarTemplate(Template, _navigationView, currentPage);
        }
    }
}


public class BindingExtension
{
    public static Binding CreateBindingToAttachedProperty(BindableProperty attachedProperty, BindableObject source)
    {
        var attachedSource = new AttachedBindingSourceBindingObject(attachedProperty, source);
        return new Binding(AttachedBindingSourceBindingObject.ValueProperty.PropertyName, source: attachedSource);
    }


    private class AttachedBindingSourceBindingObject : BindableObject
    {
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
            nameof(Value),
            typeof(object),
            typeof(AttachedBindingSourceBindingObject),
            defaultValue: null
        );

        private readonly BindableObject _source;
        private readonly string _propertyName;
        private readonly Func<object> _propertyGetter;


        public AttachedBindingSourceBindingObject(BindableProperty attachedProperty, BindableObject source)
        {
            _source = source;
            _source.PropertyChanged += Source_PropertyChanged;
            _propertyName = attachedProperty.PropertyName;
            _propertyGetter = () => source.GetValue(attachedProperty);

            object propertyValue = _propertyGetter();
            Value = propertyValue;
        }

        private void Source_PropertyChanged(object? sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == _propertyName)
            {
                object propertyValue = _propertyGetter();
                Value = propertyValue;
            }
        }

        public object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }


        ~AttachedBindingSourceBindingObject()
        {
            _source.PropertyChanged -= Source_PropertyChanged;
        }
    }
}
