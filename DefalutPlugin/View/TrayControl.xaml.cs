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
    public partial class TrayControl : UserControl, IPluginControl
    {
        public TrayControl(Guid g)
        {
            InitializeComponent();
            PluginGuid = g;
        }

        public Guid PluginGuid { get; set; }

        public pluginInfo pluginInfo => TrayPlugin.info;

        public void OnDisabled()
        {
            (DataContext as ViewModel.TrayControlVM).Stop();
        }

        public void OnEnabled()
        {
            DataContext = new ViewModel.TrayControlVM();


        }
    }
}
