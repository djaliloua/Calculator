using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RegistrationApplication.MVVM.ViewModels
{
    public interface IClone<T>
    {
        public T Clone();
    }
    public class BaseViewModel : INotifyPropertyChanged, IChangeTracking
    {
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

      
        public virtual void AcceptChanges()
        {
            _isChanged = false;
        }
    }
}
