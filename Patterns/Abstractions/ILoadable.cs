using System.Collections.ObjectModel;

namespace Patterns.Abstractions
{
    public interface ILoadable<TItem>
    {
        ObservableCollection<TItem> Items { get; protected set; }
        TItem SelectedItem { get; set; }
        bool IsEmpty { get; }
        bool IsSelected { get; }
        int Counter { get; }
        int NumberOfItems { get; set; }
        void SetItems(IList<TItem> items);
        ObservableCollection<TItem> GetItems();
        void AddOrUpdateItem(TItem item);
        void DeleteAllItems();
        void SelectedItemCallBack(TItem item);
        void ItemsCallBack(IList<TItem> item);
        bool ItemExist(TItem item);
    }
}
