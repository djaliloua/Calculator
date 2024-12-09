using Microsoft.Extensions.DependencyInjection;
using RegistrationApplication.MVVM.ViewModels;
using RegistrationApplication.MVVM.ViewModels.CountryViewModels;
using RegistrationApplication.MVVM.ViewModels.EnrolmentViewModels;
using RegistrationApplication.MVVM.ViewModels.TrainersViewModels;

namespace RegistrationApplication
{
    public class ServiceLocator
    {
        #region UI

        #endregion

        #region View Models
        public static EnrolmentViewModel EnrolmentViewModel => GetService<EnrolmentViewModel>();
        public static EnrolmentHomeViewModel EnrolmentHomeViewModel => GetService<EnrolmentHomeViewModel>();
        public static CountryViewModelUI CountryViewModelUI => GetService<CountryViewModelUI>();
        public static TrainerFormViewModel TrainerFormViewModel => GetService<TrainerFormViewModel>();
        public static MainViewModel MainViewModel => GetService<MainViewModel>();
        public static TrainerViewModel TrainerViewModel => GetService<TrainerViewModel>();
        public static TrainersProfilesViewModel TrainersProfilesViewModel => GetService<TrainersProfilesViewModel>();
        public static TrainerRegistrationViewModel TrainerRegistrationViewModel => GetService<TrainerRegistrationViewModel>();
        #endregion

        public static T GetService<T>() where T : class => App.ServiceProvider.GetRequiredService<T>();
    }
}
