
namespace LilaRent.MobileApp.MarkupExtensions;


[ContentProperty(nameof(Keys))]
public partial class MultiLocalizeFormatExtension : BaseLocalizeExtension
{
    public List<object> Keys { get; set; }

    public object? StringFormat { get; set; }


    public MultiLocalizeFormatExtension()
    {
        Keys = [];
        StringFormat = null;
    }


    public override BindingBase ProvideValue(IServiceProvider notCompetentBecauseOfEarlyXamlLoadingServiceProvider)
    {
        if (StringFormat is null)
            throw new InvalidOperationException("String format must be not null.");

        Strategy strategy = StringFormat switch
        { 
            BindingBase => new BindedFormatStrategy(_localizationManager),
            not null => new StaticFormatStrategy(_localizationManager),

            _ => throw new InvalidOperationException("Unknown strategy")
        };

        return strategy.ProvideValue(Keys, StringFormat);
    }
}
