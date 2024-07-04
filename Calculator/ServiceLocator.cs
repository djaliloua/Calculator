using Calculator.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator
{
    public class ServiceLocator
    {
        public static InputResultViewModel InputResultViewModel => GetService<InputResultViewModel>();
        public static MainViewModel MainViewModel => GetService<MainViewModel>();
        public static KeyBoardViewModel KeyBoardViewModel => GetService<KeyBoardViewModel>();
        public static BottomViewModel BottomViewModel => GetService<BottomViewModel>();
        public static T GetService<T>() => App.ServiceProvider.GetRequiredService<T>();

    }
}
