
namespace LilaRent.MobileApp.Services;


public class VVmTypePair
{
    public Type ViewType { get; }
    public Type ViewModelType { get; }


    public VVmTypePair(Type viewType, Type viewModelType)
    {
        ViewType = viewType;
        ViewModelType = viewModelType;
    }

    public void Deconstruct(out Type viewType, out Type viewModelType)
    {
        (viewType, viewModelType) = this.Deconstruct();
    }

    public (Type viewType, Type viewModelType) Deconstruct()
    {
        return (ViewType, ViewModelType);
    }

    public static VVmTypePair Create<TView, TViewModel>() where TView : IView where TViewModel : IViewModel
    {
        return new VVmTypePair(typeof(TView), typeof(TViewModel));
    }
}