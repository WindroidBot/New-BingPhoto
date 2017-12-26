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
            if (CheckBox_AutoDownload.IsEnabled == true)
            {
                Combox_country.IsEnabled = false;
            }
            ConfigHelper configHelper = new ConfigHelper();
            //读取配置文件，并从窗体体现
            if (configHelper.GetValue("BINGPHOTO","RESOV") == "1920x1080")
            {
                radioButton_Resolving1080.IsChecked = true;
            }
            else
            {
                radioButton_Resolving1366.IsChecked = true;
            }
            if(configHelper.GetValue("BINGPHOTO", "AUTODOWN") == "True")
            {
                CheckBox_AutoDownload.IsChecked = true;
            }
            if(configHelper.GetValue("BINGPHOTO", "AUTOSET") == "True")
            {
                CheckBox_AutoSetWall.IsChecked = true;
            }
            Combox_country.Text = (new HttpHelper()).GetRequestCountry(configHelper.GetValue("AUTO", "MKT"));
            
        }

        /// <summary>
        /// 【确定】按钮单击时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            ConfigHelper configHelper = new ConfigHelper();
            AutoSettingHelper autoSettingHelper = new AutoSettingHelper("setting");
            //分辨率的设置
            if (radioButton_Resolving1080.IsChecked == true)
            {
                configHelper.SetValue("BINGPHOTO", "RESOV", "1920x1080");
            }
            else
            {
                configHelper.SetValue("BINGPHOTO", "RESOV", "1366x768");
            }
            //复选框的设置
            if (CheckBox_AutoDownload.IsChecked == true)
            {
                configHelper.SetValue("BINGPHOTO", "AUTODOWN", true.ToString());
            }
            else
            {
                configHelper.SetValue("BINGPHOTO", "AUTODOWN", false.ToString());
            }
            if (CheckBox_AutoSetWall.IsChecked == true)
            {
                configHelper.SetValue("BINGPHOTO", "AUTOSET", true.ToString());
            }
            else
            {
                configHelper.SetValue("BINGPHOTO", "AUTOSET", false.ToString());
            }
            //开机启动项的设置
            if ((CheckBox_AutoDownload.IsChecked == true) || (CheckBox_AutoSetWall.IsChecked == true))
            {
                //设置启动项
                string exePath = configHelper.GetValue("BINGPHOTO", "EXEPATH");
                autoSettingHelper.SetSetupWindowOpenRun("-autoActive", "开机自动下载、设置壁纸");
                //设置计划任务
                TaskSchedulerHelper.DeleteTaskScheduler("New BingPhoto");
                SchtasksHelper schtasksHelper = new SchtasksHelper("New BingPhoto", exePath, "-autoActive", "DAILY", "1", "00:01:00");
                schtasksHelper.CreateSchtask();
            }
            else
            {
                //删除启动项
                autoSettingHelper.UnSetSetupWindowOpenRun();
                //删除计划任务
                TaskSchedulerHelper.DeleteTaskScheduler("New BingPhoto");
            }
            //保存mkt参数
            configHelper.SetValue("AUTO", "MKT", (new HttpHelper()).GetRequestMkt(Combox_country.Text));
            //其他设置写在这
            Close();
        }

        /// <summary>
        /// 【自动设置壁纸】复选框选中时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_AutoSetWall_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox_AutoDownload.IsChecked = true;
            Combox_country.IsEnabled = true;
            //AutoSettingHelper autoSettingHelper = new AutoSettingHelper();
        }

        /// <summary>
        /// 【自动下载壁纸】复选框取消选中时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_AutoDownload_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox_AutoSetWall.IsChecked = false;
            Combox_country.IsEnabled = false;
            //AutoSettingHelper autoSettingHelper = new AutoSettingHelper();
        }

        /// <summary>
        /// 【自动设置壁纸】复选框取消选中时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_AutoSetWall_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CheckBox_AutoDownload.IsChecked == false)
            {
                Combox_country.IsEnabled = false;
            }
        }

        private void CheckBox_AutoDownload_Checked(object sender, RoutedEventArgs e)
        {
            Combox_country.IsEnabled = true;
        }
    }
}
