using Calculator.ApplicationLogic;
using Calculator.SettingsLayer.Abstractions;
using CalculatorModel;
using Microsoft.Extensions.Logging;
using MVVM;
using Patterns.Implementation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Calculator.MVVM.ViewModels.Standard;

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

    protected override void SetItems(IEnumerable<TItem> items)
    {
        base.SetItems(items);
        IsLblVisible = !IsEmpty;
    }
    protected void SelectedItemCallBack(TItem selectedItem)
    {
        if (SelectedItem != null)
        {
            setValue(selectedItem);
            ServiceLocator.StandardCalculatorViewModel.IsBottomDrawerOpen = false;
        }
    }
    private void setValue(TItem v)
    {
        ServiceLocator.InputResultViewModel.SetOpValue(v.OpValue, v.OpResult);
    }
}

public class OperationListViewModel : BottomViewModelLoadable<Operation>
{
    private readonly ISettingsManager _settings;
    private readonly int _threshold;
    private readonly IOperationAppService _service;


    public OperationListViewModel(IOperationAppService service, ISettingsManager settings)
    {
        _settings = settings;
        _service = service;
        OnSelectedIem = SelectedItemCallBack;
        OnNumberOfItemsChanged = NumberOfItemsChanged;
        _threshold = (int)_settings.GetParameter("CountThreshold");
        SetSortDescription(new SortDescription("Id", ListSortDirection.Descending));
    }

    public ObservableCollection<Operation> GetAllItems()
    {
        return GetItems();
    }

    public void DeleteAllItemsAsyn()
    {
        DeleteAllItems();
    }
    public void Add(string inputText, string ouputText)
    {
        var op = _service.SaveOperation(inputText, ouputText);
        AddItem(op);
    }
    public async Task LoadAsync()
    {
        if(!IsEmpty)
        {
            return;
        }
        var data = await _service.GetAllOperations();
        await LoadItems(data);
        IsLblVisible = !IsEmpty;
    }
    #region Override Methods
    protected override void DeleteAllItems()
    {
        _service.DeleteAll();
        base.DeleteAllItems();
    }
    protected void NumberOfItemsChanged(int count)
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

public class BottomViewModel : BaseViewModel
{
    #region Private properties
    private readonly ILogger<BottomViewModel> _logger;
    private OperationListViewModel _operationVM;


    public ICommand LoadedCommand { get; private set; }
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
    public BottomViewModel(OperationListViewModel vm, ILogger<BottomViewModel> logger)
    {
        _logger = logger;
        OperationVM = vm;
        _logger.LogInformation("BottomViewModel started.....");
        DeleteAllCommand = new DelegateCommand(OnDeleteAll);
        LoadedCommand = new DelegateCommand(OnLoaded);
    }
    #endregion
    private async void OnLoaded(object parameter)
    {
        await OperationVM.LoadAsync();
    }

    #region Private methods

    #endregion

    #region Handlers
    private void OnDeleteAll(object parameter)
    {
        OperationVM.DeleteAllItemsAsyn();
    }
    #endregion

    public void AddOperation(string inputText, string ouputText)
    {
        OperationVM.Add(inputText, ouputText);
        OnPropertyChanged(nameof(OperationVM));
    }
}