using Microsoft.Win32;
using RegistrationApplication.DataAccessLayer.Implementations;
using RegistrationApplication.Extensions;
using RegistrationApplication.MVVM.Models;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using RegistrationApplication.Utility;

namespace RegistrationApplication.MVVM.ViewModels.TrainersViewModels
{
    public class TrainerFormViewModel:BaseViewModel
    {
        #region Properties
        public bool IsSave { get; set; } = true;
        private TrainerViewModel _trainerVM;
        public TrainerViewModel Trainer
        {
            get => _trainerVM;
            set => UpdateObservable(ref _trainerVM, value);
        }
        #endregion

        #region Commands
        public ICommand DownloadCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand SaveAndAddNewCommand { get; private set; }
        public ICommand DeleteRowCommand { get; private set; }
        public ICommand ChoosePictureCommand { get; private set; }
        public ICommand RemoveCommand { get; private set; }
        #endregion

        #region Constructor
        public TrainerFormViewModel()
        {
            Initialization();
            CommandSteup();
        }
        #endregion

        #region Initialization
        private void Initialization()
        {
            Trainer = new TrainerViewModel();
        }
        private void CommandSteup()
        {
            SaveCommand = new DelegateCommand(OnSave);
            DeleteRowCommand = new DelegateCommand(OnDeleteRow);
            SaveAndAddNewCommand = new DelegateCommand(OnSaveAndAddNew);
            ChoosePictureCommand = new DelegateCommand(OnChoosePicture);
            RemoveCommand = new DelegateCommand(OnRemove);
            DownloadCommand = new DelegateCommand(OnDownload);
        }
        #endregion

        #region Handlers
       
        private void OnDownload(object parameter)
        {
            Image image = ImageUtility.byteArrayToImage(Trainer.PictureFile.Picture);
            image.Save(Trainer.PictureFile.FileName);
            Notifier.Show("Image donwloaded!!!!", () =>
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = Trainer.PictureFile.FullPath, // Use the full path to the file
                    UseShellExecute = true // This tells the OS to open it with the default program
                });
            });
            
        }
        private void OnRemove(object parameter)
        {
            Trainer.UpdatePicture(new());
            Notifier.Show("Removed Image");
        }
        private void OnSaveAndAddNew(object parameter)
        {
            // Save first in the db
            if (string.IsNullOrEmpty(Trainer.Error))
            {
                if (!IsSave)
                {
                    using var repository = new TrainerRepository();
                    var saveObj = repository.Update(Trainer.FromVM());
                    ServiceLocator.TrainersProfilesViewModel.UpdateItem(saveObj.ToVM());
                    Notifier.Show("Updated");
                }
                else
                {
                    using var repository = new TrainerRepository();
                    var savedObj = repository.Save(Trainer.FromVM());
                    ServiceLocator.TrainersProfilesViewModel.AddItem(savedObj.ToVM());
                    Notifier.Show("Saved");
                }
                Trainer.AcceptChanges();
                ServiceLocator.TrainerFormViewModel.Trainer = new();
                Notifier.Show("Add new");

            }
            else
            {
                MessageBox.Show(Trainer.Error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        private void OnDeleteRow(object parameter)
        {
            var dialog = MessageBox.Show("Do you want to Delete", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialog == MessageBoxResult.Yes)
            {
                ExperienceViewModel experienceViewModel = (ExperienceViewModel)parameter;
                if (experienceViewModel != null)
                {
                    if (experienceViewModel.ExperienceId == 0)
                    {
                        Trainer.DeleteExperience(experienceViewModel);
                        return;
                    }
                    using var repository = new GenericRepository<Experience>();
                    repository.Delete(experienceViewModel.ExperienceId);
                    Trainer.DeleteExperience(experienceViewModel);
                }
            }
            
        }
        private void OnChoosePicture(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".png|jpg";
            openFileDialog.Title = "File Picker";
            if (openFileDialog.ShowDialog() == true)
            {
                Trainer.UpdatePicture(new PictureFileViewModel(openFileDialog.FileName));
            }
            
        }
        private void OnSave(object parameter)
        {
            if (string.IsNullOrEmpty(Trainer.Error))
            {
                if (!IsSave)
                {
                    using var repository = new TrainerRepository();
                    var saveObj = repository.Update(Trainer.FromVM());
                    ServiceLocator.TrainersProfilesViewModel.UpdateItem(saveObj.ToVM());
                    Notifier.Show("Updated");
                }
                else
                {
                    using var repository = new TrainerRepository();
                    var savedObj = repository.Save(Trainer.FromVM());
                    ServiceLocator.TrainersProfilesViewModel.AddItem(savedObj.ToVM());
                    Notifier.Show("Add new");
                }
                Trainer.AcceptChanges();
                ServiceLocator.TrainerRegistrationViewModel.SeletedIndex = 0;
            }
            else
            {
                MessageBox.Show(Trainer.Error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        #endregion
    }
}
