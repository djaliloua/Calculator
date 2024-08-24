using static ArchUnitNET.Fluent.ArchRuleDefinition;
using ArchUnitNET.xUnit;
using Calculator.MVVM.ViewModels;
using Calculator.MVVM.Views.Standard;

namespace Calculator.Fitness.Test.Fitness.MVVM.ViewModels
{
    public class NamingConvensionTest
    {
        [Fact]
        public void AllViewModel_ShouldEndWithViewModel()
        {
            Classes()
                .That()
                .ResideInNamespace(typeof(MainViewModel).Namespace)
                .Should()
                .HaveNameEndingWith("ViewModel")
                .Check(Helper.Architecture);
        }
        [Fact]
        public void AllViews_ShouldEndWithView()
        {
            Classes()
                .That()
                .ResideInNamespace(typeof(InputResultView).Namespace)
                .Should()
                .HaveNameEndingWith("View")
                .Check(Helper.Architecture);
        }
    }
}