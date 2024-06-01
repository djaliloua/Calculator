using System.IO;

namespace Calculator
{
    public static class Helper
    {
        public static string GetLocalPath(string folderName = "Calculator")
        {
            string p = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string pp = Path.Combine(p, folderName);
            if (!Directory.Exists(pp))
                Directory.CreateDirectory(pp);
            return pp;
        }
    }
}
