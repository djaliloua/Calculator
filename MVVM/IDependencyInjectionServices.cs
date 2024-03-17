using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace MVVM
{
    public interface IDependencyInjectionServices
    {
        public static IServiceProvider ServiceProvider { get; set; }
        void ConfigureServices(ServiceCollection services);
        void Application_Startup(object sender, StartupEventArgs e);
    }
}
