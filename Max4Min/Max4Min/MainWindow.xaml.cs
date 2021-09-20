using System.Windows;

namespace Max4Min
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (Properties.Settings.Default.HookWhenStart) Runtime.Hook();
        }

        private void ButtonHook_Click(object sender, RoutedEventArgs e)
        {
            Runtime.Hook();
            MessageBox.Show("Process performed. You can check whether CTB hook was successfully installed by maximizing windows with shift key pressd. Enjoy~", "Max4Min by gyro永不抽风");
        }

        private void ButtonUnHook_Click(object sender, RoutedEventArgs e)
        {
            Runtime.UnHook();
        }

        private void CheckBoxStartWhenStartUp_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
