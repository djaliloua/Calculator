using Calculator.DataAccessLayer.Abstractions;
using CalculatorModel;

namespace Calculator.ApplicationLogic;

public interface IOperationAppService
{
    void DeleteAll();
    Task<IList<Operation>> GetAllOperations();
    Operation SaveOperation(string inputText, string ouputText);
}

public class OperationAppService : IOperationAppService
{
    private readonly ICalculatorRepository _repo;
    public OperationAppService(ICalculatorRepository repo)
    {
        _repo = repo;
    }
    public Operation SaveOperation(string inputText, string ouputText)
    {
        return _repo.Save(new(inputText, ouputText));
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