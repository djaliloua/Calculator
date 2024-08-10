using Calculator.DataAccessLayer.Implementations;
using Calculator.MVVM.Views;
using Calculator.MVVM.Views.Standard;
using Calculator.SettingsLayer.Abstractions;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.Logging;
using MVVM;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Calculator.MVVM.ViewModels
{
    public class ControlData
    {
        public string Title { get; set; }
        public string Kind { get; set; }
        public UserControl UserControl { get; set; }
        public ControlData()
        {
            
        }
        public ControlData(string title,string kind, UserControl userControl)
        {
            UserControl = userControl;
            Title = title;
            Kind = kind;
        }
    }
    public class MainViewModel:BaseViewModel
    {
        #region Private properties
        private ISettingsManager _settings;
        private readonly ILogger<MainViewModel> _logger;
        #endregion
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
            set => UpdateObservable(ref closeLeftDrawer, value);
        }

        #region Constructor
        public MainViewModel(Repository repository, ILogger<MainViewModel> _log, ISettingsManager settings)
        {
            Controls ??= new();
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
            if(Controls.Count > 0)
            {
                SelectedControl = Controls[0];
            }
        }
        private void SetTheme(bool isDark)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(isDark ? BaseTheme.Dark : BaseTheme.Light);
            paletteHelper.SetTheme(theme);
        }
        #endregion
    }
}
