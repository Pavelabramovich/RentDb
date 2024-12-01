
namespace LilaRent.MobileApp.Behaviors;


public class AttachedBehavior<TBehavior> where TBehavior : Behavior
{
    public static readonly BindableProperty BehaviorProperty = BindableProperty.CreateAttached(
        nameof(GetBehavior).Replace("Get", string.Empty), 
        typeof(Behavior), 
        typeof(AttachedBehavior<TBehavior>), 
        defaultValue: null, 
        propertyChanged: OnBehaviorChanged
    );

    public static Behavior GetBehavior(BindableObject view)
    {
        return (Behavior)view.GetValue(BehaviorProperty);
    }

    public static void SetBehavior(BindableObject view, Behavior value)
    {
        view.SetValue(BehaviorProperty, value);
    }


    private static void OnBehaviorChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var element = (VisualElement)bindable;

        var oldBehavior = (Behavior)oldValue;
        var newBehavior = (Behavior)newValue;

        if (oldBehavior is not null)
            element.Behaviors.Remove(oldBehavior);

        if (newBehavior is not null)
            element.Behaviors.Add(newBehavior);
    }
}
