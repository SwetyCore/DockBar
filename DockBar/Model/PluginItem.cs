using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static PluginBase.PluginBase;

namespace DockBar.Model
{

    internal enum Position
    {
        Left,
        Center,
        Right
    }
    internal class PluginItem
    {

        public IPlugin plugin { get; set; }


    }
}
