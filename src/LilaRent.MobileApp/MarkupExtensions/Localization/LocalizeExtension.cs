
namespace LilaRent.MobileApp.MarkupExtensions;


[ContentProperty(nameof(Key))]
public partial class LocalizeExtension : BaseLocalizeExtension
{
    public object? Key { get; set; }
    public object? StringFormat { get; set; }

    public IValueConverter? InitialConverter { get; set; }
    public IValueConverter? FinalConverter { get; set; }

    public override BindingBase ProvideValue(IServiceProvider notCompetentBecauseOfEarlyXamlLoadingServiceProvider) 
    {
        Strategy strategy = (Key, StringFormat) switch
        {
            (BindingBase, BindingBase) => new BindedKeyBindedFormatStrategy(_localizationManager),
            (BindingBase, not null) => new BindedKeyStaticFormatStrategy(_localizationManager),
            (not null, BindingBase) => new StaticKeyBindedFormatStrategy(_localizationManager),
            (BindingBase, null) => new BindedKeyStrategy(_localizationManager),

            (not null, not null) => new StaticKeyStaticFormatStrategy(_localizationManager),
            (not null, null) => new StaticKeyStrategy(_localizationManager),
                   
            _ => throw new InvalidOperationException("Unknown strategy"),
        };

        return strategy.ProvideValue(Key, StringFormat, InitialConverter, FinalConverter);
    }
}



