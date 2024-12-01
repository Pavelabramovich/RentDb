using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;


namespace LilaRent.MobileApp.ViewModels;


public partial class TabbedViewModel : ObservableObject
{
    public ObservableCollection<IViewModel> Tabs { get; }

    [ObservableProperty]
    private IViewModel? _currentTab;


    public TabbedViewModel()
    {
        Tabs = [];
        _currentTab = null;

        Tabs.CollectionChanged += Tabs_CollectionChanged;
    }

    private void Tabs_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs eventArgs)
    {
        if (Tabs.Count == 0)
        {
            CurrentTab = null;
        }
        else if (eventArgs.OldItems?[0] == CurrentTab
            && eventArgs.Action 
               is NotifyCollectionChangedAction.Remove 
               or NotifyCollectionChangedAction.Replace)
        { 
            int oldIndex = eventArgs.OldStartingIndex;

            if (oldIndex >= Tabs.Count)
            {
                CurrentTab = Tabs[^1];
            }
            else
            {
                CurrentTab = Tabs[oldIndex];
            }
        }
    }
}
