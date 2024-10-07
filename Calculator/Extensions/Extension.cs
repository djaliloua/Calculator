using Calculator.DataAccessLayer.Contexts;
using Calculator.MVVM.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NReco.Logging.File;
using Calculator.DataAccessLayer.Implementations;
using Calculator.MVVM.Models;
using Patterns.Abstractions;
using Calculator.SettingsLayer.Abstractions;
using Calculator.SettingsLayer.Implementations;
using Calculator.MVVM.Views.Standard;
using Calculator.MVVM.ViewModels.Standard;

namespace Calculator.Extensions
{
    public static class Extension
    {
        public static IServiceCollection ViewModelsExtension(this IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<KeyBoardViewModel>();
            services.AddSingleton<InputResultViewModel>();
            services.AddSingleton<BottomViewModel>();
            services.AddSingleton<OperationListViewModel>();
            services.AddSingleton<StandardCalculatorViewModel>();
            return services;
        }
        public static IServiceCollection UIExtension(this IServiceCollection services)
        {
            services.AddSingleton<KeyboardView>();
            services.AddSingleton<InputResultView>();
            services.AddScoped<SmallScreenLayout>();
            services.AddScoped<FullScreenLayout>();
            services.AddSingleton<MainWindow>();
            return services;
        }
        public static IServiceCollection RepositoryExtension(this IServiceCollection services)
        {
            services.AddTransient<Repository>();
            return services;
        }
        public static IServiceCollection ContextExtension(this IServiceCollection services)
        {
            services.AddTransient<OperationContext>();
            //services.AddDbContext<OperationContext>(options =>
            //{
            //    options.UseSqlite($"Data Source = calculator.db");
            //});
            return services;
        }
        public static IServiceCollection CommonsExtension(this IServiceCollection services)
        {
            services.AddLogging(b => b.AddFile($"calculator.log", append: true));
            services.AddScoped<ISettingsManager, SettingsManager>();
            return services;
        }
        public static IServiceCollection BIExtension(this IServiceCollection services)
        {

            return services;
        }
    }
}
