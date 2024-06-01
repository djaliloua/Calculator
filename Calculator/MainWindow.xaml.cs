using Calculator.DataAccessLayer;
using Calculator.MVVM.ViewModels;
using System.Windows;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IRepository repository;
        public MainWindow(IRepository repository)
        {
            InitializeComponent();
            this.repository = repository;
            Closing += MainWindow_Closing;
            Loaded += MainWindow_Loaded;
            MainViewModel.BottomDrawerOpened += MainViewModel_BottomDrawerOpened;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ServiceLocator.MainViewModel.IsBottomDrawerOpen = false;
        }

        private void MainViewModel_BottomDrawerOpened()
        {
            keyboard.Focus();
        }

        private async void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            await repository.DeleteAll();
        }
     
    }
}