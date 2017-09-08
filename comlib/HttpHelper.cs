using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace comlib
{
    internal class HttpHelper
    {
        /// <summary>
        /// 使用POST方法请求web页面上的数据并返回
        /// </summary>
        /// <param name="uri">请求字符串</param>
        /// <returns>返回字符串</returns>
        public string GetHttpData(string uri)
        {
            string result = null;
            Uri url = new Uri(uri);//初始化uri
            Stream stream = null;
            StreamReader reader = null;
            try
            {
                HttpWebRequest request = (System.Net.HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();//得到响应
                stream = response.GetResponseStream();//获取响应的主体
                reader = new StreamReader(stream);//初始化读取器
                result = reader.ReadToEnd();//读取，存储结果
                reader.Close();
                stream.Close();
            }
            catch (System.Net.WebException)
            {
                return result = "Request Time out!";
            }
            return result;
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="fileUrl">壁纸的URL</param>
        /// <param name="PhotoDir">壁纸存放目录</param>
        /// <returns>下载成功返回true否则为false</returns>
        public bool DownLoadPhoto(string fileUrl, string PhotoDir)
        {
            Setting setting = new Setting();
            if (!Directory.Exists(setting.GetMyDocumentsPath() + "\\BingPhoto"))
            {
                Directory.CreateDirectory(setting.GetMyDocumentsPath() + "\\BingPhoto");
            }
            WebClient webClient = new WebClient();
            try
            {
                webClient.DownloadFile(fileUrl, PhotoDir + "/" + Path.GetFileName(fileUrl));
                return true;
            }
            catch (System.Net.WebException)
            {
                return false;
            }            
        }
    }
}
