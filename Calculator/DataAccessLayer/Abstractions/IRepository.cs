using Calculator.MVVM.Models;

namespace Calculator.DataAccessLayer.Abstractions
{
    public interface IRepository:IGenericRepository<Operation>
    {
        void DeleteAllAsync();
    }
}
