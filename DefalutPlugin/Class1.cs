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
            name="����ʱ��",
            description= "������ʱ����",
            settingPage = typeof(View.SettingPage)
        };


        public Type mainView => typeof(View.ClockControl);

    }

    public class TrayPlugin : IPlugin
    {
        public static pluginInfo info => new pluginInfo()
        {
            name = "���̲˵�",
            description = "�����̲˵���",
            settingPage = typeof(View.SettingPage)
        };


        public Type mainView => typeof(View.TrayControl);

    }

    public class PowerPlugin : IPlugin
    {
        public static pluginInfo info => new pluginInfo()
        {
            name = "����",
            description = "�ǵ�����ʾ��",
            settingPage = typeof(View.SettingPage)
        };


        public Type mainView => typeof(View.BatteryControl);

    }
}
