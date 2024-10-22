using System.IO;

namespace RegistrationApplication.MVVM.Models
{
    public class PictureFile
    {
        public string FileExtension { get; set; }
        public string FileName { get; set; }
        public string FullPath { get; set; }
        public PictureFile(string path)
        {
            FullPath = path;
            FileName = Path.GetFileName(path);
            FileExtension = Path.GetExtension(path);
        }
        public PictureFile()
        {
            FullPath = string.Empty;
            FileName = string.Empty;
            FileExtension = string.Empty;
        }

    }
}
