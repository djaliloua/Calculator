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
        private string _nbr;
        public string Nbr
        {
            get => _nbr;
            set => UpdateObservable(ref _nbr, value);   
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
        #region Private properties
        private const int _threshold = 10;
        private readonly Repository _repositoryOperation;
        private readonly ILogger<BottomViewModel> _logger;
        #endregion

        #region Commands
        public ICommand DeleteAllCommand { get; private set; }
        #endregion

        #region Constructor
        public BottomViewModel(Repository repository, 
            ILogger<BottomViewModel> logger, 
            ILoadService<Operation> loadService):base(loadService)
        {
            _repositoryOperation = repository;
            _logger = logger;
            _logger.LogInformation("BottomViewModel started.....");
            Init();
            DeleteAllCommand = new DelegateCommand(OnDeleteAll);
        }
        #endregion

        #region Private methods
        private async void Init()
        {
            await Task.Run(async () =>
            {
                await LoadItems(_repositoryOperation.GetAllItems());
                IsLblVisible = !IsEmpty;
            });
        }
        #endregion

        #region Handlers
        private void OnDeleteAll(object parameter)
        {
            DeleteAllItems();
        }
        #endregion
        public override void DeleteAllItems()
        {
            _repositoryOperation.DeleteAllAsync();
            base.DeleteAllItems();
        }
        protected override void NumberOfItemsChanged(int count)
        {
            if(count >= _threshold)
            {
                Nbr = $"{_threshold}+";
            }
            else
            {
                Nbr = $"{count}";
            }
        }

    }
}
