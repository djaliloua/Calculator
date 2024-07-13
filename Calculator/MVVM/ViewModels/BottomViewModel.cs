using Calculator.DataAccessLayer.Implementations;
using Calculator.MVVM.Models;
using Microsoft.Extensions.Logging;
using MVVM;
using Patterns.Abstractions;
using Patterns.Implementations;
using System.Windows.Input;

namespace Calculator.MVVM.ViewModels
{
    public class BottomViewModelLoadable<TItem> : Loadable<TItem> where TItem : Operation
    {
        public BottomViewModelLoadable(ILoadService<TItem> loadService):base(loadService)
        {
            
        }
        private bool _isLblVisible;
        public bool IsLblVisible
        {
            get => _isLblVisible;
            set => UpdateObservable(ref _isLblVisible, value);
        }
        public bool IsPresent(string inputText)
        {
            Operation op = Items.FirstOrDefault(o => o.OpValue.Equals(inputText));
            return op == null;
        }
        
        public override void SetItems(IEnumerable<TItem> items)
        {
            base.SetItems(items);
            IsLblVisible = !IsEmpty;
        }
        public override void SelectedItemCallBack(TItem selectedItem)
        {
            if(SelectedItem != null)
            {
                setValue(selectedItem);
                ServiceLocator.MainViewModel.IsBottomDrawerOpen = false;
            }
        }
        private void setValue(TItem v)
        {
            ServiceLocator.InputResultViewModel.InputText = v.OpValue;
            ServiceLocator.InputResultViewModel.OutputText = v.OpResult;
        }
    }
    public class LoadOperationService : ILoadService<Operation>
    {
        public IList<Operation> Reorder(IList<Operation> items)
        {
            return items.OrderByDescending(o => o.Id).ToList();
        }
    }
    public class BottomViewModel: BottomViewModelLoadable<Operation>
    {
        private Repository repository;

        private readonly ILogger<BottomViewModel> logger;
        
        public ICommand DeleteAllCommand { get; private set; }
        public BottomViewModel(Repository _repository, 
            ILogger<BottomViewModel> _log, 
            ILoadService<Operation> loadService):base(loadService)
        {
            repository = _repository;
            logger = _log;
            logger.LogInformation("BottomViewModel started.....");
            Init();
            DeleteAllCommand = new DelegateCommand(OnDeleteAll);
        }
        private async void Init()
        {
            await Task.Run(async () =>
            {
                await LoadItems(repository.GetAllItems());
                IsLblVisible = !IsEmpty;
            });
        }
        private void OnDeleteAll(object parameter)
        {
            DeleteAllItems();
        }
        public override void DeleteAllItems()
        {
            repository.DeleteAllAsync();
            base.DeleteAllItems();
        }
        
    }
}
