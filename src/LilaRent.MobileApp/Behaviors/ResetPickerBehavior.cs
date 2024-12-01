using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Services;

namespace LilaRent.MobileApp.Behaviors;


public static class ResetPickerBehavior
{
    private static readonly ILocalizationManager s_localizationManager;


    public static readonly BindableProperty AttachBehaviorProperty = BindableProperty.CreateAttached(
        "AttachBehavior", 
        typeof(bool), 
        typeof(ResetPickerBehavior), 
        defaultValue: false, 
        propertyChanged: OnAttachBehaviorChanged);


    static ResetPickerBehavior()
    {
        s_localizationManager = AppServiceProvider.Instance.GetRequiredService<ILocalizationManager>();
    }


    public static bool GetAttachBehavior(BindableObject view)
    {
        return (bool)view.GetValue(AttachBehaviorProperty);
    }

    public static void SetAttachBehavior(BindableObject view, bool value)
    {
        view.SetValue(AttachBehaviorProperty, value);
    }

    private static void OnAttachBehaviorChanged(BindableObject view, object oldValue, object newValue)
    { 
        if (view is Picker picker)
        {
            bool attachBehavior = (bool)newValue;

            if (attachBehavior)
            {
                s_localizationManager.PropertyChanged += (sender, args) =>
                {
                    // Reset values to update translations
                    var itemSource = picker.ItemsSource;
                    var selectedItem = picker.SelectedItem;

                    picker.ItemsSource = null;
                    picker.ItemsSource = itemSource;
                    picker.SelectedItem = selectedItem;
                };
            }
        }
    }
}