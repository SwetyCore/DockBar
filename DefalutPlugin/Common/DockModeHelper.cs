using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using static FinderDemo.Common.Native.DockMode;

namespace FinderDemo.Common
{
    internal class DockModeHelper
    {
        Window w;
        AppBarDockMode m;
        int size;
        public DockModeHelper(Window w1, AppBarDockMode m1, int size1)
        {
            w = w1;
            m = m1;
            size = size1;   
        }
        private static int _AppBarMessageId;
        public static int AppBarMessageId
        {
            get
            {
                if (_AppBarMessageId == 0)
                {
                    _AppBarMessageId = RegisterWindowMessage("AppBarMessage_EEDFB5206FC3");
                }

                return _AppBarMessageId;
            }
        }

        public bool IsInAppBarResize=false;
        public bool IsAppBarRegistered = false;

        public APPBARDATA GetAppBarData()
        {
            return new APPBARDATA()
            {
                cbSize = Marshal.SizeOf(typeof(APPBARDATA)),
                hWnd = new WindowInteropHelper(w).Handle,
                uCallbackMessage = AppBarMessageId,
                uEdge = (int)m,
                rc = (RECT)GetSelectedMonitor().ViewportBounds
            };
        }
        public enum AppBarDockMode
        {
            Left = 0,
            Top,
            Right,
            Bottom
        }

        public sealed class MonitorInfo : IEquatable<MonitorInfo>
        {
            public Rect ViewportBounds { get; }

            public Rect WorkAreaBounds { get; }

            public bool IsPrimary { get; }

            public string DeviceId { get; }

            internal MonitorInfo(MONITORINFOEX mex)
            {
                this.ViewportBounds = (Rect)mex.rcMonitor;
                this.WorkAreaBounds = (Rect)mex.rcWork;
                this.IsPrimary = mex.dwFlags.HasFlag(MONITORINFOF.PRIMARY);
                this.DeviceId = mex.szDevice;
            }

            public static IEnumerable<MonitorInfo> GetAllMonitors()
            {
                var monitors = new List<MonitorInfo>();
                MonitorEnumDelegate callback = delegate (IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData)
                {
                    MONITORINFOEX mi = new MONITORINFOEX();
                    mi.cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));
                    if (!GetMonitorInfo(hMonitor, ref mi))
                    {
                        throw new Win32Exception();
                    }

                    monitors.Add(new MonitorInfo(mi));
                    return true;
                };

                EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, callback, IntPtr.Zero);

                return monitors;
            }

            public override string ToString() => DeviceId;

            public override bool Equals(object obj) => Equals(obj as MonitorInfo);

            public override int GetHashCode() => DeviceId.GetHashCode();

            public bool Equals(MonitorInfo other) => this.DeviceId == other?.DeviceId;

            public static bool operator ==(MonitorInfo a, MonitorInfo b)
            {
                if (ReferenceEquals(a, b))
                {
                    return true;
                }

                if (ReferenceEquals(a, null))
                {
                    return false;
                }

                return a.Equals(b);
            }

            public static bool operator !=(MonitorInfo a, MonitorInfo b) => !(a == b);
        }

        public MonitorInfo GetSelectedMonitor()
        {
            var allMonitors = MonitorInfo.GetAllMonitors();
            var  monitor = allMonitors.First(f => f.IsPrimary);
            

            return monitor;
        }
        public void SetDock()
        {
            var source = (HwndSource)PresentationSource.FromVisual(w);

            var exstyle = (ulong)GetWindowLongPtr(source.Handle, GWL_EXSTYLE);
            exstyle |= (ulong)((uint)WS_EX_TOOLWINDOW);
            SetWindowLongPtr(source.Handle, GWL_EXSTYLE, unchecked((IntPtr)exstyle));


            var abd = GetAppBarData();
            SHAppBarMessage(ABM.NEW, ref abd);


            // set our initial location
            this.IsAppBarRegistered = true;
            OnDockLocationChanged();
        }

        public void OnDockLocationChanged()
        {
            if (IsInAppBarResize)
            {
                return;
            }

            var abd = GetAppBarData();
            abd.rc = (RECT)GetSelectedMonitor().ViewportBounds;

            SHAppBarMessage(ABM.QUERYPOS, ref abd);

            var dockedWidthOrHeightInDesktopPixels = size;
            switch (m)
            {
                case AppBarDockMode.Top:
                    abd.rc.bottom = abd.rc.top + dockedWidthOrHeightInDesktopPixels;
                    break;
                case AppBarDockMode.Bottom:
                    abd.rc.top = abd.rc.bottom - dockedWidthOrHeightInDesktopPixels;
                    break;
                case AppBarDockMode.Left:
                    abd.rc.right = abd.rc.left + dockedWidthOrHeightInDesktopPixels;
                    break;
                case AppBarDockMode.Right:
                    abd.rc.left = abd.rc.right - dockedWidthOrHeightInDesktopPixels;
                    break;
                default: throw new NotSupportedException();
            }

            SHAppBarMessage(ABM.SETPOS, ref abd);
            IsInAppBarResize = true;
        }
    }
}
