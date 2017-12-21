using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace comlib
{
    public class Setting
    {
        /// <summary>
        /// 获取当前操作系统的次要版本号
        /// </summary>
        /// <returns>当前操作系统的次要版本号</returns>
        public int GetCurrentVersionMajor()
        {
            Version currentVersion = Environment.OSVersion.Version;
            Console.WriteLine(currentVersion.Major);
            return currentVersion.Major;
        }

        /// <summary>
        /// 获取当前用户“我的文档”绝对路径
        /// </summary>
        /// <returns>我的文档绝对路径</returns>
        public string GetMyDocumentsPath()
        {
            string MyDocumentsURL = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return MyDocumentsURL;
        }

        /// <summary>
        /// 获取当前用户“Appdata/Local”绝对路径
        /// </summary>
        /// <returns>当前用户“Appdata/Local”绝对路径</returns>
        public string GetMyAppdataPath()
        {
            string MyAppdataURL = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return MyAppdataURL;
        }

        /// <summary>
        /// 获取正在执行的程序绝对路径
        /// </summary>
        /// <returns>正在执行的程序绝对路径</returns>
        public string GetRunPath()
        {
            string str = Process.GetCurrentProcess().MainModule.FileName;
            return str;
        }

        /// <summary>
        /// 获取指定文件的md5值
        /// </summary>
        /// <param name="fileName">完整的文件名</param>
        /// <returns>文件的md5值</returns>
        public string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, System.IO.FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("[system]" + ex.Message);
            }
        }

        #region 声明设置壁纸的函数
        [DllImport("user32.dll")]
        static extern bool SystemParametersInfo(int uAction, int uParam,
            string IpvParam, int fuWinIni);
        #endregion

        /// <summary>
        /// 设置壁纸
        /// </summary>
        /// <param name="Path">图片的绝对路径</param>
        public void SetWallpaper(string Path)
        {
            SystemParametersInfo(20, 0, Path, 0x01 | 0x02);
        }

        #region 声明设置壁纸的函数获取windows桌面背景的函数
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SystemParametersInfo(int uAction, int uParam, StringBuilder lpvParam, int fuWinIni);
        private const int SPI_GETDESKWALLPAPER = 0x0073;
        #endregion

        /// <summary>
        /// 获取当前用户壁纸路径
        /// </summary>
        /// <returns>当前用户壁纸路径</returns>
        public string GetOldWallpaperPath()
        {
            StringBuilder s = new StringBuilder(300);//定义存储缓冲区大小
            SystemParametersInfo(SPI_GETDESKWALLPAPER, 300, s, 0);//获取Window 桌面背景图片地址，使用缓冲区
            string OldWallpaper_path = s.ToString(); //系统桌面背景图片路径
            return OldWallpaper_path;
        }

        #region 声明判断Internet连通性的函数
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(int Description, int ReservedValue);
        #endregion

        /// <summary>
        /// 循环判断Internet连接是否正常，每500ms检测一次，可以定义检查失败的阈值
        /// </summary>
        /// <param name="threshold">检查失败的阈值</param>
        /// <returns>以布尔值返回检测结果</returns>
        public static bool TestConnectInternet(int threshold)
        {
            int Description = 0;
            for(int i = 0; i < threshold; i++)
            {
                if (!InternetGetConnectedState(Description, 0))
                {
                    Console.WriteLine("[system]Connection Failed"+i);
                    Thread.Sleep(500);
                }
                else
                {
                    Console.WriteLine("[system]Connection Successful");
                    return true;
                }
            }
            //System.Windows.MessageBox.Show("网络连接失败，请检查！\n", "必应美图小助手", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            return false;
        }

        /// <summary>
        /// 循环移动指定目录中的文件到另一个目录
        /// </summary>
        /// <param name="sourcedir">源目录</param>
        /// <param name="destdir">目的目录</param>
        public static void MoveFiles(string sourcedir,string destdir)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(sourcedir);
            foreach(FileInfo nextFile in directoryInfo.GetFiles())
            {
                try
                {
                    nextFile.MoveTo(destdir + "\\" + nextFile.Name);
                }
                catch(IOException e)
                {
                    Console.WriteLine("[system]" + nextFile.Name + "\n" + e);
                    continue;
                }                                
                Console.WriteLine("[system]File Name:" + nextFile.Name);
            }
        }
    }
}