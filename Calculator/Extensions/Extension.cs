using Calculator.DataAccessLayer.Contexts;
using Calculator.MVVM.ViewModels;
using Calculator.MVVM.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NReco.Logging.File;
using Calculator.DataAccessLayer.Implementations;
using Calculator.MVVM.Models;
using Patterns.Abstractions;

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
            return services;
        }
        public static IServiceCollection UIExtension(this IServiceCollection services)
        {
            services.AddSingleton<KeyboardView>();
            services.AddSingleton<InputResultView>();
            services.AddSingleton<MainWindow>();
            return services;
        }
        public static IServiceCollection RepositoryExtension(this IServiceCollection services)
        {
            services.AddSingleton<Repository>();
            return services;
        }
        public static IServiceCollection ContextExtension(this IServiceCollection services)
        {
            services.AddDbContext<OperationContext>(options =>
            {
                options.UseSqlite($"Data Source = calculator.db");
            });
            return services;
        }
        public static IServiceCollection CommonsExtension(this IServiceCollection services)
        {
            services.AddLogging(b => b.AddFile($"calculator.log", append: true));
            return services;
        }
        public static IServiceCollection BIExtension(this IServiceCollection services)
        {
            services.AddScoped<ILoadService<Operation>, LoadOperationService>();
            return services;
        }
    }
}
