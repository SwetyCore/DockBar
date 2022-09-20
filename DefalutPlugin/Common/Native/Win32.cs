using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static FinderDemo.Common.WindowEffects;

namespace FinderDemo.Common.Native
{
    public static class Win32
    {
        #region Blur
        public const int WS_MINIMIZEBOX = 131072;

        public const int WS_MAXIMIZEBOX = 65536;

        public const int WS_SYSMENU = 524288;

        public const int GWL_STYLE = -16;

        public const int SW_SHOWNORMAL = 1;

        public const int SC_CLOSE = 61536;

        public const int MF_ENABLED = 0;

        public const int MF_GRAYED = 1;

        public const int MF_DISABLED = 2;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32")]
        public static extern uint SetWindowLong(IntPtr hwnd, int nIndex, int NewLong);

        [DllImport("user32.dll")]
        public static extern bool EnableMenuItem(IntPtr hMenu, int uIDEnableItem, int uEnable);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, int bRevert);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowText(IntPtr hwnd, string lpString);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool ShowWindow(IntPtr hWnd, short State);

        [DllImport("user32.dll")]
        private static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMarInset);

        public static void EnableBlur(IntPtr hwind)
        {
            try
            {
                AccentPolicy structure = default(AccentPolicy);
                int num = Marshal.SizeOf(structure);
                structure.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;
                IntPtr intPtr = Marshal.AllocHGlobal(num);
                Marshal.StructureToPtr(structure, intPtr, fDeleteOld: false);
                WindowCompositionAttributeData windowCompositionAttributeData = default(WindowCompositionAttributeData);
                windowCompositionAttributeData.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
                windowCompositionAttributeData.SizeOfData = num;
                windowCompositionAttributeData.Data = intPtr;
                WindowCompositionAttributeData data = windowCompositionAttributeData;
                SetWindowCompositionAttribute(hwind, ref data);
                Marshal.FreeHGlobal(intPtr);
            }
            catch (Exception)
            {
            }
        }

        public static bool EnableDropShadow(IntPtr hwind, Margins margins)
        {
            try
            {
                int attrValue = 2;
                if (DwmSetWindowAttribute(hwind, 2, ref attrValue, 4) == 0)
                {
                    Margins pMarInset = new Margins
                    {
                        Bottom = 0,
                        Left = 0,
                        Right = 0,
                        Top = 0
                    };
                    int num = DwmExtendFrameIntoClientArea(hwind, ref pMarInset);
                    return num == 0;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Tray 
        public const int MEM_RELEASE = 0x8000;
        public const int MEM_COMMIT = 0x1000;
        public const int PAGE_READWRITE = 0x04;
        public const int WM_USER = 0x0400;
        public const int TB_GETBUTTON = (WM_USER + 23);
        public const int TB_BUTTONCOUNT = (WM_USER + 24);
        public const int TB_HIDEBUTTON = (WM_USER + 4);
        public const int STILL_ACTIVE = 0x0103;

        public static int WM_NOTIFYCALLBACK = WM_USER + 1;

        [StructLayout(LayoutKind.Sequential)]
        public class TRAYDATA
        {
            public IntPtr hwnd { get; set; }
            public uint uID;
            public uint uCallbackMessage;
            public int Reserved0;
            public int Reserved1;
            public IntPtr hIcon;                //托盘图标的句柄 
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class TBBUTTON
        {
            public int iBitmap;
            public int idCommand;
            public byte fsState;
            public byte fsStyle;
            public byte bReserved0;
            public byte bReserved1;
            public byte bReserved2;
            public byte bReserved3;
            public byte bReserved4;
            public byte bReserved5;
            public IntPtr dwData;
            public IntPtr iString;
        }
        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern int VirtualAllocEx(IntPtr hProcess,
            int lpAddress,
            int dwSize,
            int flAllocationType,
            int flProtect);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess, int lpAddress,
           int dwSize, uint dwFreeType);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hHandle);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int SendMessage(IntPtr hWnd, Int32 Msg, Int32 wParam, Int32 lParam);
        [DllImport("user32", SetLastError = true)]
        public static extern bool PostMessage(
                IntPtr hWnd,
                uint Msg,
                int wParam,
                int lParam
                );
        [DllImport("kernel32.dll", SetLastError = true, PreserveSig = true)]
        public static extern int ReadProcessMemory(IntPtr hProcess, int lpBaseAddress,
             IntPtr lpBuffer, int nSize, out int lpNumberOfBytesRead);
        [DllImport("kernel32.dll", SetLastError = true, PreserveSig = true)]
        public static extern int ReadProcessMemory(IntPtr hProcess, int lpBaseAddress,
             byte[] lpBuffer, int nSize, out int lpNumberOfBytesRead);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, ref int ProcessId);


        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, UInt32 dwProcessId);
        [DllImport("psapi.dll", SetLastError = true)]
        public static extern int GetProcessImageFileName(IntPtr hProcess, StringBuilder lpImageFileName, int nSize);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, int ucchMax);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetExitCodeProcess(IntPtr hProcess, ref int lpExitCode);
        #endregion
    }

}
