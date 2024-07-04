namespace Patterns.Abstractions
{
    public interface ILoadableService<TItem>
    {
        void DeleteItem(TItem item);
        void AddItem(TItem item);
        void UpdateItem(TItem item);
        Task LoadItems();
    }
}
