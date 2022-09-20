using FinderDemo.Common.Native;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;

namespace FinderDemo.Helper
{
    public class ContextMenuBlur
    {
        public static readonly DependencyProperty OnProperty =
            DependencyProperty.RegisterAttached(
                "On",
                typeof(bool),
                typeof(ContextMenuBlur),
                new PropertyMetadata(false, OnProperty_OnChanged)
                );

        public static bool GetOn(ContextMenu obj) =>
            (bool)obj.GetValue(OnProperty);

        public static void SetOn(ContextMenu obj, bool value) =>
            obj.SetValue(OnProperty, value);

        private static void OnProperty_OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ContextMenu contextMenu))
                return;

            if ((bool)e.NewValue == true)
                contextMenu.Opened += Popup_Opened;
            else
                contextMenu.Opened -= Popup_Opened;
        }

        private static void Popup_Opened(object sender, EventArgs e)
        {
            if (!(sender is ContextMenu contextMenu))
                return;

            HwndSource source = (HwndSource)PresentationSource.FromVisual(contextMenu);
            if (source == null)
                return;
            Win32.EnableBlur(
                source.Handle
                );

            Win32.EnableDropShadow(
                source.Handle,
                new Margins(0, 0, 0, 0)
                );
        }
    }

    public class BlurPopup
    {
        public static readonly DependencyProperty OnProperty =
            DependencyProperty.RegisterAttached(
                "On",
                typeof(bool),
                typeof(BlurPopup),
                new PropertyMetadata(false, OnProperty_OnChanged)
                );

        public static bool GetOn(Popup obj) =>
            (bool)obj.GetValue(OnProperty);

        public static void SetOn(Popup obj, bool value) =>
            obj.SetValue(OnProperty, value);

        private static void OnProperty_OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Popup popup))
                return;

            if ((bool)e.NewValue == true)
                popup.Opened += Popup_Opened;
            else
                popup.Opened -= Popup_Opened;
        }

        private static void Popup_Opened(object sender, EventArgs e)
        {
            if (!(sender is Popup contextMenu))
                return;

            HwndSource source = (HwndSource)HwndSource.FromVisual(contextMenu.Child);
            if (source == null)
                return;

            Win32.EnableBlur(
                source.Handle
                );

            Win32.EnableDropShadow(
                source.Handle,
                new Margins(0, 0, 0, 0)
                );
        }
    }
}
