using Calculator.MVVM.ViewModels;
using Calculator.MVVM.Views;
using Microsoft.Extensions.DependencyInjection;
using MVVM;
using System.Windows;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IDependencyInjectionServices
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }
        public void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<KeyboardView>();
            services.AddSingleton<KeyBoardViewModel>();
            services.AddSingleton<InputResultView>();
            services.AddSingleton<InputResultViewModel>();
            services.AddSingleton<MainWindow>();
        }

        public void Application_Startup(object sender, StartupEventArgs e)
        {
            var window = ServiceProvider.GetRequiredService<MainWindow>();
            window.Show();
        }
    }
    
}