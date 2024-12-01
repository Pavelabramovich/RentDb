
namespace LilaRent.MobileApp.MarkupExtensions;


[ContentProperty(nameof(Key))]
public partial class LocalizeFormatExtension : BaseLocalizeExtension 
{
    public object? Key { get; set; }
    public object? StringFormat { get; set; }

    public IValueConverter? Converter { get; set; }


    public override BindingBase ProvideValue(IServiceProvider notCompetentBecauseOfEarlyXamlLoadingServiceProvider) 
    {
        if (StringFormat is null)
            throw new InvalidOperationException("String format must be not null.");

        Strategy strategy = (Key, StringFormat) switch
        {
            (BindingBase, BindingBase) => new BindedKeyBindedFormatStrategy(_localizationManager),
            (BindingBase, not null) => new BindedKeyStaticFormatStrategy(_localizationManager),
            (not null, BindingBase) => new StaticKeyBindedFormatStrategy(_localizationManager),

            (not null, not null) => new StaticKeyStaticFormatStrategy(_localizationManager),
                   
            _ => throw new InvalidOperationException("Unknown strategy"),
        };

        return strategy.ProvideValue(Key, StringFormat, Converter);
    }
}



