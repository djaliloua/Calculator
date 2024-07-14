using Calculator.MVVM.ViewModels;
using System.Windows.Controls;

namespace Calculator.MVVM.Views
{
    /// <summary>
    /// Interaction logic for CalcFullScreen.xaml
    /// </summary>
    public partial class CalcFullScreen : UserControl
    {
        public CalcFullScreen()
        {
            InitializeComponent();
            MainViewModel.BottomDrawerOpened += MainViewModel_BottomDrawerOpened;
        }
        private void MainViewModel_BottomDrawerOpened()
        {
            keyboard.Focus();
        }
    }
}
