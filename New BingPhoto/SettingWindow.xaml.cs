using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using comlib;
using photolib;

namespace New_BingPhoto
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置窗口打开时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigHelper configHelper = new ConfigHelper();
            if (configHelper.GetValue("RESOV") == "1920x1080")
            {
                radioButton_Resolving1080.IsChecked = true;
            }
            else
            {
                radioButton_Resolving1366.IsChecked = true;
            }
        }

        /// <summary>
        /// 【确定】按钮单击时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            ConfigHelper configHelper = new ConfigHelper();
            //分辨率的设置
            if (radioButton_Resolving1080.IsChecked == true)
            {
                configHelper.SetValue("RESOV", "1920x1080");
            }
            else
            {
                configHelper.SetValue("RESOV", "1366x768");
            }
            //其他设置写在这

            //其他设置写在这

            Close();
        }
    }
}
