using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilaRent.MobileApp.Extensions;

public static class IndexOfExtension
{
    public static int IndexOf<T>(this IReadOnlyList<T> readOnlyList, T item)
    {
        return readOnlyList.IndexOf(i =>
        {
            if (i is null && item is null)
                return true;

            if (i is null || item is null)
                return false;

            return i.Equals(item);
        });
    }

    public static int IndexOf<T>(this IReadOnlyList<T> readOnlyList, Func<T, bool> predicate)
    {
        for (int i = 0; i < readOnlyList.Count; i++)
        {
            if (predicate(readOnlyList[i]))
                return i;
        }

        return -1;
    }
}
