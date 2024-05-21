using Calculator.MVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace Calculator.DataAccessLayer
{
    public class Repository : IRepository
    {
        private OperationContext DbContext;
        public Repository(OperationContext dbContext) 
        {
            DbContext = dbContext;
            //DbContext.Database.EnsureCreated();
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
        public async Task DeleteAll()
        {
            await DbContext.Operations.ExecuteDeleteAsync();
            await DbContext.SaveChangesAsync();
        }
        public async Task<IList<Operation>> GetAll() => await DbContext.Operations.OrderByDescending(o => o.Id)
                .ToListAsync();


    }
}
