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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using comlib;
using photolib;

namespace New_BingPhoto
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 主窗口加载时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigHelper configHelper = new ConfigHelper();
            configHelper.Initialise_ini();
            Lable_dirPath.Content = configHelper.GetValue("DIRPATH");
        }

        /// <summary>
        /// 主窗口关闭时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 【查询】按钮单击时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            HttpHelper httpHelper = new HttpHelper();
            int idx = httpHelper.GetRequestIdx(Combox_date.Text);
            if (idx < 8)
            {
                Photo photo = new Photo(idx);
                image_Photobox.Source = new BitmapImage(new Uri(photo.HDUrl));
            }
            else
            {
                Photos photos = new Photos(7, 8);
                image_Photobox.Source = new BitmapImage(new Uri(photos.GetAphotoValue(idx - 7).HDUrl));
            }


        }

        /// <summary>
        /// 【设为壁纸】按钮单击时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_setWall_Click(object sender, RoutedEventArgs e)
        {
            HttpHelper httpHelper = new HttpHelper();
            Setting setting = new Setting();
            int idx = httpHelper.GetRequestIdx(Combox_date.Text);
            if (idx < 8)
            {
                Photo photo = new Photo(idx);
                setting.SetWallpaper(photo.HDUrl);
            }
            else
            {
                Photos photos = new Photos(7, 8);
                setting.SetWallpaper(photos.GetAphotoValue(idx - 7).HDUrl);
            }
        }

        /// <summary>
        /// 【还原壁纸】按钮单击时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_resetWall_Click(object sender, RoutedEventArgs e)
        {
            ConfigHelper configHelper = new ConfigHelper();
            Setting setting = new Setting();
            setting.SetWallpaper(configHelper.GetValue("OLDWALLPATH"));
        }

        /// <summary>
        /// 【查看目录】按钮单击时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_opendir_Click(object sender, RoutedEventArgs e)
        {
            ConfigHelper configHelper = new ConfigHelper();
            if (!Directory.Exists(Lable_dirPath.Content.ToString()))
            {
                Directory.CreateDirectory(Lable_dirPath.Content.ToString());
                System.Windows.MessageBox.Show("美图保存目录不存在，并已创建！\n" + Lable_dirPath.Content,
                "必应美图小助手", MessageBoxButton.OK,MessageBoxImage.Asterisk);
            }
            System.Diagnostics.Process.Start("explorer.exe", Lable_dirPath.Content.ToString());
        }

        /// <summary>
        /// 【设置】按钮单击时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_setting_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow Form_otherSet = new SettingWindow();
            Form_otherSet.ShowDialog();
        }

        /// <summary>
        /// 【更改目录】按钮单击时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_SetDir_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                Description = "选择必应美图下载的目录",
                ShowNewFolderButton = true
            };
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ConfigHelper configHelper = new ConfigHelper();
                Lable_dirPath.Content = folderBrowserDialog.SelectedPath;
                configHelper.SetValue("DIRPATH", folderBrowserDialog.SelectedPath);
            }

        }

        /// <summary>
        /// 【下载】按钮单击时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_download_Click(object sender, RoutedEventArgs e)
        {
            HttpHelper httpHelper = new HttpHelper();

        }
    }
}