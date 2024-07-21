using Calculator.DataAccessLayer.Implementations;
using Calculator.MVVM.Models;
using Calculator.MVVM.ViewModels;
using Calculator.MVVM.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Repository _repositoryOperation;
        private UserControl _userControl;
        private double _currentWidth;
        private double _currentHeight;
        private double _widthThreshold;
        public MainWindow(Repository repository)
        {
            InitializeComponent();
            _repositoryOperation = repository;
            _currentWidth = Width;
            _widthThreshold = 250;
            SizeChanged += MainWindow_SizeChanged;
            Closing += MainWindow_Closing;
            Loaded += MainWindow_Loaded;
            MainViewModel.BottomDrawerOpened += MainViewModel_BottomDrawerOpened;
        }
        private void MainViewModel_BottomDrawerOpened()
        {
            if(content.Content is SmallScreenLayout layout)
            {
                layout.keyboard.Focus();
            }
           
        }
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Height > Height || (e.NewSize.Width - _currentWidth) > _widthThreshold)
            {
                ServiceLocator.MainViewModel.IsFullScreen = true;
            }
            else
            {
                ServiceLocator.MainViewModel.IsFullScreen = false;
            }
           
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ServiceLocator.MainViewModel.IsBottomDrawerOpen = false;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _repositoryOperation.Delete(new Operation());
        }
    }
}