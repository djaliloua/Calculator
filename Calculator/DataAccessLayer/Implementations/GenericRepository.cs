using Calculator.DataAccessLayer.Abstractions;
using Calculator.DataAccessLayer.Contexts;
using Calculator.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculator.DataAccessLayer.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly OperationContext _context;
        protected readonly DbSet<T> _entities;
        public GenericRepository(OperationContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
            _context.Database.EnsureCreated();
        }
        public GenericRepository()
        {
            _context = ServiceLocator.GetService<OperationContext>();
            _entities = _context.Set<T>();
            _context.Database.EnsureCreated();
        }
        public virtual void Delete(T item)
        {
            _entities.Remove(item);
            _context.SaveChanges();
        }

        public virtual IEnumerable<T> GetAllItems()
        {
            return _entities.ToList();
        }

        public virtual T GetItemById(int id)
        {
            return _entities.Find(id);
        }

        public virtual bool HasItem(T item)
        {
            return _entities.Any(x => x.Id==item.Id);
        }
        public virtual T SaveOrUpdate(T item)
        {
            if(item.Id != 0)
            {
                _context.Entry(item).OriginalValues.SetValues(item);
            }
            else
            {
                _entities.Add(item);
            }
            _context.SaveChanges();
            return item;
        }
    }
}
