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
using DefalutPlugin.ViewModel;
using PluginBase;

namespace DefalutPlugin.View
{
    /// <summary>
    /// ClockControl.xaml 的交互逻辑
    /// </summary>
    public partial class ClockControl : PluginControl
    {
        public ClockControl(Guid g)
        {
            InitializeComponent();

            PluginGuid = g;

        }


        public override pluginInfo pluginInfo => ClockPlugin.info;


        public override void OnDisabled()
        {
        }

        public override void OnEnabled()
        {
            DataContext = new ClockViewModel(PluginGuid);
            var a = GetPluginConfigFilePath();
        }

    }
}
