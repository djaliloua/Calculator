namespace RegistrationApplication.MVVM.ViewModels.TrainersViewModels
{

    public class ProfileViewModel:BaseViewModel
    {
        private TrainerViewModel _trainerViewModel;
        public TrainerViewModel TrainerViewModel
        {
            get => _trainerViewModel;
            set => UpdateObservable(ref _trainerViewModel, value);
        }
        public ProfileViewModel()
        {
            TrainerViewModel = ServiceLocator.TrainerViewModel;
        }
    }
}
