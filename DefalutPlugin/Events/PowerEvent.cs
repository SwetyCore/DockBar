using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DefalutPlugin.Common.Battery.BatteryHelper;

namespace DefalutPlugin.Events
{
    internal class PowerEvent
    {

        public enum PowerStates
        {
            Refresh
        }
        public Batterys Arg { get; set; }
        public PowerStates PowerState { get; set; }
    }

    
}
