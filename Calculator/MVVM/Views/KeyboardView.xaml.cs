using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator.MVVM.Views
{
    /// <summary>
    /// Interaction logic for KeyboardView.xaml
    /// </summary>
    public partial class KeyboardView : UserControl
    {
        public KeyboardView()
        {
            InitializeComponent();
        }
        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);
            Keyboard.Focus(this);
        }
    }
}
