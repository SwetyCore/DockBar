using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace PluginBase
{
    public class PluginBase
    {
        public interface IPluginControl 
        {
            public Guid PluginGuid { get; }

            public void OnEnabled();

            public void OnDisabled();

            public pluginInfo pluginInfo { get; }

        }



        public class pluginInfo
        {
            public ImageSource icon { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public Version version { get; set; }
            public string url { get; set; }
            public string author { get; set; }


            public Type settingPage { get; set; }

        }


        public interface IPlugin
        {
            public static pluginInfo info { get; }


            public Type mainView { get; }



        }
    }
}
