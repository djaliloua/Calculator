using Microsoft.EntityFrameworkCore;
using RegistrationApplication.DataAccessLayer.Abstractions;
using RegistrationApplication.DataAccessLayer.DataContext;

namespace RegistrationApplication.DataAccessLayer.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private bool disposedValue;
        protected DbContext _dbContext;
        protected DbSet<T> _table;
        public GenericRepository()
        {
            _dbContext = new TrainerDataContext();
            _table = _dbContext.Set<T>();
            _dbContext.Database.EnsureCreated();
        }

        public void Delete(object id)
        {
            T obj = GetValue(id);
            _table.Remove(obj);
            _dbContext.SaveChanges();
        }

        public IList<T> GetAll()
        {
            return _table.ToList();
        }

        public T GetValue(object id)
        {
            return _table.Find(id);
        }   

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && _dbContext != null)
                {
                    _dbContext.Dispose();
                }
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~GenericRepository()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public T Save(T entity)
        {
            _table.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            //_table.Attach(entity);
            ////Then set the state of the Entity as Modified
            //_dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }
    }
}
