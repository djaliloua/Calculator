using Microsoft.IdentityModel.Tokens;
using Patterns.Implementation;
using RegistrationApplication.DataAccessLayer.Implementations;
using RegistrationApplication.MVVM.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace RegistrationApplication.MVVM.ViewModels.EnrolmentViewModels
{
    public class ParticipiantViewModdel : ParentBaseViewModel<ParticipiantViewModdel>, IDataErrorInfo
    {
        public int ParticipiantId { get; set; }
        public string FirstName { get => field; set => UpdateObservable(ref field, value); }
        public string LastName { get => field; set => UpdateObservable(ref field, value); }
        public int SessionId
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public bool PreviousExperience
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }

        #region Validation
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
        #endregion
    }
    public class LoadableParticipiantViewModdel:Loadable<ParticipiantViewModdel>
    {
        public LoadableParticipiantViewModdel()
        {
            _ = Init();
        }

        private async Task Init()
        {
            using var repo = new ParticipiantRepository();
            await LoadItems(repo.GetAllDtos());
        }
    }
    public class EnrolmentHomeViewModel:BaseViewModel
    {
        public LoadableParticipiantViewModdel Participiants
        {
            get => field;
            set => UpdateObservable(ref field, value);
        }
        public ICommand AddCommand { get; private set; }
        public EnrolmentHomeViewModel()
        {
            Participiants = ServiceLocator.LoadableParticipiantViewModdel;
            AddCommand = new DelegateCommand(OnAdd);
        }

        private void OnAdd(object parameter)
        {
            ServiceLocator.EnrolmentViewModel.SelectedIndex++;
        }
    }
}
