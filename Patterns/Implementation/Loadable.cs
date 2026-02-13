using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;

namespace Patterns.Implementation
{
    public class Loadable<TItem> :  INotifyPropertyChanged where TItem : class
    {
        private ICollectionView _view;
        protected SortDescription SortDescription;
        protected Action<TItem> OnSelectedIem;
        protected Action<int> OnNumberOfItemsChanged;
        protected Action<int> OnCounterChanged;
        public Loadable()
        {
            Items.CollectionChanged += Items_CollectionChanged;
        }
        private void SetSortingProperty()
        {
            try
            {
                _view = CollectionViewSource.GetDefaultView(Items);
                _view.SortDescriptions.Clear();
                _view.SortDescriptions.Add(SortDescription);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        protected void SetSortDescription(SortDescription sortDescription)
        {
            SortDescription = sortDescription;
        }

        protected virtual void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Counter = Items.Count;
            NumberOfItems = Items.Count;
            OnPropertyChanged(nameof(IsEmpty));
        }

        private TItem _selectedItem;
        public TItem SelectedItem
        {
            get => _selectedItem;
            set => UpdateObservable(ref _selectedItem, value, () => OnSelectedIem?.Invoke(value));
        }
        private bool isRefreshed;
        public bool IsRefreshed
        {
            get => isRefreshed;
            set => UpdateObservable(ref isRefreshed, value);
        }

        public bool IsSelected => SelectedItem != null;
        private int _counter;
        public int Counter
        {
            get => _counter;
            set => UpdateObservable(ref _counter, value, () => OnCounterChanged?.Invoke(value));
        }
        public bool IsEmpty => Counter == 0;

        private int _numberOfItems;
        public int NumberOfItems
        {
            get => _numberOfItems;
            set => UpdateObservable(ref _numberOfItems, value, () => OnNumberOfItemsChanged?.Invoke(value));
        }
        
        private ObservableCollection<TItem> _items = new ObservableCollection<TItem>();
        public ObservableCollection<TItem> Items
        {
            get => _items;
            set => UpdateObservable(ref _items, value);
        }

        #region Public Methods

        protected Task LoadItems(IEnumerable<TItem> items)
        {
            Items.Clear();
            SetItems(items);

            return Task.CompletedTask;
        }

        protected virtual bool ItemExist(TItem item)
        {
            return Items.Contains(item);
        }
        protected virtual void SetItems(IEnumerable<TItem> items)
        {
            try
            {
                foreach(var item in items)
                {
                    AddItem(item);
                }
                Application.Current.Dispatcher.BeginInvoke(() => SetSortingProperty());
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        protected virtual void SaveOrUpdateItem(TItem item)
        {
            if (!ItemExist(item))
                AddItem(item);
            else
                UpdateItem(item);

        }
        protected virtual void DeleteAllItems()
        {
            Items.Clear();
        }
        protected virtual void DeleteItem(TItem item)
        {
            Items.Remove(item);
        }

        protected virtual void AddItem(TItem item)
        {
            Items.Add(item);
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

        protected virtual ObservableCollection<TItem> GetItems()
        {
            return Items;
        }
        #endregion

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
            callback?.Invoke();
        }
        #endregion
    }
}
