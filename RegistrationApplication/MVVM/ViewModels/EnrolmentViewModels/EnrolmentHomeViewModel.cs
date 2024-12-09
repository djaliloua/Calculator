using System.Windows.Input;

namespace RegistrationApplication.MVVM.ViewModels.EnrolmentViewModels
{
    public class EnrolmentHomeViewModel:BaseViewModel
    {
        public ICommand AddCommand { get; private set; }
        public EnrolmentHomeViewModel()
        {
            AddCommand = new DelegateCommand(OnAdd);
        }

        private void OnAdd(object parameter)
        {
            ServiceLocator.EnrolmentViewModel.SelectedIndex++;
        }
    }
}
