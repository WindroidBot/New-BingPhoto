using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using comlib;
using photolib;

namespace New_BingPhoto
{
    /// <summary>
    /// LockScreenWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LockScreenWindow : Window
    {
        public LockScreenWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体打开时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigHelper configHelper = new ConfigHelper();
            TextBlock_OutputPath.Text = configHelper.GetValue("LOCKSCREEN", "OUTPATH");
            if(configHelper.GetValue("LOCKSCREEN", "MOBLELOCK") == true.ToString())
            {
                CheckBox_includePhone.IsChecked = true;
            }
            else
            {
                CheckBox_includePhone.IsChecked = false;
            }
        }

        /// <summary>
        /// 【更改目录】单击时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_SetDir_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                Description = "选择聚焦锁屏转换输出的目录",
                ShowNewFolderButton = true,
            };
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ConfigHelper configHelper = new ConfigHelper();
                TextBlock_OutputPath.Text = folderBrowserDialog.SelectedPath;
                configHelper.SetValue("LOCKSCREEN", "OUTPATH", folderBrowserDialog.SelectedPath);
            }
        }

        /// <summary>
        /// 【一键导出】按钮单击时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Output_Click(object sender, RoutedEventArgs e)
        {
            ConfigHelper configHelper = new ConfigHelper();
            Lockscreen lockscreen = new Lockscreen(configHelper.GetValue("LOCKSCREEN", "ASSETS"), configHelper.GetValue("LOCKSCREEN", "OUTPATH"));
            lockscreen.OutputScreen();
            if (CheckBox_includePhone.IsChecked == false)
            {
                lockscreen.DeleteMoblieLock();
            }
        }

        /// <summary>
        /// 【查看目录】按钮单击时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_openDir_Click(object sender, RoutedEventArgs e)
        {
            ConfigHelper configHelper = new ConfigHelper();
            if (!Directory.Exists(TextBlock_OutputPath.Text.ToString()))
            {
                Directory.CreateDirectory(TextBlock_OutputPath.Text.ToString());
                System.Windows.MessageBox.Show("美图保存目录不存在，并已创建！\n" + TextBlock_OutputPath.Text.ToString(),
                "必应美图小助手", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            System.Diagnostics.Process.Start("explorer.exe", TextBlock_OutputPath.Text.ToString());
        }

        /// <summary>
        /// 关闭窗口时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            ConfigHelper configHelper = new ConfigHelper();
            if (CheckBox_includePhone.IsChecked == true)
            {
                configHelper.SetValue("LOCKSCREEN", "MOBLELOCK", true.ToString());
            }
            else
            {
                configHelper.SetValue("LOCKSCREEN", "MOBLELOCK", false.ToString());
            }
        }
    }
}
