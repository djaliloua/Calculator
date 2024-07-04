using Calculator.DataAccessLayer;
using Calculator.DataAccessLayer.Implementations;
using Calculator.SettingsLayer.Abstractions;
using Calculator.SettingsLayer.Implementations;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using MVVM;

namespace Calculator.MVVM.ViewModels
{

    public class MainViewModel:BaseViewModel
    {
        public static event Action BottomDrawerOpened;
        private ISettings Settings;
        protected virtual void OnBottomDrawerOpened() => BottomDrawerOpened?.Invoke();
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
        private readonly ILogger<MainViewModel> logger;
        private bool _isDark;
        public bool IsDark
        {
            get => _isDark;
            set => UpdateObservable(ref _isDark, value, () =>
            {
                SetTheme(IsDark);
                Settings.SetParameter(nameof(IsDark), value);
            });
        }
        
        public MainViewModel(Repository repository, ILogger<MainViewModel> _log)
        {
            logger = _log;
            logger.LogInformation("MainViewModel started......");
            Settings = new ThemeSettings();
            IsDark = (bool)Settings.GetParameter(nameof(IsDark));
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
