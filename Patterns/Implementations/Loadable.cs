using Patterns.Abstractions;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Patterns.Implementations
{
    public abstract class Loadable<TItem> : ILoadable<TItem>, ILoadableService<TItem>, INotifyPropertyChanged, IActivity where TItem : class
    {
        public abstract Task LoadItems();
        protected abstract void Reorder();
        protected Loadable()
        {
            Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ItemsCollectionChanged(e);
        }
        protected virtual void ItemsCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Counter = Items.Count;
        }

        private TItem _selectedItem;
        public TItem SelectedItem
        {
            get => _selectedItem;
            set => UpdateObservable(ref _selectedItem, value, () => SelectedItemCallBack(value));
        }
        private ObservableCollection<TItem> _items;
        public ObservableCollection<TItem> Items
        {
            get => _items ?? new ObservableCollection<TItem>();
            set => UpdateObservable(ref _items, value, () => ItemsCallBack(value));
        }
        public bool IsSelected => SelectedItem != null;
        private int _counter;
        public int Counter
        {
            get => _counter;
            set => UpdateObservable(ref _counter, value);
        }
        public bool IsEmpty => Counter == 0;

        private int _numberOfItems;
        public int NumberOfItems
        {
            get => _numberOfItems;
            set => UpdateObservable(ref _numberOfItems, value);
        }
        private bool _isActivity;
        public bool IsActivity
        {
            get => _isActivity;
            set => UpdateObservable(ref _isActivity, value);
        }

        #region Public Methods
        public virtual void SelectedItemCallBack(TItem item)
        {

        }
        public virtual void ItemsCallBack(IList<TItem> item)
        {
            Counter = item.Count;
        }
        public virtual bool ItemExist(TItem item)
        {
            return _items.Contains(item);
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
        public virtual void DeleteAllItems()
        {
            Items.Clear();
            Counter = Items.Count;
            Notify();
        }
        public virtual void DeleteItem(TItem item)
        {
            Items.Remove(item);
            Counter = Items.Count;
        }

        public virtual void AddItem(TItem item)
        {
            Items.Add(item);
            Counter = Items.Count;
            Reorder();
        }
        protected virtual int Index(TItem item)
        {
            return Items.IndexOf(item);
        }
        public virtual void UpdateItem(TItem item)
        {
            int index = Index(item);
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

        public void ShowActivity()
        {
            IsActivity = true;
        }

        public void HideActivity()
        {
            IsActivity = false;
        }
        #endregion
    }
}
