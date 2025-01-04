using Models.Registration;
using RegistrationApplication.MVVM.ViewModels.TrainersViewModels;
using RepositoryService.Implementation;

namespace RegistrationApplication.DataAccessLayer.Implementations
{
    public class TrainerRepository : GenericRepositoryViewModel<TrainerViewModel, Trainer>
    {
        //public IList<TrainerViewModel> GetAllDtos()
        //{
        //    return _table.ToList().ToVM();
        //}
    }
}
