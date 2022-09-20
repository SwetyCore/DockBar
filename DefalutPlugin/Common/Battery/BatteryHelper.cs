using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace DefalutPlugin.Common.Battery
{
    internal class BatteryHelper
    {
        public class Batterys
        {
            /// <summary>
            /// 显示名称
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 电池剩余
            /// </summary>
            public int EstimatedCharge { get; set; }
            /// <summary>
            /// 电池的化学特性
            /// </summary>
            public object Chemistry { get; set; }
            /// <summary>
            /// 电池电压，单位为毫伏(millivolts)
            /// </summary>
            public long DesignVoltage { get; set; }
            /// <summary>
            /// 电池状态，一般情况下是a
            /// </summary>
            public string Status { get; set; }

            /// <summary>
            /// 电池充电状态
            /// </summary>
            public int BatteryStatus { get; set; }

            /// <summary>
            /// 剩余可用事件
            /// </summary>
            public int Time { get; set; }
        }


        public class BatteryEventargs : EventArgs
        {
            public Batterys Args { get; set; }
        }


        //限制平台为Windows
        [SupportedOSPlatform("windows")]
        public class Battery : IDisposable
        {
            public delegate void ChangedEventHandle(object sender, BatteryEventargs args);

            /// <summary>
            /// 电池状态更改
            /// </summary>
            public event ChangedEventHandle Changed;

            private void WCreate_EventArrived(object sender, EventArrivedEventArgs e)
            {

                var instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];

                Batterys batt = new Batterys();
                var b = int.Parse(instance.Properties["BatteryStatus"].Value.ToString());
                batt.BatteryStatus = int.Parse(instance.Properties["BatteryStatus"].Value.ToString());
                batt.EstimatedCharge = int.Parse(instance.Properties["EstimatedChargeRemaining"].Value.ToString());

                batt.Chemistry = instance.Properties["Chemistry"].Value;
                batt.DesignVoltage = long.Parse(instance.Properties["DesignVoltage"].Value.ToString());
                batt.Name = instance.Properties["Caption"].Value.ToString();
                batt.Status = (string)instance.Properties["Status"].Value.ToString();
                batt.Time = int.Parse(instance.Properties["EstimatedRunTime"].Value.ToString());
                Changed(this, new BatteryEventargs() { Args = batt });

                //BatteryStatus :1为电池正在放电，2为可接入交流电，但绝对不是在放电（俗称充电）3为充满电，4电量低，5严重电量低，6正在充电(从来都没有见过的数字捏)

                //EstimatedChargeRemaining:为剩余电量，以%为单位
                //Caption：标题
                //Chemistry：化学性质
                //CreationClassName:所属设备
                //Description：名称
                //DesignVoltage:电池电压
                //EstimatedRunTime：剩余可使用时间，以分钟为单位
                //Name：用于识别该对象的字符串
                //PowerManagementSupported:是否可以进行电源管理
                //Status:此设备的运行状态，返回的是一个英文字符串
                //SystemName:获得调用本方法的计算机用户

            }


            WqlEventQuery qCreate = new WqlEventQuery("__InstanceModificationEvent", TimeSpan.FromSeconds(1), "TargetInstance ISA 'Win32_Battery'");
            ManagementEventWatcher wCreate;
            private bool disposedValue;


            public Battery()
            {
                wCreate = new ManagementEventWatcher(qCreate);
                wCreate.EventArrived += WCreate_EventArrived;
                wCreate.Start();
            }




            public void Start()
            {
                if (wCreate != null)
                {
                    wCreate.Start();
                }
            }


            public void Stop()
            {
                if (wCreate != null)
                {
                    wCreate.Stop();
                }
            }




            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: 释放托管状态(托管对象)
                    }

                    // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                    // TODO: 将大型字段设置为 null
                    disposedValue = true;
                }
            }

            // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
            // ~Battery()
            // {
            //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            //     Dispose(disposing: false);
            // }

            public void Dispose()
            {
                // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
    }
}
