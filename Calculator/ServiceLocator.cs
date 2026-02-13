using Calculator.MVVM.ViewModels;
using Calculator.MVVM.ViewModels.Standard;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator
{
    public class ServiceLocator
    {
        public static SettingsViewModel SettingsViewModel => GetService<SettingsViewModel>();
        public static InputResultViewModel InputResultViewModel => GetService<InputResultViewModel>();
        public static MainViewModel MainViewModel => GetService<MainViewModel>();
        public static KeyBoardViewModel KeyBoardViewModel => GetService<KeyBoardViewModel>();
        public static BottomViewModel BottomViewModel => GetService<BottomViewModel>();
        public static StandardCalculatorViewModel StandardCalculatorViewModel => GetService<StandardCalculatorViewModel>();
        public static OperationListViewModel OperationListViewModel => GetService<OperationListViewModel>();
        public static T GetService<T>() => App.ServiceProvider.GetRequiredService<T>();

    }
}
