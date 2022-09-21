using System;
using System.Windows;
namespace DockBar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal ViewModel.MainWindowVM vm;

        public MainWindow()
        {
            InitializeComponent();
            Wpf.Ui.Appearance.Background.Apply(this, Wpf.Ui.Appearance.BackgroundType.Mica);

        }

        private void WindowCloseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Environment.Exit(0);
        }

        private void SettingWindowMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new View.SettingWindow().Show();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            vm = new ViewModel.MainWindowVM();
            DataContext = vm;

        }
    }
}
