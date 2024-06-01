using MVVM;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Calculator.MVVM.ViewModels
{
    public interface ILoadable<TItem>
    {
        ObservableCollection<TItem> Items { get; set; }
        TItem SelectedItem { get; set; }
        bool IsEmpty { get; }
        bool IsSelected { get; }
        int Counter { get; }
        int NumberOfItems { get; set; }
        void SetItems(IList<TItem> items);
        void DeleteItem(TItem item);
        void AddItem(TItem item);
        void UpdateItem(TItem item);
        ObservableCollection<TItem> GetItems();
        void AddOrUpdateItem(TItem item);
        void DeleteAllItems();
        void SelectedItemCallBack(TItem item);
        void ItemsCallBack(IList<TItem> item);
    }
    public abstract class Loadable<TItem> : ILoadable<TItem>, INotifyPropertyChanged
    {
        public abstract Task LoadItems();
        protected abstract void Reorder();
        private TItem _selectedItem;
        public TItem SelectedItem
        {
            get => _selectedItem;
            set => UpdateObservable(ref _selectedItem, value, () => SelectedItemCallBack(value));
        }
        private ObservableCollection<TItem> _items;
        public ObservableCollection<TItem> Items
        {
            get => _items;
            set => UpdateObservable(ref _items, value, () => ItemsCallBack(value));
        }
        public bool IsSelected => SelectedItem != null;
        public int Counter => Items.Count;
        public bool IsEmpty => Counter == 0;

        private int _numberOfItems;
        public int NumberOfItems
        {
            get => _numberOfItems;
            set => UpdateObservable(ref _numberOfItems, value);
        }

        #region Public Methods
        public virtual void SelectedItemCallBack(TItem item)
        {

        }
        public virtual void ItemsCallBack(IList<TItem> item)
        {

        }
        public virtual void SetItems(IList<TItem> items)
        {
            Items = new ObservableCollection<TItem>(items);
            Notify();
        }
        public virtual void AddOrUpdateItem(TItem item)
        {
            if (!Items.Contains(item))
                AddItem(item);
            else
                UpdateItem(item);
            Notify();
        }
        public void DeleteAllItems()
        {
            Items.Clear();
            Notify();
        }
        public virtual void DeleteItem(TItem item)
        {
            Items.Remove(item);
        }

        public virtual void AddItem(TItem item)
        {
            Items.Add(item);
            Reorder();
        }

        public virtual void UpdateItem(TItem item)
        {
            int index = Items.IndexOf(item);
            if (index >= 0)
            {
                Items.RemoveAt(index);
                Items.Insert(index, item);
            }
        }

        public virtual ObservableCollection<TItem> GetItems()
        {
            return Items;
        }
        #endregion

        private void Notify()
        {
            OnPropertyChanged(nameof(Items));
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public void UpdateObservable<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            oldValue = newValue;
            OnPropertyChanged(propertyName);
        }
        public void UpdateObservable<T>(ref T oldValue, T newValue, Action callback, [CallerMemberName] string propertyName = "")
        {
            oldValue = newValue;
            OnPropertyChanged(propertyName);
            callback();
        }
        #endregion
    }
}
