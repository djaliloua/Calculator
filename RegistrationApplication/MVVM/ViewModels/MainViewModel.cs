using MaterialDesignThemes.Wpf;
using RegistrationApplication.MVVM.Views.CountryView;
using RegistrationApplication.MVVM.Views.CourseViews;
using RegistrationApplication.MVVM.Views.EnrolmentViews;
using RegistrationApplication.MVVM.Views.TrainersView;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace RegistrationApplication.MVVM.ViewModels
{
    public interface ISettingsManager
    {
        void SetParameter(string name, object value);
        object GetParameter(string name);
    }
    public class SettingsManager : ISettingsManager
    {
        public object GetParameter(string name)
        {
            return Properties.Settings.Default[name];
        }

        public void SetParameter(string name, object value)
        {
            Properties.Settings.Default[name] = value;
            Properties.Settings.Default.Save();
        }
    }
    public class ControlData
    {
        public string Title { get; set; }
        public string Kind { get; set; }
        public UserControl UserControl { get; set; }
        public ControlData()
        {

        }
        public ControlData(string title, string kind, UserControl userControl)
        {
            UserControl = userControl;
            Title = title;
            Kind = kind;
        }
    }
    public class MainViewModel:BaseViewModel
    {
        private ISettingsManager _settings;
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
        private bool _isLeftDrawer;
        public bool IsLeftDrawer
        {
            get => _isLeftDrawer;
            set => UpdateObservable(ref _isLeftDrawer, value);
        }
        public ObservableCollection<ControlData> Controls { get; }
        private ControlData _selectedControl;
        public ControlData SelectedControl
        {
            get => _selectedControl;
            set => UpdateObservable(ref _selectedControl, value, () =>
            {
                IsLeftDrawer = !IsLeftDrawer;
            });
        }
        public string Title { get; set; }
        public MainViewModel()
        {
            Title = "Registration App";
            _paletteHelper = new PaletteHelper();
            _settings = new SettingsManager();
            IsDark = (bool)_settings.GetParameter(nameof(IsDark));
            Controls ??= new();
            Init();
            IsLeftDrawer = false;
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
        private void Init()
        {
            Controls.Add(new ControlData("Enrol", "AccountPlus", new Enrolment()));
            Controls.Add(new ControlData("Trainers", "AccountMultiplePlus", new TrainerRegistrationView()));
            Controls.Add(new ControlData("Country", "CreditCardWirelessOutline", new CountryView()));
            if (Controls.Count > 0)
            {
                SelectedControl = Controls[0];
            }
        }
    }
}
