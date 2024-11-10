using System.Windows.Controls;
using System.Windows.Input;

namespace RegistrationApplication.MVVM.Views.CourseViews
{
    /// <summary>
    /// Interaction logic for ListOfCourses.xaml
    /// </summary>
    public partial class ListOfCourses : UserControl
    {
        public ListOfCourses()
        {
            InitializeComponent();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ServiceLocator.CourseViewModelUI.CoursesVM.IsSelected)
            {
                ServiceLocator.CourseViewModelUI.SelectedIndex++;
            }
        }
    }
}
