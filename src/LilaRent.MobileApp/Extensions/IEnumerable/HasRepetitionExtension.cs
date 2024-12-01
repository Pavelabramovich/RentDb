
using System.Collections;
using System.Collections.Generic;

namespace LilaRent.MobileApp.Extensions;


public static class HasRepetitionsExtension
{
    public static bool HasRepetitions<T>(this IEnumerable<T> enumerable)
    {
        return enumerable.Count() != enumerable.Distinct().Count();
    }

    public static bool HasRepetitions<T>(this IEnumerable<T> enumerable, IEqualityComparer<T> comparer)
    {
        return enumerable.Count() != enumerable.Distinct(comparer).Count();
    }
}
