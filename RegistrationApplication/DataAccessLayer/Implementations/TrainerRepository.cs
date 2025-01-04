using Mapster;
using RegistrationApplication.DataAccessLayer.Abstractions;
using RegistrationApplication.Extensions;
using Models.Registration;
using RegistrationApplication.MVVM.ViewModels.TrainersViewModels;

namespace RegistrationApplication.DataAccessLayer.Implementations
{
    public class TrainerRepository : GenericRepositoryDto<TrainerViewModel, Trainer>
    {
        //public IList<TrainerViewModel> GetAllDtos()
        //{
        //    return _table.ToList().ToVM();
        //}
    }
}
