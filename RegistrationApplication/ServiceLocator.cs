using Microsoft.Extensions.DependencyInjection;
using RegistrationApplication.MVVM.ViewModels;
using RegistrationApplication.MVVM.ViewModels.TrainersViewModels;

namespace RegistrationApplication
{
    public class ServiceLocator
    {
        public static MainViewModel MainViewModel => GetService<MainViewModel>();
        public static TrainerProfileDetailsViewModel TrainerProfileDetailsViewModel => GetService<TrainerProfileDetailsViewModel>();
        public static TrainerViewModel TrainerViewModel => GetService<TrainerViewModel>();
        public static ProfileViewModel ProfileViewModel => GetService<ProfileViewModel>();
        public static TrainersProfilesViewModel TrainersProfilesViewModel => GetService<TrainersProfilesViewModel>();
        public static TrainerRegistrationViewModel TrainerRegistrationViewModel => GetService<TrainerRegistrationViewModel>();
        public static TrainerRegistrationWinViewModel TrainerRegistrationWinViewModel => GetService<TrainerRegistrationWinViewModel>();
        public static T GetService<T>() where T : class => App.ServiceProvider.GetRequiredService<T>();
    }
}
