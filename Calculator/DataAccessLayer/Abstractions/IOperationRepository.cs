using Calculator.MVVM.Models;

namespace Calculator.DataAccessLayer.Abstractions
{
    public interface IOperationRepository:IGenericRepository<Operation>
    {
        void DeleteAllAsync();
    }
}
