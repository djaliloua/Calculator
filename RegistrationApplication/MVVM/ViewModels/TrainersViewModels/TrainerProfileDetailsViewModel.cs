namespace RegistrationApplication.MVVM.ViewModels.TrainersViewModels
{
    public class TrainerProfileDetailsViewModel:BaseViewModel
    {
        private TrainerViewModel _viewModel;
        public TrainerViewModel TrainerViewModel
        {
            get => _viewModel;
            set => UpdateObservable(ref _viewModel, value);
        }
        public TrainerProfileDetailsViewModel()
        {
            
        }
    }
}
