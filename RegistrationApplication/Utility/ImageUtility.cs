using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace RegistrationApplication.Utility
{
    public static class ImageUtility
    {
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
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
