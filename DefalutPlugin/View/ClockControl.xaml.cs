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


namespace DefalutPlugin.View
{
    /// <summary>
    /// ClockControl.xaml 的交互逻辑
    /// </summary>
    public partial class ClockControl : IPluginControl
    {
        public ClockControl(Guid g)
        {
            InitializeComponent();

            PluginGuid = g;

        }

        public Guid PluginGuid { get; set; }

        public pluginInfo pluginInfo => ClockPlugin.info;


        public void OnDisabled()
        {
        }

        public void OnEnabled()
        {
            DataContext = new ClockViewModel();

        }

    }
}
