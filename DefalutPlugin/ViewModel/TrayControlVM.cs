using CommunityToolkit.Mvvm.ComponentModel;
using FinderDemo.Common;
using FinderDemo.Common.Native;
using FinderDemo.Common.Tray;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using static FinderDemo.Common.Native.Win32;

namespace DefalutPlugin.ViewModel
{
    partial class TrayControlVM : ObservableObject
    {
        public class RelayCommand : ICommand
        {
            readonly Action<object> _execute;
            readonly Predicate<object> _canExecute;

            /// <summary>
            /// Creates a new command that can always execute.
            /// </summary>
            /// <param name="execute">The execution logic.</param>
            public RelayCommand(Action<object> execute) : this(execute, null)
            {
            }

            /// <summary>
            /// Creates a new command.
            /// </summary>
            /// <param name="execute">The execution logic.</param>
            /// <param name="canExecute">The execution status logic.</param>
            public RelayCommand(Action<object> execute, Predicate<object> canExecute)
            {
                if (execute == null)
                    throw new ArgumentNullException("execute");
                _execute = execute;
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute == null ? true : _canExecute(parameter);
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public void Execute(object parameter)
            {
                _execute(parameter);
            }

        }


        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 2) };

        public TrayControlVM()
        {
            timer.Tick += (a, b) =>
            {


                LoadTrayItems();


            };
            timer.Start();

            _R_CLICK_Command = new RelayCommand(param => On_Delete_Command_Excuted(param, MouseActions.R_CLICK));
            _L_CLICK_Command = new RelayCommand(param => On_Delete_Command_Excuted(param, MouseActions.L_CLICK));
            _L_DB_CLICK_Command = new RelayCommand(param => On_Delete_Command_Excuted(param, MouseActions.L_DB_CLICK));
            _HOVER_Command = new RelayCommand(param => On_Delete_Command_Excuted(param, MouseActions.HOVER));
        }

        public void Stop()
        {
            timer.Stop();
        }

        public class TrayItem
        {
            public string tip { get; set; }

            public string trayhWnd { get; set; }

            public ImageSource icon { get; set; }

            public TRAYDATA traydata { get; set; }

            public string pName { get; set; }
        }

        private void LoadTrayItems()
        {
            SysTrayWnd.TrayItemData[] trayItems = SysTrayWnd.GetTrayWndDetail(SysTrayWnd.GetTrayWnd());
            SysTrayWnd.TrayItemData[] overFlowtrayItems = SysTrayWnd.GetTrayWndDetail(SysTrayWnd.GetOverflowTrayWnd());
            var t = new ObservableCollection<TrayItem>();
            var ot = new ObservableCollection<TrayItem>();
            foreach (var item in trayItems)
            {
                try
                {

                    var ti = new TrayItem();
                    var ico = Icon.FromHandle(item.hIcon);
                    ti.icon = WindowTest.BitmapToBitmapSource(Icon.FromHandle(item.hIcon).ToBitmap());
                    ti.tip = item.lpTrayToolTip;
                    ti.traydata = item.trayData;
                    ti.pName = item.processName;
                    t.Add(ti);
                }
                catch (Exception ex)
                {

                }
            }

            foreach (var item in overFlowtrayItems)
            {
                try
                {

                    var ti = new TrayItem();
                    var ico = Icon.FromHandle(item.hIcon);
                    ti.icon = WindowTest.BitmapToBitmapSource(Icon.FromHandle(item.hIcon).ToBitmap());
                    ti.tip = item.lpTrayToolTip;
                    ti.traydata = item.trayData;
                    ti.pName = item.processName;

                    ot.Add(ti);
                }
                catch (Exception ex)
                {

                }
            }

            TrayItems = t;
            OverFlowTrayItems = ot;
        }

        [ObservableProperty]
        private ObservableCollection<TrayItem> trayItems;

        [ObservableProperty]
        private ObservableCollection<TrayItem> overFlowTrayItems;

        private RelayCommand _R_CLICK_Command;
        private RelayCommand _L_CLICK_Command;
        private RelayCommand _L_DB_CLICK_Command;
        private RelayCommand _HOVER_Command;

        public ICommand R_CLICK_Command
        {
            get
            {
                return _R_CLICK_Command;
            }
        }
        public ICommand L_CLICK_Command
        {
            get
            {
                return _L_CLICK_Command;
            }
        }
        public ICommand L_DB_CLICK_Command
        {
            get
            {
                return _L_DB_CLICK_Command;
            }
        }
        public ICommand HOVER_Command
        {
            get
            {
                return _HOVER_Command;
            }
        }


        public enum MouseActions
        {
            L_CLICK,
            L_DB_CLICK,
            R_CLICK,
            HOVER
        }
        private void On_Delete_Command_Excuted(Object param, MouseActions a)
        {

            var item = TrayItems.Where(x => x.traydata.hwnd == (IntPtr)param).FirstOrDefault() ?? OverFlowTrayItems.Where(x => x.traydata.hwnd == (IntPtr)param).FirstOrDefault();
            if (item == null)
            {

                return;
            }
            switch (a)
            {
                case MouseActions.L_CLICK:
                    {
                        Win32.PostMessage((IntPtr)item.traydata.hwnd, item.traydata.uCallbackMessage, (int)item.traydata.uID, 0x0201);
                        Win32.PostMessage((IntPtr)item.traydata.hwnd, item.traydata.uCallbackMessage, (int)item.traydata.uID, 0x0202);
                    }
                    break;
                case MouseActions.L_DB_CLICK:
                    {
                        Win32.PostMessage((IntPtr)item.traydata.hwnd, item.traydata.uCallbackMessage, (int)item.traydata.uID, 0x0203);

                    }
                    break;
                case MouseActions.R_CLICK:
                    {

                        Win32.PostMessage((IntPtr)item.traydata.hwnd, item.traydata.uCallbackMessage, (int)item.traydata.uID, 0x0204);
                        Win32.PostMessage((IntPtr)item.traydata.hwnd, item.traydata.uCallbackMessage, (int)item.traydata.uID, 0x0205);
                    }
                    break;
                case MouseActions.HOVER:
                    {
                        Win32.PostMessage((IntPtr)item.traydata.hwnd, item.traydata.uCallbackMessage, (int)item.traydata.uID, 0x0200);

                    }
                    break;
                default:
                    break;
            }
        }

    }
}
