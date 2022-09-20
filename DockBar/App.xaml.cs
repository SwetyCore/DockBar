using DockBar.Common;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using static PluginBase.PluginBase;

namespace DockBar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IEnumerable<IPlugin> Plugins { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {


            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            if (!Directory.Exists("Plugins"))
            {
                Directory.CreateDirectory("Plugins");
            }
            try
            {
                Plugins = new PluginLoader().Load();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            GlobalValues.allPlugins = new List<Model.PluginItem>();

            if (Plugins!=null)
            {
                foreach (var item in Plugins)
                {
                    GlobalValues.allPlugins.Add(new Model.PluginItem()
                    {
                        plugin = item,

                    });
                }
            }

            base.OnStartup(e);
        }

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //MessageBox.Show("我们很抱歉，当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员.", "意外的操作", MessageBoxButton.OK, MessageBoxImage.Information);//这里通常需要给用户一些较为友好的提示，并且后续可能的操作

            //File.WriteAllText("err.log", JsonConvert.SerializeObject(e.Exception));

            //Environment.Exit(-1);
        }

    }
}
