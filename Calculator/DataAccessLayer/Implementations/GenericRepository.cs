using Calculator.DataAccessLayer.Abstractions;
using Calculator.DataAccessLayer.Contexts;
using Calculator.MVVM.Models;

namespace Calculator.DataAccessLayer.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : BaseEntity
    {
        public OperationContext _context;
        private bool _isDisposed;
        public GenericRepository()
        {
            _context = new OperationContext();
            _context.Database.EnsureCreated();
        }
        public virtual void Delete(T item)
        {
            _context.Set<T>().Remove(item);
            _context.SaveChanges();
        }

        public virtual IEnumerable<T> GetAllItems()
        {
            return _context.Set<T>().ToList();
        }

        public virtual T GetItemById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual bool HasItem(T item)
        {
            return _context.Set<T>().Any(x => x.Id==item.Id);
        }
        public virtual T SaveOrUpdate(T item)
        {
            if(item.Id != 0)
            {
                _context.Update(item);
            }
            else
            {
                _context.Set<T>().Add(item);
            }
            _context.SaveChanges();
            return item;
        }
        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
            _isDisposed = true;
        }
    }
}
