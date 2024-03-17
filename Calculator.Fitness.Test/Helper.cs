
using Calculator.MVVM.ViewModels;

namespace Calculator.Fitness.Test
{
    public static class Helper
    {
        public static readonly Architecture Architecture = new ArchLoader().LoadAssembly(typeof(MainViewModel).Assembly).Build();
    }
}
