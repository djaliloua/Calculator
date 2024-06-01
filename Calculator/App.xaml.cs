﻿using Calculator.DataAccessLayer;
using Calculator.MVVM.ViewModels;
using Calculator.MVVM.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVVM;
using NReco.Logging.File;
using System.IO;
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
            services.AddLogging(b => b.AddFile($"{Path.Combine(Helper.GetLocalPath(), "calculator.log")}", append:true));
            services.AddSingleton<KeyboardView>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<IRepository, Repository>();
            services.AddSingleton<KeyBoardViewModel>();
            services.AddSingleton<InputResultView>();
            services.AddSingleton<InputResultViewModel>();
            services.AddSingleton<BottomViewModel>();
            services.AddSingleton<MainWindow>();
            services.AddDbContext<OperationContext>(options =>
            {
                options.UseSqlite($"Data Source = {Path.Combine(Helper.GetLocalPath(), "calculator.db")}");
            });
        }

        public void Application_Startup(object sender, StartupEventArgs e)
        {
            var window = ServiceLocator.GetViewModel<MainWindow>();
            window.Show();
        }
    }
    
}