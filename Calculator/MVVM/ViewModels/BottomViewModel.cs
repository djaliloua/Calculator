using Calculator.DataAccessLayer.Implementations;
using Calculator.MVVM.Models;
using Microsoft.Extensions.Logging;
using MVVM;
using Patterns.Implementations;
using System.Windows.Input;

namespace Calculator.MVVM.ViewModels
{
    public abstract class BottomViewModelLoadable<TItem> : Loadable<TItem> where TItem : Operation
    {
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
        protected override void Reorder()
        {
            var data = Items.OrderByDescending(o => o.Id).ToList();
            SetItems(data);
        }
        public override void SetItems(IList<TItem> items)
        {
            base.SetItems(items.ToList());
            IsLblVisible = !IsEmpty;
        }
        public override void SelectedItemCallBack(TItem selectedItem)
        {
            setValue(selectedItem);
            ServiceLocator.MainViewModel.IsBottomDrawerOpen = false;
        }
        private void setValue(TItem v)
        {
            ServiceLocator.InputResultViewModel.InputText = v.OpValue;
            ServiceLocator.InputResultViewModel.OutputText = v.OpResult;
        }
    }
    public class BottomViewModel: BottomViewModelLoadable<Operation>
    {
        private Repository repository;

        private readonly ILogger<BottomViewModel> logger;
        
        public ICommand DeleteAllCommand { get; private set; }
        public BottomViewModel(Repository _repository, ILogger<BottomViewModel> _log)
        {
            repository = _repository;
            logger = _log;
            logger.LogInformation("BottomViewModel started.....");
            _ = LoadItems();
            DeleteAllCommand = new DelegateCommand(OnDeleteAll);
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
        public override async Task LoadItems()
        {
            await Task.Delay(1);
            var data = repository.GetAllItems().ToList();
            SetItems(data);
            IsLblVisible = !IsEmpty;
        }
       
    }
}
