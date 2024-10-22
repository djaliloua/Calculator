using Microsoft.Win32;
using RegistrationApplication.MVVM.Views.TrainersView;
using System.Windows.Input;

namespace RegistrationApplication.MVVM.ViewModels.TrainersViewModels
{
    public class TrainerRegistrationWinViewModel:BaseViewModel
    {
        public bool IsSave { get; set; } = true;
        #region Properties
        private TrainerViewModel _trainerVM;
        public TrainerViewModel TrainerViewModel
        {
            get => _trainerVM;
            set => UpdateObservable(ref _trainerVM, value);
        }
        #endregion

        #region Commands
        public ICommand SaveCommand { get; private set; }
        public ICommand ChoosePictureCommand { get; private set; }
        #endregion

        #region Constructor
        public TrainerRegistrationWinViewModel()
        {
            TrainerViewModel = new TrainerViewModel();
            SaveCommand = new DelegateCommand(OnSave);
            ChoosePictureCommand = new DelegateCommand(OnChoosePicture);
        }
        #endregion

        #region Handlers
        private void OnChoosePicture(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.DefaultExt = "";
            openFileDialog.Title = "File Picker";
            if (openFileDialog.ShowDialog() == true)
            {
                TrainerViewModel.PicturePath = new PictureFile(openFileDialog.FileName);
            }
            else
            {
                TrainerViewModel.PicturePath = new PictureFile();
            }
        }
        private void OnSave(object parameter)
        {
            //TrainerViewModel trainer = new TrainerViewModel(string.Empty).Initialize();
            //using var repository = new TrainerRepository();
            //var t = repository.Save(trainer.FromVM());
            if(!IsSave)
            {
                ServiceLocator.TrainersProfilesViewModel.UpdateItem(TrainerViewModel);
            }
            else
            {
                ServiceLocator.TrainersProfilesViewModel.AddItem(TrainerViewModel);
            }
            var window = ServiceLocator.GetService<TrainerRegistrationWindow>();
            window.Hide();

        }
        #endregion
    }
}
