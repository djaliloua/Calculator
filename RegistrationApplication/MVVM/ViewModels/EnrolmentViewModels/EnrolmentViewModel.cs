namespace RegistrationApplication.MVVM.ViewModels.EnrolmentViewModels
{
    public class EnrolmentViewModel:BaseViewModel
    {
        private int _selectedIndex = 0;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => UpdateObservable(ref _selectedIndex, value);
        }
        public EnrolmentViewModel()
        {
            
        }
    }
}
