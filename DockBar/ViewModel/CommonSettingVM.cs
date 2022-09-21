using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandyControl.Themes;
using System.Windows;

namespace DockBar.ViewModel
{
    internal class CommonSettingVM:ObservableObject
    {
        private bool useDarkTheme;

        public bool UseDarkTheme
        {
            get { return useDarkTheme; }
            set 
            { 
                SetProperty(ref  useDarkTheme , value);
                ChangeTheme(value);
            }
        }

        private void ChangeTheme(bool darkmode)
        {

            App.Current.Resources.Clear();
            App.Current.Resources = new System.Windows.ResourceDictionary();

            ResourceDictionary resource = new ResourceDictionary();
            ResourceDictionary resource2 = new ResourceDictionary();
            ResourceDictionary resource3 = new ResourceDictionary();
            ResourceDictionary resource4 = new ResourceDictionary();
            resource3.Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml");
            if (darkmode)
            {
                resource.Source = new Uri( "pack://application:,,,/HandyControl;component/Themes/SkinDark.xaml");
                resource2.Source = new Uri("pack://application:,,,/DockBar;component/Style/Theme/Dark.xaml");
            }
            else
            {
                resource.Source = new Uri("pack://application:,,,/HandyControl;component/Themes/SkinDefault.xaml");
                resource2.Source = new Uri("pack://application:,,,/DockBar;component/Style/Theme/Light.xaml");


            }

            App.Current.Resources.MergedDictionaries.Add(resource);
            App.Current.Resources.MergedDictionaries.Add(resource2);
            App.Current.Resources.MergedDictionaries.Add(resource3);
            App.Current.Resources.MergedDictionaries.Add(resource4);

        }
    }
}
