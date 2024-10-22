using RegistrationApplication.MVVM.ViewModels;
using RegistrationApplication.MVVM.ViewModels.TrainersViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace RegistrationApplication.MVVM.Views.TrainersView
{
    /// <summary>
    /// Interaction logic for TrainerRegistrationView.xaml
    /// </summary>
    public partial class TrainerRegistrationView : UserControl
    {
        public TrainerRegistrationView()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //var x = wrap.FindName("FlipperClassic");

        }
    }

    public class CustomWrapPanel : WrapPanel
    {
        public bool IsRegistered
        {
            get { return (bool)GetValue(IsRegisteredProperty); }
            set { SetValue(IsRegisteredProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsRegistered.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRegisteredProperty =
            DependencyProperty.Register("IsRegistered", typeof(bool), typeof(CustomWrapPanel), new PropertyMetadata(true));

        public TrainersProfilesViewModel ItemsSource
        {
            get { return (TrainersProfilesViewModel)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource",
                typeof(TrainersProfilesViewModel),
                typeof(CustomWrapPanel),
                new PropertyMetadata(new TrainersProfilesViewModel(), OnItemsSourceChanged)
                );

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WrapPanel wrapPanel = (WrapPanel)d;
            if(e.NewValue != null)
            {
                wrapPanel.Children.Clear();
                TrainersProfilesViewModel trainersProfilesViewModel = (TrainersProfilesViewModel)e.NewValue;
                //ObservableCollection<TrainerViewModel> viewModels = (ObservableCollection<TrainerViewModel>)e.NewValue;
                foreach(TrainerViewModel vm in trainersProfilesViewModel.Items)
                {
                    wrapPanel.Children.Add(new ProfileView() { Margin = new Thickness(15), DataContext = vm });
                }
            }
        }
    }
}
