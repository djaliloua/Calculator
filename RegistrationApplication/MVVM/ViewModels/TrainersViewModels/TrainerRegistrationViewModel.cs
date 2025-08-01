using Patterns.Abstractions;
using Patterns.Implementation;
using RegistrationApplication.DataAccessLayer.Implementations;
using RegistrationApplication.Extensions;
using RegistrationApplication.Utility;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace RegistrationApplication.MVVM.ViewModels.TrainersViewModels
{
    public partial class TrainerViewModel: IDataErrorInfo
    {
        #region Validations
        public string Error
        {
            get
            {
                if(PictureFile != null && !string.IsNullOrEmpty(PictureFile.Error))
                {
                    return PictureFile.Error;
                }
                if (Experiences != null && Experiences.Count > 0)
                {
                    foreach (var experience in Experiences)
                    {
                        if (!string.IsNullOrEmpty(experience.Error))
                        {
                            return experience.Error;
                        }
                    }
                }

                string error = string.Empty;
                if (string.IsNullOrEmpty(LastName))
                {
                    return "Last Name is required";
                }
                if (string.IsNullOrEmpty(FirstName))
                {
                    return "First Name is required";
                }

                return error;
            }
        }
        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(LastName):
                        if (string.IsNullOrEmpty(LastName))
                        {
                            error = "Last name is required";
                        }
                        break;
                    case nameof(FirstName):
                        if (string.IsNullOrEmpty(FirstName))
                        {
                            error = "First name is required";
                        }
                        break;
                }

                return error;
            }
        }
        #endregion
    }
    public class ExperienceViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Properties
        public int ExperienceId { get; set; }
        private string _position;
        public string Position
        {
            get => _position;
            set => UpdateObservable(ref _position, value);  
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => UpdateObservable(ref _description, value);
        }

        private string _companyName = "Iveco";
        public string CompanyName
        {
            get => _companyName;
            set => UpdateObservable(ref _companyName, value);
        }

        private DateTime _from = DateTime.Now;
        public DateTime From
        {
            get => _from;
            set => UpdateObservable(ref _from, value);
        }
        private DateTime _to = DateTime.Now;
        public DateTime To
        {
            get => _to;
            set => UpdateObservable(ref _to, value);    
        }
        public int TrainerId { get; set; }

        private TrainerViewModel _viewModel;
        public TrainerViewModel Trainer
        {
            get => _viewModel;
            set => UpdateObservable(ref _viewModel, value);
        }
        
        #endregion

        #region Validations
        public string Error
        {
            get
            {
                string error = "";
                if(string.IsNullOrEmpty(Position))
                {
                    error = "Position property is required";
                }
                
                return error;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch(columnName)
                {
                    case nameof(Position):
                        if(string.IsNullOrEmpty(Position))
                        {
                            error = "Position is required";
                        }
                        break;
                }
                return error;
            }
        }
        #endregion

        
    }
    public class PictureFileViewModel : BaseViewModel, IDataErrorInfo
    {
        #region Properties
        public int PictureFileId { get; set; }
        private string _fileExtenstion;
        public string FileExtension
        {
            get => _fileExtenstion;
            set => UpdateObservable(ref _fileExtenstion, value);
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => UpdateObservable(ref _fileName, value);
        }
        private string _imagePath;
        public string FullPath
        {
            get => _imagePath;
            set => UpdateObservable(ref _imagePath, value);
        }

        private byte[] _image;
        public byte[] Picture
        {
            get => _image;
            set => UpdateObservable(ref _image, value);
        }
        public int TrainerId { get; set; }
        private TrainerViewModel _trainer;
        public TrainerViewModel Trainer
        {
            get => _trainer;
            set => UpdateObservable(ref _trainer, value);
        }
        #endregion

        #region Validations
        public string Error
        {
            get
            {
                string error = string.Empty;
                if (string.IsNullOrEmpty(FileName))
                {
                    return "Last Name is required";
                }
                if (string.IsNullOrEmpty(FileExtension))
                {
                    return "First Name is required";
                }

                return error;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case nameof(FileName):
                        if (string.IsNullOrEmpty(FileName))
                        {
                            error = "Last name is required";
                        }
                        break;
                    case nameof(FileExtension):
                        if (string.IsNullOrEmpty(FileExtension))
                        {
                            error = "First name is required";
                        }
                        break;
                }

                return error;
            }
        }
        #endregion

        #region Constructors
        public PictureFileViewModel()
        {

        }
        public PictureFileViewModel(string path)
        {
            FullPath = path;
            FileName = Path.GetFileName(path);
            FileExtension = Path.GetExtension(path);
            Picture = !string.IsNullOrEmpty(FullPath) ? ImageUtility.ImageToByteArray(Image.FromFile(FullPath)) : Array.Empty<byte>();
        }
        #endregion
        
        
    }
    public partial class TrainerViewModel : ParentBaseViewModel<TrainerViewModel>
    {
        #region Properties
        public int TrainerId { get; set; }
        public string LastName
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public string FirstName
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public string ShortDescription
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public int? Age
        {
            get => DateTime.Now.Year - Birthday?.Year;
            set;
        }

        public string EducationLevel
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }

        public string Gender
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public string JobTitle
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }

        public DateTime? Birthday
        {
            get => field ?? DateTime.Now;
            set => UpdateObservable(ref field, value, () =>
            {
                OnPropertyChanged(nameof(Age));
            });
        }

        public PictureFileViewModel PictureFile
        {
            get => field;
            set => UpdateObservable(ref field, value, () =>
            {
                if (value != null)
                {
                    UpdatePicture(new(@"DefaultResources\h1.png"));
                }
            });

        }

        public ObservableCollection<ExperienceViewModel> Experiences
        {
            get => field ?? new ObservableCollection<ExperienceViewModel>();
            set => UpdateObservable(ref field, value);
        }

        
        #endregion

       

        #region Constructor
        public TrainerViewModel()
        {
            Experiences ??= new ObservableCollection<ExperienceViewModel>();
            PictureFile ??= new PictureFileViewModel(@"DefaultResources\h1.png");
            AttachEventHandlers();
        }

        

        #endregion

        #region Public methods
        //private void UpdateChangeState() => IsChanged = true;
        public void UpdatePicture(PictureFileViewModel picturePath)
        {
            PictureFile.Picture = picturePath.Picture;
            PictureFile.FullPath = picturePath.FullPath;
            PictureFile.FileExtension = picturePath.FileExtension;
            PictureFile.FileName = picturePath.FileName;
        }
        public void DeleteExperience(ExperienceViewModel experience)
        {
            Experiences.Remove(experience);
        }
        
        #endregion
    }

    public class LoadableDecorator: Loadable<TrainerViewModel>, ILoadable<TrainerViewModel>
    {
        protected void Load()
        {
            using var repository = new TrainerRepository();
            var trainers = repository.GetAllToViewModel();
            LoadItems(trainers);
        }
        public override void AddItem(TrainerViewModel item)
        {
            using var repository = new TrainerRepository();
            var savedObj = repository.Save(item.FromVM());
            base.AddItem(savedObj.ToVM());
        }
        public override void DeleteItem(TrainerViewModel item)
        {
            using var repository = new TrainerRepository();
            repository.Delete(item.TrainerId);
            base.DeleteItem(item);
        }
        public override void UpdateItem(TrainerViewModel item)
        {
            using var repository = new TrainerRepository();
            var saveObj = repository.Update(item.FromVM());
            base.UpdateItem(item);
        }
    }
    public class TrainersProfilesViewModel: LoadableDecorator
    {
        #region Commands
        public ICommand FlipCommand { get; private set; }
        #endregion
        public TrainersProfilesViewModel()
        {
            Load();
        }
        
        
    }
    public class TrainerRegistrationViewModel:BaseViewModel
    {
        #region Properties
        private int _seletedIndex = 0;
        public int SeletedIndex
        {
            get => _seletedIndex;
            set => UpdateObservable(ref _seletedIndex, value);
        }
        private TrainersProfilesViewModel trainersProfilesViewModel;
        public TrainersProfilesViewModel TrainersProfilesVM
        {
            get => trainersProfilesViewModel;
            set
            {
                trainersProfilesViewModel = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        #endregion

        #region Constructor
        public TrainerRegistrationViewModel()
        {
            Initialization();
            CommandSetup();
        }
        #endregion

        #region
        #endregion

        #region Initialization

        private void Initialization()
        {
            TrainersProfilesVM = ServiceLocator.TrainersProfilesViewModel;
        }
        private void CommandSetup()
        {
            AddCommand = new DelegateCommand(OnAdd);
            DeleteCommand = new DelegateCommand(OnDelete);
            UpdateCommand = new DelegateCommand(OnUpdate);
        }
        #endregion

        #region Handlers
        private void OnUpdate(object parameter)
        {
            if (TrainersProfilesVM.IsSelected)
            {
                ServiceLocator.TrainerFormViewModel.Trainer = TrainersProfilesVM.SelectedItem;
                ServiceLocator.TrainerFormViewModel.Trainer.BeginEdit();
                ServiceLocator.TrainerFormViewModel.Trainer.AcceptChanges();
                ServiceLocator.TrainerFormViewModel.IsSave = false;
                SeletedIndex = 1;
            }
        }
        private void OnDelete(object parameter)
        {
            if (TrainersProfilesVM.IsSelected)
            {
                var dialog = MessageBox.Show("Do you want to Delete", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialog == MessageBoxResult.Yes)
                {
                    TrainersProfilesVM.DeleteItem(TrainersProfilesVM.SelectedItem);
                }
            }
            else
            {
                MessageBox.Show("Please, select an Item");
            }
        }
        
        private async void OnAdd(object parameter)
        {
            SeletedIndex = 1;
            await Task.Delay(500);
            ServiceLocator.TrainerFormViewModel.Trainer = new();
            ServiceLocator.TrainerFormViewModel.Trainer.BeginEdit();
            ServiceLocator.TrainerFormViewModel.IsSave = true;
        }
        #endregion

       
    }
}
