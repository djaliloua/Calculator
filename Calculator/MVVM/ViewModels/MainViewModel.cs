using Calculator.DataAccessLayer.Implementations;
using Calculator.MVVM.Views;
using Calculator.SettingsLayer.Abstractions;
using Calculator.SettingsLayer.Implementations;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using MVVM;
using System.Windows.Controls;

namespace Calculator.MVVM.ViewModels
{

    public class MainViewModel:BaseViewModel
    {
        #region Private properties
        public static event Action BottomDrawerOpened;
        private ISettingsManager _settings;
        private readonly ILogger<MainViewModel> logger;
        #endregion

        protected virtual void OnBottomDrawerOpened() => BottomDrawerOpened?.Invoke();

        private bool _canMinimize;
        public bool IsFullScreen
        {
            get => _canMinimize;
            set => UpdateObservable(ref _canMinimize, value, () =>
            {
                if (_canMinimize)
                {
                    UserControl = ServiceLocator.GetService<FullScreenLayout>();
                }
                else
                {
                    UserControl = ServiceLocator.GetService< SmallScreenLayout>();
                }
            });
        }
        private bool _isBottomDrawerOpen;
        public bool IsBottomDrawerOpen
        {
            get => _isBottomDrawerOpen;
            set => UpdateObservable(ref _isBottomDrawerOpen, value, () =>
            {
                if(value == false)
                {
                    OnBottomDrawerOpened();
                }
            });
        }
        private UserControl _userControl;
        public UserControl UserControl
        {
            get => _userControl;
            set => UpdateObservable(ref _userControl, value);
        }
        
        private bool _isDark;
        public bool IsDark
        {
            get => _isDark;
            set => UpdateObservable(ref _isDark, value, () =>
            {
                SetTheme(IsDark);
                _settings.SetParameter(nameof(IsDark), value);
            });
        }
        
        public MainViewModel(Repository repository, ILogger<MainViewModel> _log, ISettingsManager settings)
        {
            logger = _log;
            logger.LogInformation("MainViewModel started......");
            _settings = settings;
            IsDark = (bool)settings.GetParameter(nameof(IsDark));
        }
        private void SetTheme(bool isDark)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(isDark ? BaseTheme.Dark : BaseTheme.Light);
            paletteHelper.SetTheme(theme);
        }
    }
}
