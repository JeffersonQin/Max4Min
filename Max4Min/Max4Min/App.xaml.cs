using System.Windows;

namespace Max4Min
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Runtime.UnHook();
            Max4Min.Properties.Settings.Default.Save();
        }
    }
}
