using PluginBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static PluginBase.PluginBase;

namespace DefalutPlugin.View
{
    /// <summary>
    /// TrayControl.xaml 的交互逻辑
    /// </summary>
    public partial class TrayControl :  PluginControl
    {
        public TrayControl(Guid g)
        {
            InitializeComponent();
            PluginGuid = g;

        }



        public override pluginInfo pluginInfo => TrayPlugin.info;

        public override void OnDisabled()
        {
            (DataContext as ViewModel.TrayControlVM).Stop();
        }

        public override void OnEnabled()
        {
            DataContext = new ViewModel.TrayControlVM();


        }

        private void Button_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled =true;
        }
    }
}
