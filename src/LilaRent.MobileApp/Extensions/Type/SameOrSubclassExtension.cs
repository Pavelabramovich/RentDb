using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilaRent.MobileApp.Extensions;


public static class SameOrSubclassExtension
{
    public static bool IsSameOrSubclassOf(this Type type, Type potentialParent)
    {
        return type.IsSubclassOf(potentialParent) || potentialParent == type;
    }

    public static bool IsSameOrSubclassOf<T>(this Type type)
    {
        return type.IsSameOrSubclassOf(typeof(T));
    }
}
