using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DefalutPlugin.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DefalutPlugin.Common.Battery.BatteryHelper;
using static DefalutPlugin.Events.PowerEvent;

namespace DefalutPlugin.ViewModel
{
    partial class PowerPopup_VM : ObservableRecipient, IRecipient<PowerEvent>
    {
        public PowerPopup_VM()
        {
            IsActive = true;
        }

        [ObservableProperty]
        private Batterys battery;



        [RelayCommand]
        private void OpenBatterySetting()
        {
            //Start.Setting("ms-settings:batterysaver-settings");
        }

        [ObservableProperty]
        private string powerTime;




        public void Receive(PowerEvent message)
        {
            switch (message.PowerState)
            {
                case PowerStates.Refresh:
                    this.Battery = message.Arg;
                    TimeSpan time = new TimeSpan(0, message.Arg.Time, 0);
                    this.PowerTime = $"剩余可用时间为：{time.Hours}小时 {time.Minutes}分钟 {time.Seconds}秒";
                    break;
                default:
                    break;
            }
        }
    }
}
