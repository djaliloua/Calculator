using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace RegistrationApplication.MVVM.ViewModels
{
    public interface IClone<T>
    {
        public T Clone();
    }
    public interface IBaseViewModel<T> : IEditableObject
    {
        T OriginalObject { get; set; }
        bool IsEdit { get; set; }
    }
    public class BaseViewModel : INotifyPropertyChanged, IChangeTracking
    {
        protected bool _inEdit;
        private bool _isChanged;
        [NotifyParentProperty(true)]
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
    public class ParentBaseViewModel<T> : BaseViewModel, IBaseViewModel<T> where T : class
    {
        public T OriginalObject { get; set; }
        public bool IsEdit { get; set; }
        public virtual void BeginEdit()
        {
            if (IsEdit)
            {
                return;
            }
            OriginalObject = Utility.Utility.DeepCopy(this as T);
            IsEdit = true;
        }
        public virtual void CancelEdit()
        {
            if (!IsEdit)
            {
                return;
            }
            var type = this.GetType();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (OriginalObject != null && property.SetMethod != null)
                {
                    var value = property.GetValue(OriginalObject);
                    property.SetValue(this, value);
                    OnPropertyChanged(property.Name);
                }

            }
            IsEdit = false;
        }
        public virtual void EndEdit()
        {
            if (!IsEdit)
            {
                return;
            }
            OriginalObject = default;
            IsEdit = false;
        }
        public override void AcceptChanges()
        {
            base.AcceptChanges();
            var type = this.GetType();
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var (value, method) in from field in fields
                                            where typeof(BaseViewModel).IsAssignableFrom(field.FieldType)
                                            let value = field.GetValue(this)
                                            let subTypes = value.GetType()
                                            let method = subTypes.GetMethod("AcceptChanges")
                                            select (value, method))
            {
                method?.Invoke(value, null);
            }
        }
    }
}
