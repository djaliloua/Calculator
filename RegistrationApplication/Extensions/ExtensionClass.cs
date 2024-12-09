using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistrationApplication.DataAccessLayer.DataContext;
using RegistrationApplication.MVVM.Models;
using RegistrationApplication.MVVM.ViewModels;
using RegistrationApplication.MVVM.ViewModels.CountryViewModels;
using RegistrationApplication.MVVM.ViewModels.EnrolmentViewModels;
using RegistrationApplication.MVVM.ViewModels.TrainersViewModels;

namespace RegistrationApplication.Extensions
{
    public static class Extensions
    {
        public static ParticipiantViewModdel ToVM(this Participiant model) => model.Adapt<ParticipiantViewModdel>();
        public static Participiant FromVM(this ParticipiantViewModdel model) => model.Adapt<Participiant>();
        public static TrainerViewModel ToVM(this Trainer model) => model.Adapt<TrainerViewModel>();
        public static Trainer FromVM(this TrainerViewModel model) => model.Adapt<Trainer>();
        public static CountryViewModel ToVM(this Country model) => model.Adapt<CountryViewModel>();
        public static Country FromVM(this CountryViewModel model) => model.Adapt<Country>();
        public static IList<CountryViewModel> ToVM(this IList<Country> model) => model.Adapt<List<CountryViewModel>>();
        public static IList<TDestination> ToVM<TSource, TDestination>(this IList<TSource> model) => model.Adapt<List<TDestination>>();
    }
    public static class ExtensionClass
    {
        public static ServiceCollection AddView(this ServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            return services;
        }
        public static ServiceCollection AddViewModel(this ServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<TrainerRegistrationViewModel>();
            services.AddSingleton<TrainersProfilesViewModel>();
            services.AddTransient<TrainerViewModel>();
            services.AddSingleton<TrainerFormViewModel>();
            services.AddSingleton<CountryViewModelUI>();
            services.AddSingleton<EnrolmentViewModel>();
            services.AddSingleton<EnrolmentHomeViewModel>();
            services.AddSingleton<LoadableParticipiantViewModdel>();
            return services;
        }
        public static ServiceCollection AddSqlServerDbContext(this ServiceCollection services, IConfiguration config)
        {
            services.AddSqlServer<TrainerDataContext>(config.GetConnectionString("DefaultConnection"));
            return services;
        }
    }
}
