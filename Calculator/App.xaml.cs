using Calculator.Extensions;
using Microsoft.Extensions.DependencyInjection;
using MVVM;
using System.Windows;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public App()
        {
            ServiceCollection services = new ServiceCollection();

            services
                .CommonsExtension()
                .ViewModelsExtension()
                .ContextExtension()
                .BIExtension()
                .RepositoryExtension()
                .UIExtension()
                ;
            
            ServiceProvider = services.BuildServiceProvider();
        }
        

        public void Application_Startup(object sender, StartupEventArgs e)
        {
            if (Helper.AvoidMore())
            {
                var window = ServiceLocator.GetService<MainWindow>();
                window.Show();
            }
            else
                MessageBox.Show("An Instance of the Application is Already Running");
        }
    }
    
}