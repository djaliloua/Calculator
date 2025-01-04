using Calculator.DataAccessLayer.Implementations;
using Calculator.SettingsLayer.Abstractions;
using CalculatorModel;
using Microsoft.Extensions.Logging;
using MVVM;
using Patterns.Implementation;
using System.ComponentModel;
using System.Windows.Input;

namespace Calculator.MVVM.ViewModels.Standard
{
    public class BottomViewModelLoadable<TItem> : Loadable<TItem> where TItem : Operation
    {
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
                ServiceLocator.StandardCalculatorViewModel.IsBottomDrawerOpen = false;
            }
        }
        private void setValue(TItem v)
        {
            ServiceLocator.InputResultViewModel.InputText = v.OpValue;
            ServiceLocator.InputResultViewModel.OutputText = v.OpResult;
        }
    }

    public class OperationListViewModel: BottomViewModelLoadable<Operation>
    {
        private readonly ISettingsManager _settings;
        private ICollectionView _myCollectionView;
        private readonly int _threshold;
        public OperationListViewModel(ISettingsManager settings)
        {
            _settings = settings;
            _threshold = (int)_settings.GetParameter("CountThreshold");
            SetSortDescription(new SortDescription("Id", ListSortDirection.Descending));
            Init();
        }
        private async void Init()
        {
            await Task.Run(async () =>
            {
                using CalculatorRepository repo = new CalculatorRepository();
                await LoadItems(repo.GetAll());
                IsLblVisible = !IsEmpty;
            });
        }
        #region Override Methods
        public override void DeleteAllItems()
        {
            using var repo = new CalculatorRepository();
            repo.DeleteAllAsync();
            base.DeleteAllItems();
        }
        protected override void NumberOfItemsChanged(int count)
        {
            if (count >= _threshold)
            {
                Nbr = $"{_threshold}+";
            }
            else
            {
                Nbr = $"{count}";
            }
        }
        #endregion
    }

    public class BottomViewModel: BaseViewModel
    {
        #region Private properties
        
        private readonly ISettingsManager _settings;
        private readonly ILogger<BottomViewModel> _logger;

        private OperationListViewModel _operationVM;
        public OperationListViewModel OperationVM
        {
            get => _operationVM;
            set => UpdateObservable(ref _operationVM, value);
        }

        #endregion

        #region Commands
        public ICommand DeleteAllCommand { get; private set; }
        #endregion

        #region Constructor
        public BottomViewModel(ILogger<BottomViewModel> logger, ISettingsManager settings)
        {
            _logger = logger;
            _settings = settings;
            OperationVM = ServiceLocator.OperationListViewModel;
            _logger.LogInformation("BottomViewModel started.....");
            DeleteAllCommand = new DelegateCommand(OnDeleteAll);
        }
        #endregion

        #region Private methods
        
        #endregion

        #region Handlers
        private void OnDeleteAll(object parameter)
        {
            OperationVM.DeleteAllItems();
        }
        #endregion

        
    }
}
