using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefalutPlugin.ViewModel
{
    partial class SettingVM : ObservableObject
    {
        public SettingVM(Guid guid)
        {
            
            Clock_Setting.guid = guid;
        }


        public class ClockSetting
        {
            public string formatstr { get; set; }
            public Guid guid { get; set; }
        }

        [ObservableProperty]
        private ClockSetting clock_Setting = new ClockSetting();

        public class ClockSettingChangedMsg : ValueChangedMessage<ClockSetting>
        {
            public ClockSettingChangedMsg(ClockSetting cs) : base(cs)
            {

            }
        }
        [RelayCommand]
        private void SaveClockSetting()
        {

            WeakReferenceMessenger.Default.Send(new ClockSettingChangedMsg(Clock_Setting));
        }
    }
}
