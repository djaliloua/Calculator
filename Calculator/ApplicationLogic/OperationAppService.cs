using CalculatorModel;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Calculator.ApplicationLogic;

public interface IOperationAppService
{
    void DeleteAll();
    Task DeleteAfter10DaysAsync();
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
        dbContext.SaveChanges();
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

    public async Task DeleteAfter10DaysAsync()
    {
        // 1. Calculate the cutoff date (10 days ago) in C#
        var cutoffDate = DateTime.UtcNow.AddDays(-10);
        using var context = _dbFactory.CreateDbContext();
        // 2. Perform the bulk delete
        await context.Operations
            .Where(o => o.OperationDate < cutoffDate)
            .ExecuteDeleteAsync();
    }
}