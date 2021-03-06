﻿using System;
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
            //判断用户操作系统版本，若不为Win10，则导出“聚焦”图片功能不可用
            if ((new Setting()).GetCurrentVersionMajor() == 10)
            {
                Button_LockScreen.IsEnabled = true;
            }
            else
            {
                Button_LockScreen.IsEnabled = false;
            }
            if (!Setting.TestConnectInternet(1))
            {
                System.Windows.MessageBox.Show("网络连接失败，部分功能将不可用！\n", "必应美图小助手", MessageBoxButton.OK, MessageBoxImage.Error);
                Combox_date.IsEnabled = false;
                Button_Search.IsEnabled = false;
                Button_download.IsEnabled = false;
                Button_setWall.IsEnabled = false;
                Button_resetWall.IsEnabled = false;
                Button_downloadAll.IsEnabled = false;
                //Button_setting.IsEnabled = false;
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
            string mkt = httpHelper.GetRequestMkt(Combox_country.Text);
            if (idx < 8)
            {
                Photo photo = new Photo(idx, mkt);
                httpHelper.DownLoadPhoto(photo.HDUrl);
                setting.SetWallpaper(imagedir + "/" + System.IO.Path.GetFileName(photo.HDUrl));
            }
            else
            {
                Photos photos = new Photos(7, 8, mkt);
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
                string oldpath = configHelper.GetValue("BINGPHOTO", "DIRPATH");
                if (folderBrowserDialog.SelectedPath == oldpath)
                {
                    return;
                }
                if(System.Windows.MessageBox.Show("您更改了图片目录，是否要迁移图片？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Setting.MoveFiles(oldpath, folderBrowserDialog.SelectedPath);
                }
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
            string mkt = httpHelper.GetRequestMkt(Combox_country.Text);
            if (idx < 8)
            {
                Photo photo = new Photo(idx, mkt);
                if (configHelper.GetValue("BINGPHOTO","RESOV") == "1920x1080")
                {
                    //httpHelper.DownLoadPhoto(photo.HDUrl);
                    httpHelper.DownLoadPhoto(photo.HDUrl, photo.PublicName);
                }
                else
                {
                    httpHelper.DownLoadPhoto(photo.WXGAUrl);
                }                
            }
            else
            {
                Photos photos = new Photos(7, 8, mkt);
                if (configHelper.GetValue("BINGPHOTO","RESOV") == "1920x1080")
                {
                    //httpHelper.DownLoadPhoto(photos.GetAphotoValue(idx - 7).HDUrl);
                    httpHelper.DownLoadPhoto(photos.GetAphotoValue(idx - 7).HDUrl, photos.GetAphotoValue(idx - 7).Copyright);
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
            Photos photos0 = new Photos(-1, 8, "zh-cn");
            Photos photos1 = new Photos(7, 8, "zh-cn");
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

        /// <summary>
        /// 选择日期后查询预览的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Combox_date_DropDownClosed(object sender, EventArgs e)
        {
            HttpHelper httpHelper = new HttpHelper();
            int idx = httpHelper.GetRequestIdx(Combox_date.Text);
            string mkt = httpHelper.GetRequestMkt(Combox_country.Text);
            if (idx < 8)
            {
                Photo photo = new Photo(idx,mkt);
                Console.WriteLine(photo.RequestStr);
                Label_copyright.Content = photo.Copyright;
                image_Photobox.Source = new BitmapImage(new Uri(photo.HDUrl));
            }
            else
            {
                Photos photos = new Photos(7, 8, mkt);
                Label_copyright.Content = photos.GetAphotoValue(idx - 7).Copyright;
                image_Photobox.Source = new BitmapImage(new Uri(photos.GetAphotoValue(idx - 7).HDUrl));
            }
        }

        /// <summary>
        /// 【关于】按钮点击是执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_AboutBox_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();

        }

        /// <summary>
        /// 【查询】按钮点击是执行的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            HttpHelper httpHelper = new HttpHelper();
            int idx = httpHelper.GetRequestIdx(Combox_date.Text);
            string mkt = httpHelper.GetRequestMkt(Combox_country.Text);
            if (idx < 8)
            {
                Photo photo = new Photo(idx, mkt);
                Label_copyright.Content = photo.Copyright;
                image_Photobox.Source = new BitmapImage(new Uri(photo.HDUrl));
            }
            else
            {
                Photos photos = new Photos(7, 8, mkt);
                Label_copyright.Content = photos.GetAphotoValue(idx - 7).Copyright;
                image_Photobox.Source = new BitmapImage(new Uri(photos.GetAphotoValue(idx - 7).HDUrl));
            }
        }
    }
}