using DatabaseContexts;
using Mapster;
using Microsoft.EntityFrameworkCore;
using RepositoryService.Interface;

namespace RepositoryService.Implementation
{
    public class GenericRepositoryViewModel<TVM, T> : GenericRepository<T>, IRepositoryViewModel<TVM, T> where T : class
    {
        public virtual IList<TVM> GetAllToViewModel()
        {
            List<TVM> _table_ = new List<TVM>();
            if (_table == null || _table.Count() == 0)
            {
                return _table_;
            }
            foreach (var item in _table)
            {
                TVM vm = item.Adapt<TVM>();
                if (vm != null)
                {
                    _table_.Add(vm);
                }
            }
            return _table_;
        }
    }
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

        ~GenericRepository()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
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
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public async Task DeleteAsync(object id)
        {
            object obj = await _table.FindAsync(id);
            _table.Remove((T)obj);
            await _dbContext.SaveChangesAsync();
        }

        public Task<T> GetValueAsync(object id)
        {
            return Task.FromResult(GetValue(id));
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public Task<T> SaveAsync(T entity)
        {
            _table.Add(entity);
            _dbContext.SaveChangesAsync();
            return Task.FromResult(entity);
        }

        public Task<T> UpdateAsync(T entity)
        {
            _dbContext.Update(entity);
            _dbContext.SaveChangesAsync();
            return Task.FromResult(entity);
        }
    }
}
