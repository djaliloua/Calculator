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
    public class ExperienceViewModel : ExperienceBaseViewModel, IClone<ExperienceViewModel>, IDataErrorInfo
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

        public ExperienceViewModel Clone() => (ExperienceViewModel)MemberwiseClone();

        public override void BeginEdit()
        {
            if (_inEdit) return;

            // Save current values for rollback
            OriginalObject = new();
            OriginalObject.Position = Position;
            OriginalObject.To = To;
            OriginalObject.From = From;
            OriginalObject.Description = Description;
            OriginalObject.Trainer = Trainer;
            _inEdit = true;
        }
        public override void CancelEdit()
        {
            if (!_inEdit) return;

            // Restore from backup copy
            if (OriginalObject != null)
            {
                Position = OriginalObject.Position;
                To = OriginalObject.To;
                From = OriginalObject.From;
                Description = OriginalObject.Description;
                Trainer = OriginalObject.Trainer;
            }
            _inEdit = false;
        }

    }
    public class PictureFileViewModel : PictureFileBaseViewModel, IClone<PictureFileViewModel>, IDataErrorInfo
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
        public string Error => throw new NotImplementedException();

        public string this[string columnName] => throw new NotImplementedException();
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

        public override void BeginEdit()
        {
            if (_inEdit) return;

            // Save current values for rollback
            OriginalObject =new();
            OriginalObject.Picture = Picture;
            OriginalObject.Trainer = Trainer;
            OriginalObject.FileExtension = FileExtension;
            OriginalObject.FullPath = FullPath;
            OriginalObject.FileName = FileName;
            _inEdit = true;
        }
        public override void CancelEdit()
        {
            if (!_inEdit) return;
            if(OriginalObject == null) return;
            if(OriginalObject != null)
            {
                FullPath = OriginalObject.FullPath;
                FileName = OriginalObject.FileName;
                FileExtension = OriginalObject.FileExtension;
                Picture = OriginalObject.Picture;
            }
            _inEdit = false;
        }
        public PictureFileViewModel Clone() => (PictureFileViewModel)MemberwiseClone();
        
    }
    public class TrainerViewModel : TrainerBaseViewModel, IDataErrorInfo, IClone<TrainerViewModel>, IEquatable<TrainerViewModel> 
    {
        #region Properties
        public int TrainerId { get; set; }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => UpdateObservable(ref _lastName, value);
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => UpdateObservable(ref _firstName, value);
        }
        private string _shortDescription;
        public string ShortDescription
        {
            get => _shortDescription;
            set => UpdateObservable(ref _shortDescription, value);
        }
        private int? _age;
        public int? Age
        {
            get => DateTime.Now.Year - Birthday?.Year;
            set => UpdateObservable(ref _age, value);
        }
        
        private string _educationLevel;
        public string EducationLevel
        {
            get => _educationLevel;
            set => UpdateObservable(ref _educationLevel, value);
        }
        private string _gender;
        public string Gender
        {
            get => _gender;
            set => UpdateObservable(ref _gender, value);
        }

        private string _jobTitle;
        public string JobTitle
        {
            get => _jobTitle;
            set => UpdateObservable(ref _jobTitle, value);
        }

        private DateTime? _birthday = DateTime.Now;
        public DateTime? Birthday
        {
            get => _birthday;
            set => UpdateObservable(ref _birthday, value);
        }

        private PictureFileViewModel _picturePath;
        public PictureFileViewModel PictureFile
        {
            get => _picturePath;
            set
            {
                if(_picturePath != null)
                {
                    _picturePath.PropertyChanged -= PictureFile_PropertyChanged;
                }
                _picturePath = value;
                if(_picturePath.Picture == null)
                {
                    UpdatePicture(new(@"DefaultResources\h1.png"));
                }

                if (_picturePath != null)
                {
                    _picturePath.PropertyChanged += PictureFile_PropertyChanged;
                }
            }
        }

        private ObservableCollection<ExperienceViewModel> _experience;
        public ObservableCollection<ExperienceViewModel> Experiences
        {
            get => _experience ?? new ObservableCollection<ExperienceViewModel>();
            set
            {
                if(value != null)
                {
                    foreach(var experience in value)
                    {
                        experience.PropertyChanged -= Experience_PropertyChanged;
                    }
                }
                _experience = value;
                if(value != null)
                {
                    foreach (var experience in value)
                    {
                        experience.PropertyChanged += Experience_PropertyChanged;
                    }
                }
            }
        }

        private void Experience_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ExperienceViewModel experience = (ExperienceViewModel)sender;
            if (experience.IsChanged)
            {
                // Mark TrainerViewModel as changed if PictureFile's IsChanged is true
                IsChanged = true;
            }
        }
        #endregion

        #region Validations
        public string Error
        {
            get
            {
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
                string error = string.Empty ;
                switch(columnName)
                {
                    case nameof(LastName):
                        if(string.IsNullOrEmpty(LastName))
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

        #region Constructor
        public TrainerViewModel()
        {
            Experiences ??= new ObservableCollection<ExperienceViewModel>();
            PictureFile ??= new PictureFileViewModel(@"DefaultResources\h1.png");
        }

        #endregion

        #region Public methods
        
        public override void AcceptChanges()
        {
            base.AcceptChanges();
            PictureFile.AcceptChanges();
            foreach (var experience in Experiences)
            {
                experience.AcceptChanges();
            }
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
                LastName = OriginalObject.LastName;
                FirstName = OriginalObject.FirstName;
                Age = OriginalObject.Age;
                EducationLevel = OriginalObject.EducationLevel;
                JobTitle = OriginalObject.JobTitle;
                Birthday = OriginalObject.Birthday;
                UpdatePicture(OriginalObject.PictureFile);
                Gender = OriginalObject.Gender;
                ShortDescription = OriginalObject.ShortDescription;
                Experiences = OriginalObject.Experiences;
                for(int i = 0; i < OriginalObject.Experiences.Count; i++)
                {
                    if (Experiences[i].ExperienceId == OriginalObject.Experiences[i].ExperienceId)
                    {
                        Experiences[i] = OriginalObject.Experiences[i];
                    }
                }
                OnPropertyChanged(nameof(Experiences));
            }
            _inEdit = false;
        }
        public void UpdateExperience(ExperienceViewModel originalModel, ExperienceViewModel currentModel)
        {
            currentModel.Position = originalModel.Position;
            currentModel.Description = originalModel.Description;
            currentModel.From = originalModel.From;
            currentModel.To = originalModel.To;
            currentModel.Trainer = originalModel.Trainer;
        }

        //private void UpdateChangeState() => IsChanged = true;
        public void UpdatePicture(PictureFileViewModel picturePath)
        {
            PictureFile.Picture = picturePath.Picture;
            PictureFile.FullPath = picturePath.FullPath;
            PictureFile.FileExtension = picturePath.FileExtension;
            PictureFile.FileName = picturePath.FileName;
        }
        public TrainerViewModel Clone()
        {
            TrainerViewModel model = new();
            model.FirstName = FirstName;
            model.LastName = LastName;
            model.Birthday = Birthday;
            model.Gender = Gender;
            model.ShortDescription = ShortDescription;
            model.JobTitle = JobTitle;
            model.PictureFile = PictureFile.Clone();
            model.Experiences = new ObservableCollection<ExperienceViewModel>(Experiences.Select(item => item.Clone()).ToList());
            
            return model;
        }
        
        public void DeleteExperience(ExperienceViewModel experience)
        {
            Experiences.Remove(experience);
        }

        public bool Equals(TrainerViewModel other)
        {
            if (other == null) return false;
            return TrainerId == other.TrainerId;
        }
        private void PictureFile_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PictureFile.IsChanged)
            {
                // Mark TrainerViewModel as changed if PictureFile's IsChanged is true
                IsChanged = true;
            }
        }
        #endregion

    }
    public class TrainersProfilesViewModel:Loadable<TrainerViewModel>, IClone<TrainersProfilesViewModel>
    {
        #region Commands
        public ICommand FlipCommand { get; private set; }
        #endregion
        public TrainersProfilesViewModel()
        {
            Init();
        }
        private void Init()
        {
            using var repository = new TrainerRepository();
            var trainers = repository.GetAllDtos();
            LoadItems(trainers);
        }
        
        private void Update(TrainerViewModel parameter)
        {
            TrainerViewModel selected = parameter;
            if (parameter != null)
            {
                using var repository = new TrainerRepository();
                repository.Update(selected.FromVM());
            }
        }
        public TrainersProfilesViewModel Clone()
        {
            return (TrainersProfilesViewModel)MemberwiseClone();
        }
    }
    public class TrainerRegistrationViewModel:BaseViewModel, IClone<TrainerRegistrationViewModel>
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
                    using var repository = new TrainerRepository();
                    repository.Delete(TrainersProfilesVM.SelectedItem.FromVM().TrainerId);
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
            ServiceLocator.TrainerFormViewModel.IsSave = true;
        }
        #endregion

        public TrainerRegistrationViewModel Clone()
        {
            return (TrainerRegistrationViewModel)MemberwiseClone();
        }
    }
}
