using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using comlib;

namespace photolib
{
    public class AutoSettingHelper
    {
        private string inipath;//ini配置文件的URL
        private string mainpath;//主程序的URL
        private string dirpath;//图片存放目录
        private string photoURI;//图片的URL
        private bool autoDownloadPhoto;//是否自动下载图片
        private bool autoSetWall;//是否自动设置壁纸

        public string Inipath { get => inipath; set => inipath = value; }
        public string Mainpath { get => mainpath; set => mainpath = value; }
        public bool AutoDownloadPhoto { get => autoDownloadPhoto; set => autoDownloadPhoto = value; }
        public bool AutoSetWall { get => autoSetWall; set => autoSetWall = value; }
        public string Dirpath { get => dirpath; set => dirpath = value; }
        public string PhotoURI { get => photoURI; set => photoURI = value; }

        public AutoSettingHelper()
        {
            Setting setting = new Setting();
            ConfigHelper configHelper = new ConfigHelper();
            Photo photo = new Photo(0);
            Inipath = setting.GetMyDocumentsPath() + "\\bingphoto.ini";
            Mainpath = configHelper.GetValue("BINGPHOTO", "EXEPATH");
            Dirpath = configHelper.GetValue("BINGPHOTO", "DIRPATH");
            AutoDownloadPhoto = Convert.ToBoolean(configHelper.GetValue("BINGPHOTO", "AUTODOWN"));
            AutoSetWall = Convert.ToBoolean(configHelper.GetValue("BINGPHOTO", "AUTOSET"));
            if(configHelper.GetValue("BINGPHOTO", "RESOV")== "1920x1080")
            {
                PhotoURI = photo.HDUrl;
            }
            else
            {
                PhotoURI = photo.WXGAUrl;
            }
        }
        
        /// <summary>
        /// 自动执行，具体执行动作取决于配置文件，不需要参数。
        /// </summary>
        public void AutoActive()
        {
            if (AutoDownloadPhoto)
            {
                AutoDownload(PhotoURI);
            }
            if (AutoDownloadPhoto && AutoSetWall)
            {
                AutoSetwall(PhotoURI);
            }
        }

        /// <summary>
        /// 从指定URL自动下载图片，只能被AutoActive()调用
        /// </summary>
        /// <param name="photouri">图片的URL</param>
        private void AutoDownload(string photouri)
        {
            HttpHelper httpHelper = new HttpHelper();
            httpHelper.DownLoadPhoto(photouri);
        }

        /// <summary>
        /// 自动设置壁纸，只能被AutoActive()调用
        /// </summary>
        /// <param name="photouri">图片的URL</param>
        private void AutoSetwall(string photouri)
        {

            Setting setting = new Setting();
            AutoDownload(photouri);
            setting.SetWallpaper(Dirpath + "/" + System.IO.Path.GetFileName(photouri));
        }
    }
}
