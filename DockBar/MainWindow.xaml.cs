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
        }

        private void WindowCloseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Environment.Exit(0);
        }

        private void SettingWindowMenuItem_Click(object sender, RoutedEventArgs e)
        {
            new View.SettingWindow().Show();
            this.UpdateLayout();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            vm = new ViewModel.MainWindowVM();
            DataContext = vm;

        }
    }
}
