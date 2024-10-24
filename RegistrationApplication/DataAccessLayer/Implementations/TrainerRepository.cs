using Mapster;
using RegistrationApplication.DataAccessLayer.Abstractions;
using RegistrationApplication.Extensions;
using RegistrationApplication.MVVM.Models;
using RegistrationApplication.MVVM.ViewModels.TrainersViewModels;

namespace RegistrationApplication.DataAccessLayer.Implementations
{
    public class TrainerRepository : GenericRepository<Trainer>, ITrainerRepository
    {
        public IList<TrainerViewModel> GetAllDtos()
        {
            return _table.ToList().ToVM();
        }
    }
}
