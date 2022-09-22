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
using PluginBase;


namespace DockBar.ViewModel
{
    partial class MainWindowVM:ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<PluginControl> leftContainer = new ObservableCollection<PluginControl>();

        [ObservableProperty]
        private ObservableCollection<PluginControl> centerContainer = new ObservableCollection<PluginControl>();

        [ObservableProperty]
        private ObservableCollection<PluginControl> rightContainer = new ObservableCollection<PluginControl>();
    }
}
