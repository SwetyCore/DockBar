using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static PluginBase.PluginBase;

namespace DockBar.View
{
    /// <summary>
    /// PluginManage.xaml 的交互逻辑
    /// </summary>
    public partial class PluginManage : Page
    {
        public PluginManage()
        {
            InitializeComponent();
            DataContext = Common.GlobalValues.pmvm;
        }



        private void LBoxSort_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }

        private void LBoxSort_Drop(object sender, DragEventArgs e)
        {
            var items = (LBoxSort.ItemsSource as ObservableCollection<IPluginControl>);
            var pos = e.GetPosition(LBoxSort);
            var result = VisualTreeHelper.HitTest(LBoxSort, pos);
            if (result == null)
            {
                return;
            }
            //查找元数据
            var fs = e.Data.GetFormats();
            var sourcePerson =e.Data.GetData(fs.FirstOrDefault()) as IPluginControl;
            if (sourcePerson == null)
            {
                return;
            }
            //查找目标数据
            var listBoxItem = Utils.FindVisualParent<ListBoxItem>(result.VisualHit);
            if (listBoxItem == null)
            {
                return;
            }
            var targetPerson = (listBoxItem.Content )as  IPluginControl;
            if (ReferenceEquals(targetPerson, sourcePerson))
            {
                return;
            }
            var si = items.IndexOf(sourcePerson);
            var ti = items.IndexOf(targetPerson);

            items.Remove(sourcePerson );
            if (si>ti)
            {
                items.Insert(ti, sourcePerson);

            }
            else
            {
                items.Insert(ti, sourcePerson);

            }
            //items[ti] = sourcePerson as IPluginControl;


        }

        internal static class Utils
        {

            
            //根据子元素查找父元素
            public static T FindVisualParent<T>(DependencyObject obj) where T : class
            {
                while (obj != null)
                {
                    if (obj is T)
                        return obj as T;

                    obj = VisualTreeHelper.GetParent(obj);
                }
                return null;
            }
        }

        private void LBoxSort_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var pos = e.GetPosition(LBoxSort);
                HitTestResult result = VisualTreeHelper.HitTest(LBoxSort, pos);
                if (result == null)
                {
                    return;
                }
                var listBoxItem = Utils.FindVisualParent<ListBoxItem>(result.VisualHit);
                if (listBoxItem == null || listBoxItem.Content != LBoxSort.SelectedItem)
                {
                    return;
                }
                DataObject dataObj = new DataObject(listBoxItem.Content);
                DragDrop.DoDragDrop(LBoxSort, dataObj, DragDropEffects.Move);
            }

        }


    }
}
