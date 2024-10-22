using Microsoft.Win32;
using Patterns.Implementation;
using RegistrationApplication.DataAccessLayer.Implementations;
using RegistrationApplication.Extensions;
using RegistrationApplication.MVVM.Views.TrainersView;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace RegistrationApplication.MVVM.ViewModels.TrainersViewModels
{
    public interface IClone<T>
    {
        public T Clone();
    }
    public class PictureFile : BaseViewModel
    {
        public int PictureId { get; set; }
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
        public PictureFile(string path)
        {
            FullPath = path;
            FileName = Path.GetFileName(path);
            FileExtension = Path.GetExtension(path);
            Picture = !string.IsNullOrEmpty(FullPath) ? ImageToByteArray(Image.FromFile(FullPath)) : Array.Empty<byte>();
        }
        private byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
        public PictureFile()
        {
            FullPath = string.Empty;
            FileName = string.Empty;
            FileExtension = string.Empty;
            Picture = Array.Empty<byte>();
        }

    }
    public class TrainerViewModel : BaseViewModel, IDataErrorInfo, IClone<TrainerViewModel> 
    {
        public int TrainerId { get; set; }

        private string _lastName = "Ali";
        public string LastName
        {
            get => _lastName;
            set => UpdateObservable(ref _lastName, value);
        }

        private string _firstName = "Abdou Djalilou";
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

        private int _age;
        public int Age
        {
            get => _age;
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

        private string _jobTitle = "Software developer";
        public string JobTitle
        {
            get => _jobTitle;
            set => UpdateObservable(ref _jobTitle, value);
        }
        private PictureFile _picturePath = new PictureFile(@"C:\Users\djali\OneDrive\Immagini\Rullino\ali1.jpg");
        public PictureFile PicturePath
        {
            get => _picturePath;
            set => UpdateObservable(ref _picturePath, value, () =>
            {
                Picture = value.Picture;
            });
        }
        private byte[] _pictureByte;
        public byte[] Picture
        {
            get => _pictureByte;
            set => UpdateObservable(ref _pictureByte, value);
        }

        public ICommand ChoosePictureCommand { get; private set; }
        public string Error => throw new NotImplementedException();

        public string this[string columnName] => throw new NotImplementedException();

        public TrainerViewModel()
        {
            //PictureByte = ImageToByteArray(Image.FromFile(PicturePath.FullPath));
            ChoosePictureCommand = new DelegateCommand(OnChoosePicture);
        }
        public TrainerViewModel(string path)
        {
            //IsSaveBtn = isSaveBtn;
            Picture = !string.IsNullOrEmpty(path) ? ImageToByteArray(Image.FromFile(PicturePath.FullPath)) : Array.Empty<byte>();
            ChoosePictureCommand = new DelegateCommand(OnChoosePicture);

        }
        private void OnChoosePicture(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.DefaultExt = "";
            openFileDialog.Title = "File Picker";
            if (openFileDialog.ShowDialog() == true)
            {
                PicturePath = new PictureFile(openFileDialog.FileName);
            }
        }
        public byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
        public TrainerViewModel Initialize()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            EducationLevel = string.Empty;
            JobTitle = string.Empty;
            PicturePath = new();
            return this;
        }

        public TrainerViewModel Clone() => (TrainerViewModel)MemberwiseClone();
       
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

        protected override int Index(TrainerViewModel item)
        {
            TrainerViewModel searchIem = Items.Where(x => x.TrainerId == item.TrainerId).FirstOrDefault();
            return base.Index(searchIem);
        }
    }
    public class TrainerRegistrationViewModel:BaseViewModel, IClone<TrainerRegistrationViewModel>
    {
        #region Properties
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
        public ICommand OpenCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        #endregion

        #region Constructor
        public TrainerRegistrationViewModel()
        {
            TrainersProfilesVM = ServiceLocator.TrainersProfilesViewModel;
            AddCommand = new DelegateCommand(OnAdd);
            OpenCommand = new DelegateCommand(OnOpen);
            DeleteCommand = new DelegateCommand(OnDelete);
            UpdateCommand = new DelegateCommand(OnUpdate);
        }
        #endregion

        #region Handlers
        private void OnUpdate(object parameter)
        {
            if (TrainersProfilesVM.IsSelected)
            {
                var window = ServiceLocator.GetService<TrainerRegistrationWindow>();
                ServiceLocator.TrainerRegistrationWinViewModel.TrainerViewModel = TrainersProfilesVM.SelectedItem.Clone();
                ServiceLocator.TrainerRegistrationWinViewModel.IsSave = false;
                window.Show();
            }
        }
        private void OnDelete(object parameter)
        {
            
            
            if (TrainersProfilesVM.IsSelected)
            {
                var dialog = MessageBox.Show("Do you want to Delete", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (dialog == MessageBoxResult.Yes)
                {
                    //using var repository = new TrainerRepository();
                    //repository.Delete(selected.FromVM().TrainerId);
                    TrainersProfilesVM.DeleteItem(TrainersProfilesVM.SelectedItem);
                }
            }
            else
            {
                MessageBox.Show("Please, select an Item");
            }
        }
        private void OnOpen(object parameter)
        {
            if(TrainersProfilesVM.IsSelected)
            {
                var window = ServiceLocator.GetService<TrainerProfileDetails>();
                ServiceLocator.TrainerProfileDetailsViewModel.TrainerViewModel = TrainersProfilesVM.SelectedItem;
                window.Show();
            }
        }
        private void OnAdd(object parameter)
        {
            var window = ServiceLocator.GetService<TrainerRegistrationWindow>();
            ServiceLocator.TrainerRegistrationWinViewModel.TrainerViewModel = new TrainerViewModel();
            ServiceLocator.TrainerRegistrationWinViewModel.IsSave = true;
            window.Show();
        }
        #endregion

        public TrainerRegistrationViewModel Clone()
        {
            return (TrainerRegistrationViewModel)MemberwiseClone();
        }
    }
}
