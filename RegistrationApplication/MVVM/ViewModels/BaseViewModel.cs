using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RegistrationApplication.MVVM.ViewModels
{
    public class Utility
    {
        public static T DeepCopy<T>(T obj)
        {
            if (obj == null)
            {
                return default(T);
            }

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = false
            };
            return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(obj, options), options);
        }
    }
    public interface IBaseViewModel<T> : IEditableObject
    {
        T OriginalObject { get; set; }
        bool IsEdit { get; set; }
    }
    public class BaseViewModel : INotifyPropertyChanged, IChangeTracking
    {
        private bool _isChanged;

        private bool _isActivity;

        public bool IsActivity
        {
            get
            {
                return _isActivity;
            }
            set
            {
                UpdateObservable(ref _isActivity, value, "IsActivity");
            }
        }

        [NotifyParentProperty(true)]
        public bool IsChanged
        {
            get
            {
                return _isChanged;
            }
            protected set
            {
                UpdateObservable(ref _isChanged, value, "IsChanged");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            _isChanged = true;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void UpdateObservable<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }

        protected void UpdateObservable<T>(ref T field, T value, Action callback, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
                callback?.Invoke();
            }
        }

        public virtual void ShowProgressBar()
        {
            IsActivity = true;
        }

        public virtual void HideProgressBar()
        {
            IsActivity = false;
        }

        public virtual void AcceptChanges()
        {
            _isChanged = false;
        }
    }
    public class ParentBaseViewModel<T> : BaseViewModel, IBaseViewModel<T>, IEditableObject where T : ParentBaseViewModel<T>
    {
        public T OriginalObject { get; set; }

        public bool IsEdit { get; set; }

        //public ParentBaseViewModel()
        //{
        //    AttachEventHandlers();
        //}

        public virtual void BeginEdit()
        {
            if (!IsEdit)
            {
                OriginalObject = Utility.DeepCopy(this as T);
                IsEdit = true;
            }
        }

        public virtual void CancelEdit()
        {
            if (!IsEdit)
            {
                return;
            }

            PropertyInfo[] properties = GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (OriginalObject != null && propertyInfo.SetMethod != null)
                {
                    object value = propertyInfo.GetValue(OriginalObject);
                    propertyInfo.SetValue(this, value);
                    OnPropertyChanged(propertyInfo.Name);
                }
            }

            IsEdit = false;
        }

        public virtual void EndEdit()
        {
            if (IsEdit)
            {
                OriginalObject = null;
                IsEdit = false;
            }
        }

        public override void AcceptChanges()
        {
            base.AcceptChanges();
            FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (FieldInfo fieldInfo in fields)
            {
                if (fieldInfo.GetValue(this) is BaseViewModel baseViewModel)
                {
                    baseViewModel.AcceptChanges();
                }

                if (!(fieldInfo.GetValue(this) is IEnumerable enumerable) || fieldInfo.GetValue(this) is string)
                {
                    continue;
                }

                foreach (object item in enumerable)
                {
                    if (item is BaseViewModel baseViewModel2)
                    {
                        baseViewModel2.AcceptChanges();
                    }
                }
            }
        }

        protected void AttachEventHandlers()
        {
            AttachHandlersRecursive(this, new HashSet<object>());
        }

        private void AttachHandlersRecursive(object obj, HashSet<object> visited)
        {
            if (obj == null || obj is string || visited.Contains(obj))
                return;

            visited.Add(obj);

            if (obj is INotifyPropertyChanged npc)
                npc.PropertyChanged += Property_Changed;

            var type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            object value;
            foreach (var prop in properties)
            {
                if (!prop.CanRead) continue;
                
                try
                {
                    value = prop.GetValue(obj);
                }
                catch
                {
                    continue; // safely skip inaccessible or problematic property
                }
                if (value == null || value is string)
                    continue;

                // Attach to ObservableCollection
                if (value is INotifyCollectionChanged collection)
                {
                    collection.CollectionChanged += (s, e) =>
                    {
                        if (e.NewItems != null)
                        {
                            foreach (var newItem in e.NewItems)
                            {
                                AttachHandlersRecursive(newItem, visited);
                            }
                        }
                        if (e.OldItems != null)
                        {
                            foreach (var oldItem in e.OldItems)
                            {
                                DetachPropertyChanged(oldItem);
                            }
                        }

                        base.IsChanged = true;
                    };
                }

                // Attach to items in collection
                if (value is IEnumerable enumerable)
                {
                    foreach (var item in enumerable)
                    {
                        AttachHandlersRecursive(item, visited);
                    }
                    continue;
                }

                // Recurse into properties of nested BaseViewModel
                if (value is BaseViewModel)
                {
                    AttachHandlersRecursive(value, visited);
                }
            }
        }

        private void DetachPropertyChanged(object obj)
        {
            if (obj is INotifyPropertyChanged npc)
                npc.PropertyChanged -= Property_Changed;

            if (obj is IEnumerable enumerable && !(obj is string))
            {
                foreach (var item in enumerable)
                {
                    DetachPropertyChanged(item);
                }
            }
        }


        private void Property_Changed(object sender, PropertyChangedEventArgs e)
        {
            IsChanged = true;
        }
    }
}
