using Calculator.DataAccessLayer.Abstractions;
using CalculatorModel;

namespace Calculator.ApplicationLogic;

public interface IOperationAppService
{
    void DeleteAll();
    Task<IList<Operation>> GetAllOperations();
    Task<Operation> SaveOperation(string inputText, string ouputText);
}

public class OperationAppService : IOperationAppService
{
    private readonly ICalculatorRepository _repo;
    public OperationAppService(ICalculatorRepository repo)
    {
        _repo = repo;
    }
    public async Task<Operation> SaveOperation(string inputText, string ouputText)
    {
        return await _repo.SaveAsync(new(inputText, ouputText));
    }
    public async Task<IList<Operation>> GetAllOperations()
    {
        return await _repo.GetAllAsync();
    }
    public void DeleteAll()
    {
        _repo.DeleteAllAsync();
    }
}