using RegistrationApplication.MVVM.Models;
using RegistrationApplication.MVVM.ViewModels.TrainersViewModels;

namespace RegistrationApplication.DataAccessLayer.Abstractions
{
    public interface ITrainerRepository:IGenericRepository<Trainer>
    {
        IList<TrainerViewModel> GetAllDtos();
    }
}
