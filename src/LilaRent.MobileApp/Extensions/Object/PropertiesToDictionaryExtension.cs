using System.Diagnostics.CodeAnalysis;


namespace LilaRent.MobileApp.Extensions;


public static class PropertiesToDictionaryExtension
{
    public static Dictionary<string, object?> PropertiesToDictionary(this object @object)
    {
        ArgumentNullException
            .ThrowIfNull(@object, nameof(@object));

        var props = @object.GetType().GetProperties();
        var propertiesDictionary = props.ToDictionary(p => p.Name, p => p.GetValue(@object));

        return propertiesDictionary!;
    }
}
