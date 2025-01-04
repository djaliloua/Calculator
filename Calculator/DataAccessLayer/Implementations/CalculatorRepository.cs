using Calculator.DataAccessLayer.Abstractions;
using CalculatorModel;
using Microsoft.EntityFrameworkCore;
using RepositoryLibrary.Implementation;

namespace Calculator.DataAccessLayer.Implementations
{
    public class CalculatorRepository : GenericRepository<Operation>, ICalculatorRepository
    {
        public CalculatorRepository():base()
        {
            
        }
        public async void DeleteAllAsync()
        {
            await _table.ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();
        }

        public override void Delete(object id)
        {
           _table
                .FromSql($"delete from OperationsTable\r\nwhere JULIANDAY(date('now')) - JULIANDAY(date(OperationDate)) > 10;");
        }
    }
}
