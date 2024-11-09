using RegistrationApplication.MVVM.ViewModels.TrainersViewModels;
using System.Windows.Controls;

namespace RegistrationApplication.MVVM.Views.TrainersView
{
    /// <summary>
    /// Interaction logic for ListOfTrainers.xaml
    /// </summary>
    public partial class ListOfTrainers : UserControl
    {
        public ListOfTrainers()
        {
            InitializeComponent();
        }

        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListView listView = sender as ListView;
            if(listView != null && listView.SelectedItem is TrainerViewModel item)
            {
                ServiceLocator.TrainerFormViewModel.Trainer = item;
                ServiceLocator.TrainerFormViewModel.Trainer.BeginEdit();
                ServiceLocator.TrainerFormViewModel.IsSave = false;
                ServiceLocator.TrainerRegistrationViewModel.SeletedIndex = 1;
            }
        }
    }
}
