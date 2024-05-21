using MVVM;
using System.Collections.ObjectModel;

namespace Calculator.MVVM.ViewModels
{
    public abstract class Loadable<TItem> : BaseViewModel
    {
        public ObservableCollection<TItem> Items { get; set; } = new ObservableCollection<TItem>();
        private TItem _selectedItem;
        public TItem SelectedItem
        {
            get => _selectedItem;
            set => UpdateObservable(ref _selectedItem, value);
        }
        public abstract Task LoadItems();
        public virtual ObservableCollection<TItem> GetItems() => Items;
        public virtual void DeleteItem(TItem item)
        {
            Items.Remove(item);
        }
        public bool IsSelected => SelectedItem != null;
        public int Counter => Items.Count;
        public bool IsEmpty => Counter == 0;
        public virtual void Reorder()
        {

        }
        public virtual void DeleteAllItems() => Items.Clear();
        public virtual void Update(TItem item)
        {
            SelectedItem = item;
            OnPropertyChanged(nameof(SelectedItem));
        }

        public virtual void AddItem(TItem item)
        {
            Items.Add(item);
            Reorder();
            //Notify();
        }
        public virtual void SetItems(IEnumerable<TItem> items)
        {
            Items = new ObservableCollection<TItem>(items);
            Notify();
        }
        public virtual void Notify() => OnPropertyChanged(nameof(Items));
        private int _numberOfItems;
        public virtual int NumberOfItems
        {
            get => _numberOfItems;
            set => UpdateObservable(ref _numberOfItems, value);
        }
    }
}
