using Notification.Wpf;
using Notification.Wpf.Base.Options;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using System.Diagnostics;

namespace RegistrationApplication
{
    public static class Notifier
    {
        private static CustomizedOptions Options;
        private static readonly NotificationManager notificationManager = new();
        static Notifier()
        {
            Options = new CustomizedOptions()
            {
                Background= new SolidColorBrush(Colors.White),
                Foreground =Brushes.DarkRed,
                Icon = PackIconKind.Star
            };
        }
        public static void Show(string message)
        {
            var content = new NotificationContent()
            {
                Title="Notification",
                Message=message,
                Type = NotificationType.Success,
                TrimType = NotificationTextTrimType.Attach, // will show attach button on message
                RowsCount = 3, //Will show 3 rows and trim after
            };
            notificationManager.Show(content);
        }
        public static void Show(string message, Action callback)
        {
            var content = new NotificationContent()
            {
                Title = "Notification",
                Message = message,
                Type = NotificationType.Success,
                TrimType = NotificationTextTrimType.Attach, // will show attach button on message
                RowsCount = 3, //Will show 3 rows and trim after
                //Background = Options.Background,
                //Foreground = Options.Foreground,
                RightButtonContent = "Open",
                LeftButtonContent = "Cancel",
                RightButtonAction = callback,
                LeftButtonAction = () =>
                {
                    Debug.WriteLine(message);
                }
            };
            notificationManager.Show(content);
        }

    }
}
