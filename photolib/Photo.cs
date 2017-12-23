using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using comlib;
using System.Text.RegularExpressions;

namespace photolib
{
    public class Photo
    {
        private int enddate;//开始日期
        private string country;//国家和地区
        private string requestStr;//请求字符串
        private string hDUrl;//1920x1080 分辨率图片URL
        private string wXGAUrl;//1366x768 分辨率图片URL
        private string copyright;//版权与说明信息
        private string publicName;//更加友好的文件名
        private string hash;//图片的哈希值

        public int Startdate { get => enddate; set => enddate = value; }
        public string RequestStr { get => requestStr; set => requestStr = value; }
        public string HDUrl { get => hDUrl; set => hDUrl = value; }
        public string WXGAUrl { get => wXGAUrl; set => wXGAUrl = value; }
        public string Copyright { get => copyright; set => copyright = value; }
        public string PublicName { get => publicName; set => publicName = value; }
        public string Hash { get => hash; set => hash = value; }
        public string Country { get => country; set => country = value; }

        /// <summary>
        /// Photo的构造方法
        /// </summary>
        /// <param name="idx">API中的idx字段（相对时间参数）</param>
        /// <param name="mkt">API中的mkt字段（国家代码）</param>
        public Photo(int idx, string mkt)
        {

            HttpHelper httpHelper = new HttpHelper();
            requestStr = httpHelper.GetRequestUrl(idx, 1, mkt);
            Console.WriteLine("[system]Request URL String is" + requestStr);
            string requestJson = httpHelper.GetHttpData(httpHelper.GetRequestUrl(idx, 1, mkt));
            enddate = int.Parse(httpHelper.GetJsonValue(requestJson, 0, "enddate"));
            hDUrl = "http://www.bing.com" + httpHelper.GetJsonValue(requestJson, 0, "url");
            wXGAUrl = "http://www.bing.com" + httpHelper.GetJsonValue(requestJson, 0, "urlbase") + "_1366x768.jpg";
            copyright = httpHelper.GetJsonValue(requestJson, 0, "copyright");
            hash = httpHelper.GetJsonValue(requestJson, 0, "hsh");
            publicName = GetPublicName();
        }

        private string GetPublicName()
        {
            string[] sArray = Regex.Split(copyright, "，", RegexOptions.IgnoreCase);
            //Console.WriteLine(sArray[0]);
            return sArray[0];
        }
    }
}
