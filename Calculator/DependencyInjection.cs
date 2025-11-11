using Calculator.ApplicationLogic;
using Calculator.DataAccessLayer.Abstractions;
using Calculator.DataAccessLayer.Implementations;
using DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator;

public static class DependencyInjection
{
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
