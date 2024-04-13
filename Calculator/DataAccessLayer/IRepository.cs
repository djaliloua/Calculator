using Calculator.MVVM.Models;

namespace Calculator.DataAccessLayer
{
    public interface IRepository
    {
        Task<IList<Operation>> GetAll();
        Task SaveItem(Operation operation);
        Task UpdateItem(Operation operation);
        Task DeleteAll();
        Task<bool> IsAlreadyPresent(string input);
    }
}
