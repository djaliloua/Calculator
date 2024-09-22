namespace Patterns.Abstractions
{
    public interface ILoadableService<TItem>
    {
        void DeleteItem(TItem item);
        void AddItem(TItem item);
        void UpdateItem(TItem item);
    }
    public interface ILoadService<TItem>
    {
        IList<TItem> Reorder(IList<TItem> items);
    }
}
