using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistrationApplication.Models;
using RegistrationApplication.MVVM.ViewModels;
using RegistrationApplication.MVVM.ViewModels.TrainersViewModels;
using RegistrationApplication.MVVM.Views.TrainersView;

namespace RegistrationApplication.Extensions
{
    public static class Extensions
    {
        public static TrainerViewModel ToVM(this Trainer model) => model.Adapt<TrainerViewModel>();
        public static Trainer FromVM(this TrainerViewModel model) => model.Adapt<Trainer>();
    }
    public static class ExtensionClass
    {
        public static ServiceCollection AddView(this ServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<TrainerRegistrationWindow>();
            services.AddSingleton<TrainerProfileDetails>();
            return services;
        }
        public static ServiceCollection AddViewModel(this ServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<TrainerRegistrationViewModel>();
            services.AddSingleton<TrainerRegistrationWinViewModel>();
            services.AddSingleton<TrainersProfilesViewModel>();
            services.AddTransient<TrainerViewModel>();
            services.AddSingleton<ProfileViewModel>();
            services.AddSingleton<TrainerProfileDetailsViewModel>();
            return services;
        }
        public static ServiceCollection AddSqlServerDbContext(this ServiceCollection services, IConfiguration config)
        {
            services.AddSqlServer<Solution4Africa_MainContext>(config.GetConnectionString("DefaultConnection"));
            return services;
        }
    }
}
