namespace RegistrationApplication.DataAccessLayer.Abstractions
{
    public interface IGenericRepository<T>
    {
        void Delete(object id);
        T GetValue(object id);
        IList<T> GetAll();
        T Save(T entity);
        T Update(T entity);
    }
}
