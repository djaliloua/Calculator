using Calculator.DataAccessLayer.Abstractions;
using CalculatorModel;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using RepositoryLibrary.Implementation;

namespace Calculator.DataAccessLayer.Implementations
{
    public class CalculatorRepository : GenericRepository<Operation>, ICalculatorRepository
    {
        public CalculatorRepository(IDbContextFactory<OperationContext> dbContextFactory) :base(dbContextFactory)
        {
            
        }
        public async void DeleteAllAsync()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            await dbContext.Set<Operation>().ExecuteDeleteAsync();
            await dbContext.SaveChangesAsync();
        }

        public override void Delete(object id)
        {
            using var dbContext = _dbContextFactory.CreateDbContext();
            dbContext.Set<Operation>()
                .FromSql($"delete from OperationsTable\r\nwhere JULIANDAY(date('now')) - JULIANDAY(date(OperationDate)) > 10;");
        }
    }
}
