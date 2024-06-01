using System.IO;
using System.Windows;

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
        public static bool AvoidMore()
        {
            using (Mutex mutex = new Mutex(false, "MutexDemo"))
            {
                //Checking if Other External Thread is Running
                if (!mutex.WaitOne(5000, false))
                {
                    MessageBox.Show("An Instance of the Application is Already Running");
                    return false;
                }
                return true;
            }
        }
    }
}
