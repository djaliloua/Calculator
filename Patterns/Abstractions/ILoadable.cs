using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Patterns.Abstractions
{
    public interface ILoadable<TItem>: INotifyPropertyChanged, IActivity where TItem : class
    {
        ObservableCollection<TItem> Items { get; protected set; }
        TItem SelectedItem { get; set; }
        bool IsEmpty { get; }
        bool IsSelected { get; }
        int Counter { get; }
        int NumberOfItems { get; set; }
        ObservableCollection<TItem> GetItems();
        void SaveOrUpdateItem(TItem item);
        void DeleteAllItems();
        void SelectedItemCallBack(TItem item);
        void ItemsCallBack(IList<TItem> item);
        bool ItemExist(TItem item);
        void DeleteItem(TItem item);
        void AddItem(TItem item);
        void UpdateItem(TItem item);
    }
}
