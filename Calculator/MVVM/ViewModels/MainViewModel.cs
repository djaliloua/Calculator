using Calculator.DataAccessLayer.Implementations;
using Calculator.MVVM.Views.Standard;
using Calculator.SettingsLayer.Abstractions;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using MVVM;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace Calculator.MVVM.ViewModels
{
    public record ControlData(string Title, string Kind, UserControl UserControl);
    public class MainViewModel:BaseViewModel
    {
        #region Private properties
        private ISettingsManager _settings;
        public static event Action LeftDrawerClosed;
        private readonly ILogger<MainViewModel> _logger;

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
        public MainViewModel(CalculatorRepository repository, ILogger<MainViewModel> _log, ISettingsManager settings)
        {
            Controls ??= new();
            _paletteHelper = new PaletteHelper();
            Init();
            _logger = _log;
            _logger.LogInformation("MainViewModel started......");
            _settings = settings;
            IsDark = (bool)settings.GetParameter(nameof(IsDark));
        }
        #endregion

        #region Private methods
        private void Init()
        {
            Controls.Add(new ControlData("Standard", "Calculator", new StandardCalculator()));
            // add About screen
            Controls.Add(new ControlData("About", "InformationCircle", new Calculator.MVVM.Views.OtherControls.About()));
            if(Controls.Count > 0)
            {
                SelectedControl = Controls[0];
            }
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
