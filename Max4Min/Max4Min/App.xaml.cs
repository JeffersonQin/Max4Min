using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;

namespace Max4Min
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static TaskbarIcon taskbarIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            taskbarIcon = (TaskbarIcon)FindResource("Taskbar");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Runtime.UnHook();
            Max4Min.Properties.Settings.Default.Save();
        }
    }
}
