using CalculatorModel;
using RepositoryLibrary.Interface;

namespace Calculator.DataAccessLayer.Abstractions
{
    public interface ICalculatorRepository:IGenericRepository<Operation>
    {
        void DeleteAllAsync();
    }
}
