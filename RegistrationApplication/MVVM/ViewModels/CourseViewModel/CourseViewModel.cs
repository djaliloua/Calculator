using Patterns.Implementation;
using RegistrationApplication.DataAccessLayer;
using RegistrationApplication.Extensions;

namespace RegistrationApplication.MVVM.ViewModels.CourseViewModel
{
    public class CourseProformaViewModel:BaseViewModel
    {
        public int CourseProformaId { get; set; }
        public int CourseId { get; set; }

        private CourseViewModel _course;
        public CourseViewModel Course
        {
            get => _course;
            set => UpdateObservable(ref _course, value);
        }
    }
    public class CourseViewModel:BaseViewModel, IEquatable<CourseViewModel>, IClone<CourseViewModel>
    {
        public int CourseId { get; set; }
        private string _courseName;
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
        private CourseProformaViewModel _courseProformaViewModel;
        public virtual CourseProformaViewModel CourseProforma
        {
            get => _courseProformaViewModel;
            set => UpdateObservable(ref _courseProformaViewModel, value);
        }

        public bool Equals(CourseViewModel other)
        {
            if(other == null) return false;
            return CourseId == other.CourseId;
        }

        public CourseViewModel Clone() => (CourseViewModel)MemberwiseClone();
        
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
        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => UpdateObservable(ref _selectedIndex, value);
        }

        public ListOfCourseViewModel CoursesVM { get; set; }
        public CourseViewModelUI()
        {
            SelectedIndex = 0;
            CoursesVM = ServiceLocator.ListOfCourseViewModel;
        }
    }
}
