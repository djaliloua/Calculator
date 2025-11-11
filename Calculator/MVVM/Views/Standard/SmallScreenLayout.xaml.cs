using System.Windows.Controls;

namespace Calculator.MVVM.Views.Standard
{
    /// <summary>
    /// Interaction logic for SmallScreenLayout.xaml
    /// </summary>
    public partial class SmallScreenLayout : UserControl
    {
        public SmallScreenLayout(int height=450)
        {
            InitializeComponent();
            bottomView.Height = height;
        }
    }
}
