using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Windows.Forms;

namespace comlib
{
    public class HttpHelper
    {
        /// <summary>
        /// 将输入转化为API对应的时间参数，最多至前14天
        /// </summary>
        /// <param name="idstr">输入的相对时间参数</param>
        /// <returns>api中的idx可接受的值</returns>
        public int GetRequestIdx(string idstr)
        {
            switch (idstr)
            {
                case "明天":
                    return -1;
                case "今天":
                    return 0;
                case "昨天":
                    return 1;
                case "前天":
                    return 2;
                case "前3天":
                    return 3;
                case "前4天":
                    return 4;
                case "前5天":
                    return 5;
                case "前6天":
                    return 6;
                case "前7天":
                    return 7;
                case "前8天":
                    return 8;
                case "前9天":
                    return 9;
                case "前10天":
                    return 10;
                case "前11天":
                    return 11;
                case "前12天":
                    return 12;
                case "前13天":
                    return 13;
                case "前14天":
                    return 14;
            }
            return -2;
        }

        /// <summary>
        /// 将输入转化为API对应的国家参数
        /// </summary>
        /// <param name="mktstr">输入的国家参数</param>
        /// <returns>api中的mkt可接受的值</returns>
        public string GetRequestMkt(string mktstr)
        {
            switch (mktstr)
            {
                case "中国":
                    return "zh-cn";
                case "日本":
                    return "ja-jp";
                case "印度":
                    return "en-in";
                case "德国":
                    return "de-de";
                case "法国":
                    return "fr-fr";
                case "英国":
                    return "en-gb";
                case "巴西":
                    return "pt-br";
                case "加拿大":
                    return "en-ca";
                case "美国":
                    return "en-us";
                case "澳大利亚":
                    return "en-au";
            }
            return "";
        }

        /// <summary>
        /// 构造请求字符串
        /// </summary>
        /// <param name="idx">相对日期参数</param>
        /// <param name="n">结果集数量</param>
        /// <returns>请求字符串</returns>
       public string GetRequestUrl(int idx,int n,string mkt)
        {
            string RequestUrl = "http://www.bing.com/HPImageArchive.aspx?format=js&idx=" + idx + "&n=" + n + "&mkt=" + mkt;
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
        /// 获取JSON的字段值
        /// </summary>
        /// <param name="jsonstr">json字符串</param>
        /// <param name="index">json中的索引</param>
        /// <param name="key">键名</param>
        /// <returns>json字段的值</returns>
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
        public string DirectGetJsonValue(int idx, int n,string mkt ,int index, string key)
        {
            HttpHelper httpHelper = new HttpHelper();
            return httpHelper.GetJsonValue(httpHelper.GetHttpData(httpHelper.GetRequestUrl(idx, n, mkt)), index, key);
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="fileUrl">壁纸的URL</param>
        /// <returns>下载成功返回true否则为false</returns>
        public void DownLoadPhoto(string fileUrl)
        {
            Setting setting = new Setting();
            ConfigHelper configHelper = new ConfigHelper();
            string PhotoDir = configHelper.GetValue("BINGPHOTO", "DIRPATH");
            //Console.WriteLine("【system】读取到的图片保存目录是：" + PhotoDir);
            if (!Directory.Exists(PhotoDir))
            {
                Directory.CreateDirectory(PhotoDir);
                System.Windows.Forms.MessageBox.Show("美图保存目录不存在，并已创建！\n" + PhotoDir,
                "必应美图小助手", MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                Console.WriteLine("【system】图片目录不存在，并已创建，在" + PhotoDir);
            }
            //判断文件是否存在，若存在则直接返回
            if (File.Exists(configHelper.GetValue("BINGPHOTO","DIRPATH") + "/" + Path.GetFileName(fileUrl)))
            {
                Console.WriteLine("【system】文件已存在");
                return;
            }
            WebClient webClient = new WebClient();
            try
            {
                webClient.DownloadFile(fileUrl, PhotoDir + "/" + Path.GetFileName(fileUrl));
                Console.WriteLine("【system】下载成功！");
            }
            catch (System.Net.WebException)
            {
                Console.WriteLine("【system】下载失败！");
            }            
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="fileUrl">壁纸的URL</param>
        /// <param name="fileName">自定义文件名</param>
        public void DownLoadPhoto(string fileUrl,string fileName)
        {
            Setting setting = new Setting();
            ConfigHelper configHelper = new ConfigHelper();
            string PhotoDir = configHelper.GetValue("BINGPHOTO", "DIRPATH");
            //Console.WriteLine("【system】读取到的图片保存目录是：" + PhotoDir);
            if (!Directory.Exists(PhotoDir))
            {
                Directory.CreateDirectory(PhotoDir);
                System.Windows.Forms.MessageBox.Show("美图保存目录不存在，并已创建！\n" + PhotoDir,
                "必应美图小助手", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Console.WriteLine("【system】图片目录不存在，并已创建，在" + PhotoDir);
            }
            //判断文件是否存在，若存在则直接返回
            if (File.Exists(configHelper.GetValue("BINGPHOTO", "DIRPATH") + "/" + fileName + ".jpg"))
            {
                Console.WriteLine("【system】文件已存在");
                return;
            }           
            WebClient webClient = new WebClient();
            try
            {
                //webClient.DownloadFile(fileUrl, PhotoDir + "/" + Path.GetFileName(fileUrl));
                webClient.DownloadFile(fileUrl, PhotoDir + "/" + fileName + ".jpg");
                Console.WriteLine("【system】下载成功！");
            }
            catch (System.Net.WebException)
            {
                Console.WriteLine("【system】下载失败！");
            }
        }
    }
}
