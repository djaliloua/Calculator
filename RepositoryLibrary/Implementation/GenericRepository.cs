using CalculatorModel;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using RepositoryLibrary.Interface;

namespace RepositoryLibrary.Implementation
{
    public class GenericRepositoryViewModel<TVM, T> : GenericRepository<T>, IRepositoryViewModel<TVM, T> where T : class
    {
        private readonly IDbContextFactory<OperationContext> _dbContextFactory;
        public GenericRepositoryViewModel(IDbContextFactory<OperationContext> dbContextFactory):base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public virtual IList<TVM> GetAllToViewModel()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            return dbContext.Set<T>().ToList().ToVM<T, TVM>();
        }
    }
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected IDbContextFactory<OperationContext> _dbContextFactory;
        protected DbSet<T> _table;
        public GenericRepository(IDbContextFactory<OperationContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            using var dbContext = dbContextFactory.CreateDbContext();
            _table = dbContext.Set<T>();
            dbContext.Database.EnsureCreated();
        }

        public virtual void Delete(object id)
        {
            T obj = GetValue(id);
            using var dbContext = _dbContextFactory.CreateDbContext();
            dbContext.Set<T>().Remove(obj);
            dbContext.SaveChanges();
        }

        public IList<T> GetAll()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            return dbContext.Set<T>().ToList();
        }

        public T GetValue(object id)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            return dbContext.Set<T>().Find(id);
        }

        public T Save(T entity)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            dbContext.Set<T>().Add(entity);
            dbContext.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            dbContext.Update(entity);
            dbContext.SaveChanges();
            return entity;
        }

        public async Task DeleteAsync(object id)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            object obj = await dbContext.Set<T>().FindAsync(id);
            dbContext.Set<T>().Remove((T)obj);
            await dbContext.SaveChangesAsync();
        }

        public Task<T> GetValueAsync(object id)
        {
            return Task.FromResult(GetValue(id));
        }

        public async Task<IList<T>> GetAllAsync()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.Set<T>().ToListAsync();
        }

        public Task<T> SaveAsync(T entity)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            dbContext.Set<T>().Add(entity);
            dbContext.SaveChangesAsync();
            return Task.FromResult(entity);
        }

        public Task<T> UpdateAsync(T entity)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            dbContext.Update(entity);
            dbContext.SaveChangesAsync();
            return Task.FromResult(entity);
        }
    }
}
