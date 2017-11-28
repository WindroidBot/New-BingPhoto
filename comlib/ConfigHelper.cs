using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace comlib
{
    public class ConfigHelper
    {
        #region 声明读写INI文件的API函数
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString
            (string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString
            (string section, string key, string defVal, StringBuilder retVal, int size, string filePath);
        #endregion

        /// <summary>
        /// 读取ini文件的值
        /// </summary>
        /// <param name="section">节名</param>
        /// <param name="key">键名</param>
        /// <returns></returns>
        public string GetValue(string section,string key)
        {
            string iniPath = (new Setting()).GetMyDocumentsPath() + "\\bingphoto.ini";
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(section, key, "", temp, 1024, iniPath);
            return temp.ToString();
        }
        
        /// <summary>
        /// 修改ini文件的值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void SetValue(string section,string key,string value)
        {
            string iniPath = (new Setting()).GetMyDocumentsPath() + "\\bingphoto.ini";
            WritePrivateProfileString(section, key, value, iniPath);
        }
              
        /// <summary>
        /// 初始化ini配置文件并赋值
        /// </summary>
        public void Initialise_ini()
        {
            Setting setting = new Setting();
            string iniPath = setting.GetMyDocumentsPath() + "\\bingphoto.ini";
            if (!System.IO.File.Exists(iniPath))
            {
                //[BINGPHOTO]
                WritePrivateProfileString("BINGPHOTO", "OLDWALLPATH", setting.GetOldWallpaperPath(), iniPath);
                WritePrivateProfileString("BINGPHOTO", "DIRPATH", setting.GetMyDocumentsPath() + "\\BingPhotos", iniPath);
                WritePrivateProfileString("BINGPHOTO", "EXEPATH", setting.GetRunPath(), iniPath);
                WritePrivateProfileString("BINGPHOTO", "RESOV", "1920x1080", iniPath);
                WritePrivateProfileString("BINGPHOTO", "AUTODOWN", false.ToString(), iniPath);
                WritePrivateProfileString("BINGPHOTO", "AUTOSET", false.ToString(), iniPath);
                WritePrivateProfileString("BINGPHOTO", "PUBLICNAME", false.ToString(), iniPath);
                //[LOCKSCREEN]
                WritePrivateProfileString("LOCKSCREEN", "ASSETS", setting.GetMyAppdataPath()+ "\\Packages\\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\\LocalState\\Assets", iniPath);
                WritePrivateProfileString("LOCKSCREEN", "OUTPATH", setting.GetMyDocumentsPath() + "\\BingPhotos\\locksereen", iniPath);
                WritePrivateProfileString("LOCKSCREEN", "MOBLELOCK", false.ToString(), iniPath);
                //[AUTO]
                WritePrivateProfileString("AUTO", "LASTDWNDATE", "", iniPath);
                WritePrivateProfileString("AUTO", "LASTSETDATE", "", iniPath);

                //File.SetAttributes(iniPath, FileAttributes.Hidden); //设置为隐藏文件
                Console.WriteLine("【system】配置文件不存在，并已创建！");
            }
            else
            {
                Console.WriteLine("【system】配置文件已存在！");
                Flush_ini(iniPath);
            }
        }

        /// <summary>
        /// 刷新ini配置文件，只允许被Initialise_ini()调用
        /// </summary>
        private void Flush_ini(string iniPath)
        {
            Setting setting = new Setting();
            WritePrivateProfileString("BINGPHOTO", "OLDWALLPATH", setting.GetOldWallpaperPath(), iniPath);
            WritePrivateProfileString("BINGPHOTO", "EXEPATH", setting.GetRunPath(), iniPath);
            Console.WriteLine("【system】配置文件已刷新！");
        }
    }
}