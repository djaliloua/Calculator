using Models.Registration;
using RegistrationApplication.MVVM.ViewModels.EnrolmentViewModels;
using RepositoryService.Interface;

namespace RegistrationApplication.DataAccessLayer.Abstractions
{
    public interface IParticipiantRepository: IGenericRepository<Participiant>
    {
        IList<ParticipiantViewModdel> GetAllDtos();
    }
}
