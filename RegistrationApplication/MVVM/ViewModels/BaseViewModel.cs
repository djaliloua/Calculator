using RegistrationApplication.MVVM.ViewModels.TrainersViewModels;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RegistrationApplication.MVVM.ViewModels
{
    public interface IClone<T>
    {
        public T Clone();
    }
    public interface IBaseViewModel<T>: IEditableObject
    {
        T OriginalObject { get; }
    }
    public class PictureFileBaseViewModel : BaseViewModel, IBaseViewModel<PictureFileViewModel>
    {
        private PictureFileViewModel _originalObject;
        public PictureFileViewModel OriginalObject
        {
            get => _originalObject;
            protected set => _originalObject = value;
        }

        public virtual void BeginEdit()
        {
            throw new NotImplementedException();
        }

        public virtual void CancelEdit()
        {
            throw new NotImplementedException();
        }

        public virtual void EndEdit()
        {
            if (!_inEdit) return;

            // Commit changes by clearing the backup
            OriginalObject = null;
            _inEdit = false;
        }
    }
    public class TrainerBaseViewModel : BaseViewModel, IBaseViewModel<TrainerViewModel>
    {
        private TrainerViewModel _viewModel;
        public TrainerViewModel OriginalObject
        {
            get => _viewModel;
            protected set
            {
                _viewModel = value;
            }
        }
        public virtual void BeginEdit()
        {
            
        }

        public virtual void CancelEdit()
        {
            throw new NotImplementedException();
        }

        public virtual void EndEdit()
        {
            if(!_inEdit) return;

            // Commit changes by clearing the backup
            OriginalObject = null;
            _inEdit = false;
        }
    }
    public class ExperienceBaseViewModel : BaseViewModel, IBaseViewModel<ExperienceViewModel>
    {
        private ExperienceViewModel _viewModel;
        public ExperienceViewModel OriginalObject
        {
            get => _viewModel;
            protected set => _viewModel = value;
        }

        public virtual void BeginEdit()
        {
            throw new NotImplementedException();
        }

        public virtual void CancelEdit()
        {
            throw new NotImplementedException();
        }

        public virtual void EndEdit()
        {
            if (!_inEdit) return;

            // Commit changes by clearing the backup
            OriginalObject = null;
            _inEdit = false;
        }
    }
    public class BaseViewModel : INotifyPropertyChanged, IChangeTracking
    {
        protected bool _inEdit;
        private bool _isChanged;
        public bool IsChanged
        {
            get => _isChanged;
            protected set => UpdateObservable(ref _isChanged, value);
        }
       
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                _isChanged = true;
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //public event EventHandler OnLoad;

        public void UpdateObservable<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = "")
        {
            oldValue = newValue;
            OnPropertyChanged(propertyName);
        }
        public void UpdateObservable<T>(ref T oldValue, T newValue, Action callback, [CallerMemberName] string propertyName = "")
        {
            oldValue = newValue;
            OnPropertyChanged(propertyName);
            callback();
        }

        public virtual void AcceptChanges()
        {
            _isChanged = false;
        }
    }
}
