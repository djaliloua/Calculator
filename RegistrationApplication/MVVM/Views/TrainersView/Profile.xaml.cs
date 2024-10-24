using RegistrationApplication.Utility;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Drawing = System.Drawing;

namespace RegistrationApplication.MVVM.Views.TrainersView
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {
        public Profile()
        {
            InitializeComponent();
        }
    }
    public class CustomImage : Image
    {
        public byte[] PictureByte
        {
            get { return (byte[])GetValue(PictureByteProperty); }
            set { SetValue(PictureByteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PictureByte.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PictureByteProperty =
            DependencyProperty.Register("PictureByte", typeof(byte[]), typeof(CustomImage), new PropertyMetadata(OnPictureByteChanged));

        private static void OnPictureByteChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Image image = sender as Image;
            if (image != null && e.NewValue != null)
            {
                byte[] b = (byte[])e.NewValue;
                if (b.Length > 0)
                    image.Source = ImageUtility.ConvertToBitmap(b);
            }
            else
            {
                image.Source = ImageUtility.ConvertToBitmap(ImageUtility.ImageToByteArray(Drawing.Image.FromFile(@"DefaultResources\h1.png")));
            }
        }
        

    }
}
