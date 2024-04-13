using Calculator.DataAccessLayer;
using System.Windows;

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
        }

        private async void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            await repository.DeleteAll();
        }
    }
}