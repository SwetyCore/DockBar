using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DockBar.View
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
            Wpf.Ui.Appearance.Background.Apply(this, Wpf.Ui.Appearance.BackgroundType.Mica);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            var source = (HwndSource)PresentationSource.FromVisual(this);
            Common.BlurUtils.EnableDropShadow(
                source.Handle,
                new Margins(0, 0, 0, 0)
                );
            base.OnSourceInitialized(e);    
        }


    }
}
