using Calculator.DataAccessLayer;
using Calculator.MVVM.Models;
using Microsoft.Extensions.Logging;
using MVVM;
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
        public virtual bool IsPresent(string inputText)
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
        private IRepository repository;

        private readonly ILogger<BottomViewModel> logger;
        
        public ICommand DeleteAllCommand { get; private set; }
        public BottomViewModel(IRepository _repository, ILogger<BottomViewModel> _log)
        {
            repository = _repository;
            logger = _log;
            logger.LogInformation("BottomViewModel started.....");
            _ = LoadItems();
            DeleteAllCommand = new DelegateCommand(On_DeleteAll);
        }
        private async void On_DeleteAll(object parameter)
        {
            await repository.DeleteAll();
            DeleteAllItems();
        }
        public override async Task LoadItems()
        {
            var data = await repository.GetAll();
            SetItems(data);
            IsLblVisible = !IsEmpty;
        }
       
    }
}
