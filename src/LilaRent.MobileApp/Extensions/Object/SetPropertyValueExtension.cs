using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LilaRent.MobileApp.Extensions;

public static class SetPropertyValueExtension
{
    public static void SetPropertyValue<T>(this object @object, string propertyName, T value)
    {
        ArgumentNullException
            .ThrowIfNull(@object, nameof(@object));

        ArgumentException
            .ThrowIfNullOrWhiteSpace(propertyName, nameof(@propertyName));

        var property = @object.GetType().GetProperty(propertyName) 
            ?? throw new InvalidOperationException("Property with this name not found in this type.");


        try
        {
            property.SetValue(@object, value);
        }
        catch (Exception ex) when (ex 
            is ArgumentException
            or TargetException
            or MethodAccessException
            or TargetInvocationException)
        {
            throw new InvalidOperationException(ex.Message, ex);
        }   
    }
}
