using CalculatorModel;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Calculator.ApplicationLogic;

public interface IOperationAppService
{
    void DeleteAll();
    void DeleteAfter10Days();
    Task<IList<Operation>> GetAllOperations();
    Operation SaveOperation(string inputText, string ouputText);
}

public class OperationAppService : IOperationAppService
{
    private readonly IDbContextFactory<OperationContext> _dbFactory;
    public OperationAppService(IDbContextFactory<OperationContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }
    public Operation SaveOperation(string inputText, string ouputText)
    {
        Operation op = new(inputText, ouputText);
        using var dbContext = _dbFactory.CreateDbContext();
        var operations = dbContext.Set<Operation>();
        operations.Add(op);
        return op;
    }
    public async Task<IList<Operation>> GetAllOperations()
    {
        using var dbContext = _dbFactory.CreateDbContext();
        return await dbContext.Operations.ToListAsync();
    }
    public void DeleteAll()
    {
        using var dbContext = _dbFactory.CreateDbContext();
        dbContext.Operations.ExecuteDelete();
    }

    public void DeleteAfter10Days()
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-10);
        using var dbContext = _dbFactory.CreateDbContext();
        dbContext.Operations
            .Where(op => op.OperationDate < cutoffDate)
            .ExecuteDelete();
    }
}