using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Calculator.MVVM.Views.Standard
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
        }

        private void OpenGitHub_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "https://github.com/djaliloua/Calculator", UseShellExecute = true });
            }
            catch { }
        }

        private void OpenLicense_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = "https://github.com/djaliloua/Calculator/blob/master/LICENSE", UseShellExecute = true });
            }
            catch { }
        }
    }
}