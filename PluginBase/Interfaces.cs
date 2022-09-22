using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using static PluginBase.PluginBase;

namespace PluginBase
{
    public abstract class PluginControl: UserControl
    {
       
        
        public Guid PluginGuid { get; set; }

        public abstract void OnEnabled();

        public abstract void OnDisabled();

        public abstract pluginInfo pluginInfo { get; }

        public string GetPluginConfigFilePath()
        {
            var abl = Path.GetDirectoryName(System.Reflection.Assembly.GetCallingAssembly().Location);
            return Path.Combine(abl, $"{PluginGuid}.json");
        }


    }
    public class PluginBase
    {

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
