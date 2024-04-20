using Calculator.DataAccessLayer;
using System.Windows;
using System.Windows.Input;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IRepository repository;
        public MainWindow(IRepository repository)
        {
            InitializeComponent();
            this.repository = repository;
            Closing += MainWindow_Closing;
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var f = FocusManager.GetFocusedElement(this);
            if (f != null)
            {
                Keyboard.Focus(f);
            }
        }

        private async void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            await repository.DeleteAll();
        }
    }
}