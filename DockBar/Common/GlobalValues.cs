using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockBar.Common
{
    internal static class GlobalValues
    {
        public static List<Model.PluginItem> allPlugins;


        public static MainWindow GetAppBar()
        {
            return App.Current.MainWindow as MainWindow;
        }

        public static ViewModel.PluginManageVM pmvm = new ViewModel.PluginManageVM();
    }
}
