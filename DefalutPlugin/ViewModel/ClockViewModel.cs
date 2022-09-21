using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DefalutPlugin.ViewModel
{
    partial class ClockViewModel:ObservableRecipient, IRecipient<SettingVM.ClockSettingChangedMsg>
    {
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 1) };

        [ObservableProperty]
        private string timeNow;

        private string formatstr = "f";
        private Guid guid;
        public ClockViewModel(Guid g)
        {
            timer.Tick += (a, b) =>
            {

                TimeNow = DateTime.Now.ToString(formatstr);

            };
            timer.Start();
            guid = g;
            IsActive=true;
        }

        public void Receive(SettingVM.ClockSettingChangedMsg message)
        {
            if (message.Value.guid==guid)
            {
                formatstr = message.Value.formatstr;
            }
            //DateTime.Now.ToString()
        }
    }
}
