using System.Windows.Input;

namespace RegistrationApplication.MVVM.ViewModels.CourseViewModel
{
    public class CourseFormViewModel:BaseViewModel
    {
        public bool IsSave { get; set; } = true;
        private CourseViewModel _course = new();
        public CourseViewModel Course
        {
            get => _course;
            set => UpdateObservable(ref _course, value);
        }
        #region Commands
        public ICommand CancelCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand SaveAndAddNewCommand { get; private set; }
        public ICommand DeleteRowCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        #endregion
        public CourseFormViewModel()
        {
            CancelCommand = new DelegateCommand(OnCancel);
        }
        private void OnCancel(object parameter)
        {
            ServiceLocator.CourseViewModelUI.SelectedIndex--;
        }
    }
}
