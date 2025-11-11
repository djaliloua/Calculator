using Calculator.ApplicationLogic;
using Calculator.DataAccessLayer.Abstractions;
using Calculator.DataAccessLayer.Implementations;
using Calculator.MVVM.ViewModels;
using Calculator.MVVM.ViewModels.Standard;
using Calculator.MVVM.Views.Standard;
using Calculator.SettingsLayer.Abstractions;
using Calculator.SettingsLayer.Implementations;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NReco.Logging.File;

namespace Calculator;

public static class DependencyInjection
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
        services.AddTransient<CalculatorRepository>();
        return services;
    }
    public static IServiceCollection ContextExtension(this IServiceCollection services)
    {


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
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddDbContextFactory<OperationContext>(options =>
        {
            if (!options.IsConfigured)
            {
                IConfiguration Configuration = new ConfigurationBuilder()
                .AddUserSecrets<OperationContext>()
                .Build();
                options.UseSqlite(Configuration.GetConnectionString("Calculator_db2"));
            }
        });
        services.AddScoped<IOperationAppService, OperationAppService>();
        services.AddScoped<ICalculatorRepository, CalculatorRepository>();
        return services;
    }
}
