using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using comlib;

namespace photolib
{
    public class Photo
    {
        private int startdate;//开始日期
        public enum Countrylist { _JP, _CN, _IN, _DE, _FR, _GB, _BR, _CA, _US, _AU };//国家和地区
        private string requestStr;//请求字符串
        private string hDUrl;//1920x1080 分辨率图片URL
        private string wXGAUrl;//1366x768 分辨率图片URL
        private string copyright;//版权与说明信息

        public int Startdate { get => startdate; set => startdate = value; }
        public string RequestStr { get => requestStr; set => requestStr = value; }
        public string HDUrl { get => hDUrl; set => hDUrl = value; }
        public string WXGAUrl { get => wXGAUrl; set => wXGAUrl = value; }
        public string Copyright { get => copyright; set => copyright = value; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx">API中的idx字段（相对时间参数）</param>
        /// <param name="n">API中的n字段（返回的结果数目）</param>
        public Photo(int idx, int n)
        {

            HttpHelper httpHelper = new HttpHelper();
            string requestJson = httpHelper.GetHttpData(httpHelper.GetRequestUrl(idx, n));
            this.startdate = int.Parse(httpHelper.GetJsonValue(requestJson, 0, "startdate"));
            this.requestStr = httpHelper.GetRequestUrl(idx, n);
            this.hDUrl = "http://www.bing.com" + httpHelper.GetJsonValue(requestJson, 0, "url");
            this.wXGAUrl = "http://www.bing.com" + httpHelper.GetJsonValue(requestJson, 0, "urlbase") + "_1366x768.jpg";
            this.copyright = httpHelper.GetJsonValue(requestJson, 0, "copyright");
        }
    }
}
