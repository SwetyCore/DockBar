using DockBar.Helper.Win32.Native;
using System;
using System.Drawing.Printing;
using System.Runtime.InteropServices;

namespace DockBar.Common
{
    internal class BlurUtils
    {



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
                User32.SetWindowCompositionAttribute(hwind, ref data);
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
                if (DWMAPI.DwmSetWindowAttribute(hwind, 2, ref attrValue, 4) == 0)
                {
                    Margins pMarInset = new Margins
                    {
                        Bottom = 0,
                        Left = 0,
                        Right = 0,
                        Top = 0
                    };
                    int num = DWMAPI.DwmExtendFrameIntoClientArea(hwind, ref pMarInset);
                    return num == 0;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
