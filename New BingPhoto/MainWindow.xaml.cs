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
            Lable_dirPath.Content = configHelper.GetValue("BINGPHOTO", "DIRPATH");
            if ((Environment.OSVersion.Version).Major == 10)
            {
                Button_LockScreen.IsEnabled = true;
            }
            else
            {
                Button_LockScreen.IsEnabled = false;
            }
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
            ConfigHelper configHelper = new ConfigHelper();
            string imagedir = configHelper.GetValue("BINGPHOTO","DIRPATH");
            int idx = httpHelper.GetRequestIdx(Combox_date.Text);
            if (idx < 8)
            {
                Photo photo = new Photo(idx);
                httpHelper.DownLoadPhoto(photo.HDUrl);
                setting.SetWallpaper(imagedir + "/" + System.IO.Path.GetFileName(photo.HDUrl));
            }
            else
            {
                Photos photos = new Photos(7, 8);
                httpHelper.DownLoadPhoto(photos.GetAphotoValue(idx - 7).HDUrl);
                setting.SetWallpaper(imagedir + "/" + System.IO.Path.GetFileName(photos.GetAphotoValue(idx - 7).HDUrl));
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
            setting.SetWallpaper(configHelper.GetValue("BINGPHOTO","OLDWALLPATH"));
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
            SettingWindow settingWindow = new SettingWindow();
            settingWindow.ShowDialog(); 
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
                ShowNewFolderButton = true,
            };
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ConfigHelper configHelper = new ConfigHelper();
                Lable_dirPath.Content = folderBrowserDialog.SelectedPath;
                configHelper.SetValue("BINGPHOTO", "DIRPATH", folderBrowserDialog.SelectedPath);
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
            ConfigHelper configHelper = new ConfigHelper();
            int idx = httpHelper.GetRequestIdx(Combox_date.Text);
            if (idx < 8)
            {
                Photo photo = new Photo(idx);
                if (configHelper.GetValue("BINGPHOTO","RESOV") == "1920x1080")
                {
                    httpHelper.DownLoadPhoto(photo.HDUrl);
                }
                else
                {
                    httpHelper.DownLoadPhoto(photo.WXGAUrl);
                }                
            }
            else
            {
                Photos photos = new Photos(7, 8);
                if (configHelper.GetValue("BINGPHOTO","RESOV") == "1920x1080")
                {
                    httpHelper.DownLoadPhoto(photos.GetAphotoValue(idx - 7).HDUrl);
                }
                else
                {
                    httpHelper.DownLoadPhoto(photos.GetAphotoValue(idx - 7).WXGAUrl);
                }               
            }
        }

        /// <summary>
        /// 【一键批量下载】按钮单击时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_downloadAll_Click(object sender, RoutedEventArgs e)
        {
            Photos photos0 = new Photos(-1, 8);
            Photos photos1 = new Photos(7, 8);
            photos0.BeathDownloadImage();
            photos1.BeathDownloadImage();
        }

        /// <summary>
        /// 【Win10锁屏小助手】按钮单击时执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_LockScreen_Click(object sender, RoutedEventArgs e)
        {
            LockScreenWindow lockScreenWindow = new LockScreenWindow();
            lockScreenWindow.ShowDialog();
        }
    }
}