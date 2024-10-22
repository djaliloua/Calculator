using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistrationApplication.Extensions;
using System.Configuration;
using System.Data;
using System.Windows;

namespace RegistrationApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static IConfiguration Configuration { get; private set; }
        public App()
        {
            Configuration = new ConfigurationBuilder()
                .AddUserSecrets<App>()
                .Build()
                ;

            ServiceCollection services = new ServiceCollection();
            services
                .AddViewModel()
                //.AddSqlServerDbContext(Configuration)
                .AddView();

            ServiceProvider = services.BuildServiceProvider();
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var window = ServiceProvider.GetRequiredService<MainWindow>();
            window.Show();
        }
    }

}
