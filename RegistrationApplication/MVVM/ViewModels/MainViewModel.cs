using RegistrationApplication.MVVM.Views.TrainersView;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace RegistrationApplication.MVVM.ViewModels
{
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
        public ObservableCollection<ControlData> Controls { get; }
        private ControlData _selectedControl;
        public ControlData SelectedControl
        {
            get => _selectedControl;
            set => UpdateObservable(ref _selectedControl, value);
        }
        public string Title { get; set; }
        public MainViewModel()
        {
            Title = "Registration App";
            Controls ??= new();
            Init();
        }
        private void Init()
        {
            Controls.Add(new ControlData("Trainers", "AccountMultiplePlus", new TrainerRegistrationView()));
            if (Controls.Count > 0)
            {
                SelectedControl = Controls[0];
            }
        }
    }
}
