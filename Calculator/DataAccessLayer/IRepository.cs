using Calculator.MVVM.Models;

namespace Calculator.DataAccessLayer
{
    public interface IRepository
    {
        Task<IList<Operation>> GetAll();
        Task<Operation> SaveItem(Operation operation);
        Task<Operation> UpdateItem(Operation operation);
        Task DeleteAll();
    }
}
