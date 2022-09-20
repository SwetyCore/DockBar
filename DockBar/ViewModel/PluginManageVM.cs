using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DockBar.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static PluginBase.PluginBase;

namespace DockBar.ViewModel
{
    partial class PluginManageVM:ObservableObject
    {

        

        public List<Model.PluginItem> AllPlugins
        {
            get { return Common.GlobalValues.allPlugins; }
        }


        public MainWindowVM mwvm
        {
            get 
            { 
                var mv = Common.GlobalValues.GetAppBar();

                if (mv != null)
                {
                    return mv.DataContext as MainWindowVM;
                }
                return null; 
            }
            set 
            {
                //var mv = Common.GlobalValues.GetAppBar();

                //if (mv != null)
                //{
                //     mv.DataContext  =value ;
                //}
            }
        }

        private int _selectedTabIndex;

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set 
            { 
                SetProperty(ref _selectedTabIndex , value);

                GetContainer=SetContainer();
            }
        }
        private ObservableCollection<IPluginControl> SetContainer()
        {
            switch (SelectedTabIndex)
            {
                case 0: return mwvm.LeftContainer;
                case 1: return mwvm.CenterContainer;
                case 2: return mwvm.RightContainer;
                default: return mwvm.LeftContainer;
            }
        }


        [ObservableProperty]
        private ObservableCollection<IPluginControl> getContainer;


        [RelayCommand]
        private void ContentLoaded()
        {
            GetContainer = SetContainer();
        }


        [RelayCommand]
        private void AddItem(object item)
        {
            var pi = item as PluginItem;
            if (pi == null)
            {
                return;
            }
            var i = Activator.CreateInstance(pi.plugin.mainView, System.Guid.NewGuid()) as IPluginControl;
            //pi.plugin.instances.Add(i);
            GetContainer.Add(i );
            i.OnEnabled();
            
        }
        [RelayCommand]
        private void RemoveItem(object item)
        {
            var i = item as IPluginControl;
            GetContainer.Remove(i);
            i.OnDisabled();

        }

        [RelayCommand]
        private void OpemItemSetting(object item)
        {
            var i = item as IPluginControl;
            if (i.pluginInfo.settingPage == null)
            {
                return ;
            }

            new View.PluginSettingWindow(Activator.CreateInstance(i.pluginInfo.settingPage, i.PluginGuid) as Page).Show(); ;

        }

    }
}
