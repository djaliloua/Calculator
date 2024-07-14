using Calculator.DataAccessLayer.Implementations;
using Calculator.MVVM.Models;
using Calculator.MVVM.ViewModels;
using System.Windows;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Repository repository;
        public MainWindow(Repository repository)
        {
            InitializeComponent();
            this.repository = repository;
            Closing += MainWindow_Closing;
            Loaded += MainWindow_Loaded;
            MainViewModel.BottomDrawerOpened += MainViewModel_BottomDrawerOpened;
            SizeChanged += MainWindow_SizeChanged;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Height > Height)
            {
                ServiceLocator.MainViewModel.IsFullScreen = true;
            }
            else
            {
                ServiceLocator.MainViewModel.IsFullScreen = false;
            }
            Focus();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ServiceLocator.MainViewModel.IsBottomDrawerOpen = false;
        }

        private void MainViewModel_BottomDrawerOpened()
        {
            //keyboard.Focus();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            repository.Delete(new Operation());
        }
    }
}