using PluginBase;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using static PluginBase.PluginBase;

namespace DefalutPlugin
{
    public class ClockPlugin : IPlugin
    {
        public static pluginInfo info => new pluginInfo()
        {
            name="数字时钟",
            description= "是数字时钟捏",
            settingPage = typeof(View.SettingPage)
        };


        public Type mainView => typeof(View.ClockControl);

    }

    public class TrayPlugin : IPlugin
    {
        public static pluginInfo info => new pluginInfo()
        {
            name = "托盘菜单",
            description = "是托盘菜单捏",
            settingPage = typeof(View.SettingPage)
        };


        public Type mainView => typeof(View.TrayControl);

    }

    public class PowerPlugin : IPlugin
    {
        public static pluginInfo info => new pluginInfo()
        {
            name = "电量",
            description = "是电量显示捏",
            settingPage = typeof(View.SettingPage)
        };


        public Type mainView => typeof(View.BatteryControl);

    }
}
