using Calculator.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator
{
    public static class ServiceHelper
    {
        public static InputResultViewModel InputResultViewModel => GetViewModel<InputResultViewModel>();
        public static MainViewModel MainViewModel => GetViewModel<MainViewModel>();
        public static KeyBoardViewModel KeyBoardViewModel => GetViewModel<KeyBoardViewModel>();
        public static T GetViewModel<T>() => App.ServiceProvider.GetRequiredService<T>();

    }
}
