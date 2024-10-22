using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RegistrationApplication.MVVM.ViewModels
{
    public interface IActivity
    {
        bool IsActivity { get; set; }
        void ShowActivity();
        void HideActivity();
    }
    public class BaseViewModel : INotifyPropertyChanged, IActivity, IChangeTracking
    {

        protected virtual void OnShow()
        {

        }
        public BaseViewModel()
        {
            IsActivity = false;
        }

        private bool _isActivity;
        public bool IsActivity
        {
            get => _isActivity;
            set => UpdateObservable(ref _isActivity, value);
        }
        private bool _isChanged;
        public bool IsChanged => _isChanged;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                _isChanged = true;
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

        public void ShowActivity()
        {
            IsActivity = true;
            OnShow();
        }

        public void HideActivity()
        {
            IsActivity = false;
            OnShow();
        }

        public void AcceptChanges()
        {
            _isChanged = false;
        }
    }
}
