using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DefalutPlugin.ViewModel
{
    partial class ClockViewModel:ObservableObject
    {
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 1) };

        [ObservableProperty]
        private string timeNow;

        public ClockViewModel()
        {
            timer.Tick += (a, b) =>
            {

                TimeNow = DateTime.Now.ToString("f");

            };
            timer.Start();
        }

    }
}
