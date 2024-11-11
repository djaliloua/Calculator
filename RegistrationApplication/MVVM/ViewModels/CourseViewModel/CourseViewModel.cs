using Patterns.Implementation;
using RegistrationApplication.DataAccessLayer;
using RegistrationApplication.Extensions;
using System.Windows.Input;

namespace RegistrationApplication.MVVM.ViewModels.CourseViewModel
{
    public class BaseCourseViewModel :BaseViewModel, IBaseViewModel<CourseViewModel>
    {
        private CourseViewModel _orignalObject;
        public CourseViewModel OriginalObject
        {
            get => _orignalObject;
            protected set
            {
                _orignalObject = value;
            }
        }

        public virtual void BeginEdit()
        {
            throw new NotImplementedException();
        }

        public virtual void CancelEdit()
        {
            
        }

        public virtual void EndEdit()
        {
            if (!_inEdit) return;

            // Commit changes by clearing the backup
            OriginalObject = null;
            _inEdit = false;
        }
    }
    public class CourseProformaViewModel:BaseViewModel, IClone<CourseProformaViewModel>
    {
        public int CourseProformaId { get; set; }
        public int CourseId { get; set; }

        private CourseViewModel _course;
        public CourseViewModel Course
        {
            get => _course;
            set => UpdateObservable(ref _course, value);
        }

        public CourseProformaViewModel Clone() => (CourseProformaViewModel)MemberwiseClone();
        
    }
    public class CourseViewModel: BaseCourseViewModel, IClone<CourseViewModel>
    {
        public int CourseId { get; set; }
        private string _courseName = "Deep learning";
        public string CourseName
        {
            get => _courseName;
            set => UpdateObservable(ref _courseName, value);
        }
        private string _courseDescription;
        public string CourseDescription
        {
            get => _courseDescription;
            set => UpdateObservable(ref _courseDescription, value);
        }
        private string _courseExpectation;
        public string CourseExpectation
        {
            get => _courseExpectation;
            set => UpdateObservable(ref _courseExpectation, value);
        }
        private CourseProformaViewModel _courseProformaViewModel = new();
        public virtual CourseProformaViewModel CourseProforma
        {
            get => _courseProformaViewModel;
            set => UpdateObservable(ref _courseProformaViewModel, value);
        }
        public void UpdateProforma(CourseProformaViewModel courseProformaViewModel)
        {

        }
        public override void AcceptChanges()
        {
            base.AcceptChanges();
            CourseProforma.AcceptChanges();
        }
        public CourseViewModel Clone()
        {
            CourseViewModel model = new();
            model.CourseId = CourseId;
            model.CourseName = CourseName;
            model.CourseDescription = CourseDescription;
            model.CourseExpectation = CourseExpectation;
            model.CourseProforma = CourseProforma.Clone();
            return model;
        }
        public override void BeginEdit()
        {
            if (_inEdit) return;

            // Save current values for rollback
            OriginalObject = Clone();

            _inEdit = true;
        }
        public override void CancelEdit()
        {
            if (!_inEdit) return;

            // Restore from backup copy
            if (OriginalObject != null)
            {
                CourseName = OriginalObject.CourseName;
                CourseDescription = OriginalObject.CourseDescription;
                CourseExpectation = OriginalObject.CourseExpectation;
                CourseProforma = OriginalObject.CourseProforma;
            }
            _inEdit = false;
        }

    }
    public class ListOfCourseViewModel:Loadable<CourseViewModel>
    {
        public ListOfCourseViewModel()
        {
            Init();
        }
        private void Init()
        {
            LoadItems(StaticDataSource.Courses.ToVM());
        }
    }
    public class CourseViewModelUI:BaseViewModel
    {
        #region Properties
        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => UpdateObservable(ref _selectedIndex, value);
        }

        private ListOfCourseViewModel _courseVM;
        public ListOfCourseViewModel CoursesVM
        {
            get => _courseVM;
            set => UpdateObservable(ref _courseVM, value);
        }
        
        #endregion

        #region Commands
        public ICommand NewCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        #endregion

        #region Constructor
        public CourseViewModelUI()
        {
            SelectedIndex = 0;
            CoursesVM = ServiceLocator.ListOfCourseViewModel;
            NewCommand = new DelegateCommand(OnNew);
            DeleteCommand = new DelegateCommand(OnDelete);
            UpdateCommand = new DelegateCommand(OnUpdate);
        }
        #endregion

        #region Handlers
        private void OnDelete(object parameter)
        {

        }
        private void OnUpdate(object parameter)
        {
            if (CoursesVM.IsSelected)
            {
                ServiceLocator.CourseFormViewModel.Course = CoursesVM.SelectedItem;
                ServiceLocator.CourseFormViewModel.Course.BeginEdit();
                ServiceLocator.CourseFormViewModel.IsSave = false;
                GoForward();
            }

        }
        private void OnNew(object parameter)
        {
            ServiceLocator.CourseFormViewModel.Course = new();
            ServiceLocator.CourseFormViewModel.Course.BeginEdit();
            ServiceLocator.CourseFormViewModel.IsSave = true;
            GoForward();
        }
        public void GoForward()
        {
            SelectedIndex++;
        }
        public void GoBack() => SelectedIndex--;
        #endregion
    }
}
