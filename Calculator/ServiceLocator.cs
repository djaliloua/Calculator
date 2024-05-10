using Calculator.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator
{
    public class ServiceLocator
    {
        public static InputResultViewModel InputResultViewModel => GetViewModel<InputResultViewModel>();
        public static MainViewModel MainViewModel => GetViewModel<MainViewModel>();
        public static KeyBoardViewModel KeyBoardViewModel => GetViewModel<KeyBoardViewModel>();
        public static BottomViewModel BottomViewModel => GetViewModel<BottomViewModel>();
        public static T GetViewModel<T>() => App.ServiceProvider.GetRequiredService<T>();

    }
}
