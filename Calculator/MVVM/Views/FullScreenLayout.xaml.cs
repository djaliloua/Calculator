using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator.MVVM.Views
{
    /// <summary>
    /// Interaction logic for FullScreenLayout.xaml
    /// </summary>
    public partial class FullScreenLayout : UserControl
    {
        public FullScreenLayout()
        {
            InitializeComponent();
            bottom.Loaded += Bottom_Loaded;
        }

        private void Bottom_Loaded(object sender, RoutedEventArgs e)
        {
            if(ServiceLocator.MainViewModel.IsFullScreen)
            {
                bottom.txt.Visibility = Visibility.Hidden;
            }
        }
    }
}
