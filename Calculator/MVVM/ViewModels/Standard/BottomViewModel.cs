using Calculator.DataAccessLayer.Implementations;
using Calculator.MVVM.Models;
using Calculator.SettingsLayer.Abstractions;
using Microsoft.Extensions.Logging;
using MVVM;
using Patterns.Abstractions;
using Patterns.Implementations;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace Calculator.MVVM.ViewModels.Standard
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
                ServiceLocator.StandardCalculatorViewModel.IsBottomDrawerOpen = false;
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
            return items;
        }
    }
    public class BottomViewModel: BottomViewModelLoadable<Operation>
    {
        #region Private properties
        private readonly int _threshold;
        private readonly ISettingsManager _settings;
        private readonly ILogger<BottomViewModel> _logger;
        private ICollectionView _myCollectionView;
        #endregion

        #region Commands
        public ICommand DeleteAllCommand { get; private set; }
        #endregion

        #region Constructor
        public BottomViewModel(
            ILogger<BottomViewModel> logger,
            ISettingsManager settings,
            ILoadService<Operation> loadService):base(loadService)
        {
            _logger = logger;
            _settings = settings;
            _threshold = (int)_settings.GetParameter("CountThreshold");
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
                using var repo = new Repository();
                await LoadItems(repo.GetAllItems());
                IsLblVisible = !IsEmpty;
            });
            _myCollectionView = CollectionViewSource.GetDefaultView(Items);
            _myCollectionView.SortDescriptions.Clear();
            _myCollectionView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Descending));
        }
        #endregion

        #region Handlers
        private void OnDeleteAll(object parameter)
        {
            DeleteAllItems();
        }
        #endregion

        #region Override Methods
        public override void AddItem(Operation item)
        {
            base.AddItem(item);
            _myCollectionView = CollectionViewSource.GetDefaultView(Items);
            _myCollectionView.SortDescriptions.Clear();
            _myCollectionView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Descending));
        }
        public override void DeleteAllItems()
        {
            using var repo = new Repository();
            repo.DeleteAllAsync();
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
        #endregion
    }
}
