using Calculator.DataAccessLayer.Implementations;
using Calculator.MVVM.Models;
using Calculator.MVVM.ViewModels;
using Calculator.MVVM.Views.Standard;
using Calculator.SettingsLayer.Abstractions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly OperationRepository _repositoryOperation;
        private double _currentWidth;
        private double _widthThreshold;
        private ISettingsManager _settings;
        public MainWindow(OperationRepository repository, ISettingsManager settings)
        {
            InitializeComponent();
            _repositoryOperation = repository;
            _settings = settings;
            _currentWidth = MinWidth;
            Width = (double)_settings.GetParameter(nameof(Width));
            Height = (double)_settings.GetParameter(nameof(Height));
            WindowState = (WindowState)(int)_settings.GetParameter(nameof(WindowState));
            _widthThreshold = (double)_settings.GetParameter("WidthThreshold");
            SizeChanged += MainWindow_SizeChanged;
            Closing += MainWindow_Closing;
            StateChanged += MainWindow_StateChanged;
            MainViewModel.LeftDrawerClosed += MainViewModel_LeftDrawerClosed;
        }

        private void MainViewModel_LeftDrawerClosed()
        {
            PopupBox_Closed(null, null);
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen flag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;

            while (dependencyObject != null)
            {
                if (dependencyObject is ContentControl) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }
        private void MainWindow_StateChanged(object sender, EventArgs e)
        {
            _settings.SetParameter("WindowState", (int)WindowState);
        }
        
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Height > Height 
                || (e.NewSize.Width - _currentWidth) > _widthThreshold)
            {
                ServiceLocator.StandardCalculatorViewModel.IsFullScreen = true;
            }
            else
            {
                ServiceLocator.StandardCalculatorViewModel.IsFullScreen = false;
            }
            _settings.SetParameter(nameof(Width), e.NewSize.Width);
            _settings.SetParameter(nameof(Height), e.NewSize.Height);
            _settings.SetParameter("IsFullScreen", ServiceLocator.StandardCalculatorViewModel.IsFullScreen);
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _repositoryOperation.Delete(new Operation());
        }

        private void PopupBox_Closed(object sender, RoutedEventArgs e)
        {
            if(content.Content is StandardCalculator standard)
            {
                if (standard.content.Content is SmallScreenLayout layout)
                {
                    layout.keyboard.Focus();
                }

                if (standard.content.Content is FullScreenLayout fulllayout)
                {
                    fulllayout.calculator.keyboard.Focus();
                }
            }
            
        }
    }
}