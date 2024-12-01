using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LilaRent.MobileApp.Extensions;

public static class IndexExtension
{
    public static void RemoveAt<T>(this ObservableCollection<T> observableCollection, Index index)
    {
        observableCollection.RemoveAt(index.GetOffset(observableCollection.Count));
    }
}
