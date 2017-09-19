using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace comlib
{
    public class HttpHelper
    {
        /// <summary>
        /// 构造请求字符串
        /// </summary>
        /// <param name="idx">相对日期参数</param>
        /// <param name="n">结果集数量</param>
        /// <returns>请求字符串</returns>
       public string GetRequestUrl(int idx,int n)
        {
            string RequestUrl = "http://cn.bing.com/HPImageArchive.aspx?format=js&idx=" + idx + "&n=" + n;
            return RequestUrl;
        }            
        
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
        /// 
        /// </summary>
        /// <param name="jsonstr"></param>
        /// <param name="index"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetJsonValue(string jsonstr, int index, string key)
        {
            JObject jsonObj = JObject.Parse(jsonstr);
            return jsonObj["images"][index][key].ToString();
        }

        /// <summary>
        /// 直接获取json的字段值
        /// </summary>
        /// <param name="idx">相对日期参数</param>
        /// <param name="n">结果集数量</param>
        /// <param name="index">结果集元素的索引</param>
        /// <param name="key">元素中的字段</param>
        /// <returns>json中的字段值</returns>
        public string DirectGetJsonValue(int idx, int n, int index, string key)
        {
            HttpHelper httpHelper = new HttpHelper();
            return httpHelper.GetJsonValue(httpHelper.GetHttpData(httpHelper.GetRequestUrl(idx, n)), index, key);
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="fileUrl">壁纸的URL</param>
        /// <param name="PhotoDir">壁纸存放目录</param>
        /// <returns>下载成功返回true否则为false</returns>
        public bool DownLoadPhoto(string fileUrl)
        {
            Setting setting = new Setting();
            ConfigHelper configHelper = new ConfigHelper();
            string PhotoDir = configHelper.GetValue("DIRPATH");
            Console.WriteLine("【system】读取到的图片保存目录是：" + PhotoDir);
            if (!Directory.Exists(PhotoDir))
            {
                Directory.CreateDirectory(PhotoDir);
                Console.WriteLine("【system】图片目录不存在，并已创建，在" + PhotoDir);
            }
            WebClient webClient = new WebClient();
            try
            {
                webClient.DownloadFile(fileUrl, PhotoDir + "/" + Path.GetFileName(fileUrl));
                Console.WriteLine("【system】下载成功！");
                return true;
            }
            catch (System.Net.WebException)
            {
                Console.WriteLine("【system】下载失败！");
                return false;
            }            
        }
    }
}
