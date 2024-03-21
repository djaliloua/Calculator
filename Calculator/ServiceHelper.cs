using Calculator.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator
{
    public static class ServiceHelper
    {
        public static InputResultViewModel GetInputResultVM() => GetViewModel<InputResultViewModel>();
        public static KeyBoardViewModel GetKeyboardVM() => GetViewModel<KeyBoardViewModel>();
        public static T GetViewModel<T>() => App.ServiceProvider.GetRequiredService<T>();

    }
}
