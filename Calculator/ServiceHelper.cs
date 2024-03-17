using Calculator.MVVM.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public static class ServiceHelper
    {
        public static InputResultViewModel GetInputResultVM() => App.ServiceProvider.GetRequiredService<InputResultViewModel>();
        public static KeyBoardViewModel GetKeyboardVM() => App.ServiceProvider.GetRequiredService<KeyBoardViewModel>();

    }
}
