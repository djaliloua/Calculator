using System.Windows;
using System.Windows.Input;

namespace RegistrationApplication.MVVM.ViewModels.CourseViewModel
{
    public class CourseFormViewModel:BaseViewModel
    {
        public bool IsSave { get; set; } = true;
        private CourseViewModel _course;
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
            _course = new();
            CancelCommand = new DelegateCommand(OnCancel);
            SaveCommand = new DelegateCommand(OnSave);
        }
        private void OnSave(object parameter)
        {
            if(Course.IsChanged)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to save?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                {
                    
                }
            }
            else
            {
                Notifier.Show("No changes");
            }
            ServiceLocator.CourseViewModelUI.GoBack();
        }
        private void OnCancel(object parameter)
        {
            if (Course.IsChanged)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to discard changes?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(result == MessageBoxResult.Yes)
                {
                    Course.CancelEdit();
                    Course.AcceptChanges();
                    Notifier.Show("Changed");
                }
                else
                {
                    Notifier.Show("Changes not saved");
                }
                
            }
            ServiceLocator.CourseViewModelUI.GoBack();
        }
    }
}
