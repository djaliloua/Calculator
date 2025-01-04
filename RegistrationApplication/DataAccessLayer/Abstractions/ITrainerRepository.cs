using Models.Registration;
using RegistrationApplication.MVVM.ViewModels.TrainersViewModels;
using RepositoryService.Interface;

namespace RegistrationApplication.DataAccessLayer.Abstractions
{
    public interface ITrainerRepository:IGenericRepository<Trainer>
    {
        IList<TrainerViewModel> GetAllDtos();
    }
}
