using Calculator.MVVM.Views.Standard;
using Calculator.SettingsLayer.Abstractions;
using MVVM;
using System.Windows.Controls;

namespace Calculator.MVVM.ViewModels.Standard;

public class StandardCalculatorViewModel : BaseViewModel
{
    public static event Action BottomDrawerOpened;
    protected virtual void OnBottomDrawerOpened() => BottomDrawerOpened?.Invoke();

    public void CloseBottomDawer()
    {
        IsBottomDrawerOpen = false;
    }

    private bool _canMinimize;
    public bool IsFullScreen
    {
        get => _canMinimize;
        set => UpdateObservable(ref _canMinimize, value, () =>
        {
            if (_canMinimize)
            {
                UserControl = ServiceLocator.GetService<FullScreenLayout>();
            }
            else
            {
                UserControl = ServiceLocator.GetService<SmallScreenLayout>();
            }
        });
    }
    private UserControl _userControl;
    public UserControl UserControl
    {
        get => _userControl;
        set => UpdateObservable(ref _userControl, value);
    }
    private bool _isBottomDrawerOpen;
    public bool IsBottomDrawerOpen
    {
        get => _isBottomDrawerOpen;
        set => UpdateObservable(ref _isBottomDrawerOpen, value, () =>
        {
            if (value == false)
            {
                OnBottomDrawerOpened();
            }
        });
    }
    public StandardCalculatorViewModel()
    {
    }
}
