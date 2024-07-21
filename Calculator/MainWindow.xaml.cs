using Calculator.DataAccessLayer.Implementations;
using Calculator.MVVM.Models;
using Calculator.MVVM.ViewModels;
using Calculator.MVVM.Views;
using Calculator.SettingsLayer.Abstractions;
using Calculator.SettingsLayer.Implementations;
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
        private double _currentWidth;
        private double _widthThreshold;
        private ISettingsManager _settings;
        public MainWindow(Repository repository, ISettingsManager settings)
        {
            InitializeComponent();
            _repositoryOperation = repository;
            _currentWidth = Width;
            _settings = settings;
            Width = (double)_settings.GetParameter(nameof(Width));
            Height = (double)_settings.GetParameter(nameof(Height));
            _widthThreshold = (double)_settings.GetParameter("WidthThreshold");
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

           if(content.Content is FullScreenLayout fulllayout)
            {
                fulllayout.calculator.keyboard.Focus();
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
            _settings.SetParameter(nameof(Width), e.NewSize.Width);
            _settings.SetParameter(nameof(Height), e.NewSize.Height);
            _settings.SetParameter("IsFullScreen", ServiceLocator.MainViewModel.IsFullScreen);
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