namespace Calculator.DataAccessLayer.Abstractions
{
    public interface IGenericRepository<T> where T : class
    {
        T GetItemById(int id);
        IEnumerable<T> GetAllItems();
        void Delete(T item);
        bool HasItem(T item);
        T SaveOrUpdate(T item);
    }
}
