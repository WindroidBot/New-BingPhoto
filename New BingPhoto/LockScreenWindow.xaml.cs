using System;
using System.Collections.Generic;
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
        /// 【锁屏工具】打开时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigHelper configHelper = new ConfigHelper();
            TextBlock_OutputPath.Text = configHelper.GetValue("LOCKSCREEN", "OUTPATH");
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
        /// 【一键导出】单击时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Output_Click(object sender, RoutedEventArgs e)
        {
            ConfigHelper configHelper = new ConfigHelper();
            Lockscreen lockscreen = new Lockscreen(configHelper.GetValue("LOCKSCREEN", "ASSETS"), configHelper.GetValue("LOCKSCREEN", "OUTPATH"));
            lockscreen.OutputScreen(false);
        }
    }
}
