using RegistrationApplication.MVVM.Models;
using RegistrationApplication.MVVM.ViewModels.EnrolmentViewModels;

namespace RegistrationApplication.DataAccessLayer.Abstractions
{
    public interface IParticipiantRepository: IGenericRepository<Participiant>
    {
        IList<ParticipiantViewModdel> GetAllDtos();
    }
}
