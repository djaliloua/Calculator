using Calculator.MVVM.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Calculator.DataAccessLayer
{
    public class Repository : IRepository
    {
        private OperationContext DbContext;
        public Repository(OperationContext dbContext) 
        {
            DbContext = dbContext;
            DbContext.Database.EnsureCreated();
        }
        public async Task DeleteAllAsync()
        {
            await DbContext.Operations.ExecuteDeleteAsync();
            await DbContext.SaveChangesAsync();
        }
        public async Task<Operation> UpdateItem(Operation operation)
        {
            if (operation.Id != 0)
            {
                await DbContext.Operations.ExecuteUpdateAsync(
                    s => s.SetProperty(b => b.OpValue, b => operation.OpValue)
                    .SetProperty(b => b.OpResult, b=> operation.OpResult)
                    );
                await DbContext.SaveChangesAsync();
            }
            return operation;
        }
        public async Task<bool> IsAlreadyPresent(string input)
        {
            await Task.Delay(1);
            var data = DbContext.Operations.Where(o => o.OpValue == input).ToList();
            return data.Count()!=0;
        }
        public async Task<Operation> SaveItem(Operation operation)
        {
            await DbContext.Operations.AddAsync(operation);
            await DbContext.SaveChangesAsync();
            return operation;
        }
        public async Task DeleteItems()
        {
            await Task.Delay(1);
            DbContext.Operations
                .FromSql($"delete from OperationsTable\r\nwhere JULIANDAY(date('now')) - JULIANDAY(date(OperationDate)) > 31;");
        }
        private static bool Diff(DateTime t1, DateTime t2)
        {
            TimeSpan difference = t1 - t2;
            return difference.Days > 31;
        }
        public async Task<IList<Operation>> GetAll() => await DbContext.Operations.OrderByDescending(o => o.Id)
                .ToListAsync();


    }
}
