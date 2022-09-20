using CommunityToolkit.Mvvm.Messaging;
using DefalutPlugin.Events;
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
using static DefalutPlugin.Common.Battery.BatteryHelper;
using static DefalutPlugin.Events.PowerEvent;
using static PluginBase.PluginBase;

namespace DefalutPlugin.View
{
    /// <summary>
    /// BatteryControl.xaml 的交互逻辑
    /// </summary>
    public partial class BatteryControl : UserControl,IPluginControl
    {
        public BatteryControl(Guid g)
        {
            InitializeComponent();
            PluginGuid = g;
        }

        public Guid PluginGuid { get; set; }

        public pluginInfo pluginInfo => PowerPlugin.info;


        private static Battery BatteryMonitor = new Battery();

        public void OnDisabled()
        {
            BatteryMonitor.Stop();
            BatteryMonitor.Dispose();
        }

        public void OnEnabled()
        {
            DataContext = new ViewModel.PowerPopup_VM();
            BatteryMonitor.Changed += Battery_Changed;
            BatteryMonitor.Start();
        }

        private void Battery_Changed(object sender, BatteryEventargs args)
        {
            WeakReferenceMessenger.Default.Send(new PowerEvent()
            {
                PowerState = PowerStates.Refresh,
                Arg = args.Args
            });
        }
    }
}
