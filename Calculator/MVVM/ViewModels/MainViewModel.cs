using Calculator.DataAccessLayer;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using MVVM;

namespace Calculator.MVVM.ViewModels
{
    
    public class MainViewModel:BaseViewModel
    {
        public static event Action BottomDrawerOpened;
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
            });
        }
        
        public MainViewModel(IRepository repository, ILogger<MainViewModel> _log)
        {
            logger = _log;
            logger.LogInformation("MainViewModel started......");
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
