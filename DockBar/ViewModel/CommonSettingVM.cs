using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf.Ui.Appearance;

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

            if (darkmode)
            {
                Theme.Apply(
                 ThemeType.Dark,
                BackgroundType.Mica, true, false);
            }
            else
            {
                Theme.Apply(
                 ThemeType.Light,
                BackgroundType.Mica, true, false);
            }

        }
    }
}
