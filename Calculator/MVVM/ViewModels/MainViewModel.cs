using Calculator.Auth;
using Calculator.MVVM.Views;
using Calculator.MVVM.Views.Standard;
using Calculator.SettingsLayer.Abstractions;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MVVM;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Calculator.MVVM.ViewModels
{
    public record ControlData(string Title, string Kind, UserControl UserControl);
    
    public class MainViewModel : BaseViewModel
    {
        #region Private properties
        private ISettingsManager _settings;
        public static event Action LeftDrawerClosed;
        private readonly ILogger<MainViewModel> _logger;
        private readonly IAuthService _authService;

        protected virtual void OnLeftDrawerClosed() => LeftDrawerClosed?.Invoke();
        #endregion
        private bool _isDark;
        private readonly PaletteHelper _paletteHelper;
        public bool IsDark
        {
            get => _isDark;
            set => UpdateObservable(ref _isDark, value, () =>
            {
                SetTheme(IsDark);
                _settings.SetParameter(nameof(IsDark), value);
            });
        }

        private bool _isAuthenticated;
        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            private set => UpdateObservable(ref _isAuthenticated, value, () =>
            {
                CommandManager.InvalidateRequerySuggested();
            });
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            private set => UpdateObservable(ref _userName, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand LogoutCommand { get; }

        public ObservableCollection<ControlData> Controls { get; }
        private ControlData _selectedControl;
        public ControlData SelectedControl
        {
            get => _selectedControl;
            set => UpdateObservable(ref _selectedControl, value, () =>
            {
                CloseLeftDrawer = false;
            });
        }
        private bool closeLeftDrawer;
        public bool CloseLeftDrawer
        {
            get => closeLeftDrawer;
            set => UpdateObservable(ref closeLeftDrawer, value, () =>
            {
                if (!CloseLeftDrawer)
                {
                    OnLeftDrawerClosed();
                }
            });
        }

        #region Constructor
        public MainViewModel(
            ILogger<MainViewModel> _log,
            ISettingsManager settings,
            [FromKeyedServices("provider")]IAuthService authService)
        {
            Controls ??= new();
            _paletteHelper = new PaletteHelper();
            Init();

            _logger = _log;
            _logger.LogInformation("MainViewModel started......");
            _settings = settings;
            _authService = authService;

            IsDark = (bool)settings.GetParameter(nameof(IsDark));
            UserName = "Guest";
            IsAuthenticated = false;

            LoginCommand = new DelegateCommand(async _ => await LoginAsync(), _ => !IsAuthenticated);
            LogoutCommand = new DelegateCommand(async _ => await LogoutAsync(), _ => IsAuthenticated);
        }
        #endregion

        #region Private methods
        private void Init()
        {
            Controls.Add(new ControlData("Standard", "Calculator", new StandardCalculator()));
            Controls.Add(new ControlData("Settings", "Cog", new SettingsView())); // Add this line
            Controls.Add(new ControlData("About", "InformationCircle", new About()));
            if (Controls.Count > 0)
            {
                SelectedControl = Controls[0];
            }
        }

        private async Task LoginAsync()
        {
            var result = await _authService.LoginAsync();
            if (!result.IsAuthenticated)
            {
                _logger.LogWarning("Login failed.");
                return;
            }

            UserName = result.UserName;
            IsAuthenticated = true;
        }

        private async Task LogoutAsync()
        {
            await _authService.LogoutAsync();
            UserName = "Guest";
            IsAuthenticated = false;
        }

        private void SetTheme(bool isDark)
        {
            Theme theme = _paletteHelper.GetTheme();
            if (isDark)
            {
                theme.SetPrimaryColor(Color.FromArgb(255, 103, 58, 183));
                theme.SetBaseTheme(BaseTheme.Dark);
            }
            else
            {
                theme.SetPrimaryColor(Color.FromArgb(255, 103, 58, 183));
                theme.SetBaseTheme(BaseTheme.Light);
            }
            _paletteHelper.SetTheme(theme);
        }

        #endregion
    }
}
