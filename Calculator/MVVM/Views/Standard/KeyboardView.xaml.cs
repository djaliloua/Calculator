using System.Windows.Controls;

namespace Calculator.MVVM.Views.Standard
{
    /// <summary>
    /// Interaction logic for KeyboardView.xaml
    /// </summary>
    public partial class KeyboardView : UserControl
    {
        public KeyboardView()
        {
            InitializeComponent();
            Loaded += KeyboardView_Loaded;
        }

        private void KeyboardView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Focus();
        }
    }
}
