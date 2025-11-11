using Calculator.MVVM.ViewModels.Standard;
using System.Windows.Controls;

namespace Calculator.MVVM.Views.Standard
{
    /// <summary>
    /// Interaction logic for StandardCalculator.xaml
    /// </summary>
    public partial class StandardCalculator : UserControl
    {
        public StandardCalculator()
        {
            InitializeComponent();
            Loaded += StandardCalculator_Loaded;
            StandardCalculatorViewModel.BottomDrawerOpened += StandardCalculatorViewModel_BottomDrawerOpened;
        }

        private void StandardCalculator_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ServiceLocator.StandardCalculatorViewModel.CloseBottomDawer();
        }

        private void StandardCalculatorViewModel_BottomDrawerOpened()
        {
            if (content.Content is SmallScreenLayout layout)
            {
                layout.keyboard.Focus();
            }

            if (content.Content is FullScreenLayout fulllayout)
            {
                fulllayout.calculator.keyboard.Focus();
            }
        }
        
    }
}
