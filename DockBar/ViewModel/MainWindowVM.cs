using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static PluginBase.PluginBase;

namespace DockBar.ViewModel
{
    partial class MainWindowVM:ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<IPluginControl> leftContainer = new ObservableCollection<IPluginControl>();

        [ObservableProperty]
        private ObservableCollection<IPluginControl> centerContainer = new ObservableCollection<IPluginControl>();

        [ObservableProperty]
        private ObservableCollection<IPluginControl> rightContainer = new ObservableCollection<IPluginControl>();
    }
}
