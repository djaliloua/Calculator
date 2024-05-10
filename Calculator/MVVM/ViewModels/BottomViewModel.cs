using Calculator.DataAccessLayer;
using Calculator.MVVM.Models;
using Microsoft.Extensions.Logging;
using MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Calculator.MVVM.ViewModels
{
    public class BottomViewModel:BaseViewModel
    {
        private IRepository repository;
        public ObservableCollection<Operation> Operations { get; } = new ObservableCollection<Operation>();
        private Operation _selectedOperation;
        public Operation SelectedOperation
        {
            get => _selectedOperation;
            set => UpdateObservable(ref _selectedOperation, value);
        }
        private readonly ILogger<BottomViewModel> logger;
        public ICommand DeleteAllCommand { get; private set; }
        public BottomViewModel(IRepository _repository, ILogger<BottomViewModel> _log)
        {
            repository = _repository;
            logger = _log;
            logger.LogInformation("BottomViewModel started.....");
            load();
            DeleteAllCommand = new DelegateCommand(On_DeleteAll);
        }
        private async void On_DeleteAll(object parameter)
        {
            await repository.DeleteAll();
            await Load();
        }
        public async Task Load()
        {
            Operations.Clear();
            var data = await repository.GetAll();
            foreach (Operation operation in data)
            {
                Operations.Add(operation);
            }
        }
        private async void load()
        {
            await Load();
        }
    }
}
