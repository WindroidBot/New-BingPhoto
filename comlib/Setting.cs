using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace comlib
{
    public class Setting
    {
        /// <summary>
        /// 获取“我的文档”绝对路径
        /// </summary>
        /// <returns>我的文档绝对路径</returns>
        public string GetMyDocumentsPath()
        {
            string MyDocumentsURL = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return MyDocumentsURL;
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
    }
}
