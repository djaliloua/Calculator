using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RegistrationApplication.MVVM.Views.TrainersView
{
    /// <summary>
    /// Interaction logic for ProfileView.xaml
    /// </summary>
    public partial class ProfileView : UserControl
    {
        public ProfileView()
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
                    image.Source = ConvertToBitmap(b);
            }
        }
        public static BitmapImage ConvertToBitmap(byte[] imageData)
        {
            BitmapImage bmp = new BitmapImage();

            using (MemoryStream strm = new MemoryStream())
            using (MemoryStream ms = new MemoryStream())
            {
                strm.Write(imageData, 0, imageData.Length);
                strm.Position = 0;

                System.Drawing.Image img = System.Drawing.Image.FromStream(strm);

                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.StreamSource = ms;
                bmp.EndInit();
                bmp.Freeze();

                return bmp;
            }
        }

    }
    
}
